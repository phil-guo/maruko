using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Maruko.Core.Application;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Runtime.Security;
using Maruko.Zero.Config;
using Microsoft.IdentityModel.Tokens;
using IObjectMapper = Maruko.Core.ObjectMapping.IObjectMapper;


namespace Maruko.Zero
{
    public class SysUserService : CurdAppService<SysUser, SysUserDTO>, ISysUserService
    {
        public override SysUserDTO CreateOrEdit(SysUserDTO request)
        {
            if (request.Id == 1 || request.UserName == "admin")
                throw new Exception("admin管理员不允许被修改");

            SysUser data = null;
            if (request.Id == 0)
            {
                request.Password = request.Password.Get32MD5One();
                request.CreateTime = DateTime.Now;
                data = Repository.Insert(ObjectMapper.Map<SysUser>(request));
            }
            else
            {
                data = Table.FirstOrDefault(item => item.Id == request.Id);
                if (data == null)
                    throw new Exception("系统用户不存在");

                data.RoleId = request.RoleId;
                data.UserName = request.UserName;

                if (!string.IsNullOrEmpty(request.Password))
                    data.Password = request.Password.Get32MD5One();

                data = Repository.Update(data);
            }

            return ObjectMapper.Map<SysUserDTO>(data);
        }

        public AjaxResponse<object> Login(LoginVM request)
        {
            if (string.IsNullOrEmpty(request.Name))
                throw new Exception("用户名不能为空");

            if (string.IsNullOrEmpty(request.Password))
                throw new Exception("密码不能为空");

            request.Password = request.Password.Get32MD5One();

            var sysUser =
                Table.FirstOrDefault(item => item.UserName == request.Name && item.Password == request.Password);

            if (sysUser == null)
                throw new Exception("用户名密码错误");

            var config = new Maruko.Core.Web.Config.AppConfig();

            var token = new JwtSecurityToken(
                audience: config.Web.Key,
                claims: new[]
                {
                    new Claim(ClaimTypes.Sid, sysUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, sysUser.UserName),
                    new Claim(ClaimTypes.Role, sysUser.RoleId.ToString()),
                    new Claim(ClaimTypes.Expired, config.Web.AuthExpired.ToString()),
                    new Claim(ClaimTypes.UserData, sysUser.Icon ?? "")
                },
                issuer: config.Web.Key,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(config.Web.AuthExpired),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Web.Secret)),
                    SecurityAlgorithms.HmacSha256)
            );

            return new AjaxResponse<object>(new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }

        public override PagedResultDto PageSearch(PageDto search)
        {
            var userName = search
                .DynamicFilters
                .FirstOrDefault(item => item.Field == "userName")?.Value.ToString();

            var roleName = search
                .DynamicFilters
                .FirstOrDefault(item => item.Field == "roleName")?.Value.ToString();

            var roleId = search
                .DynamicFilters
                .FirstOrDefault(item => item.Field == "roleId")?.Value.ToString();

            var query = Table.GetAll()
                .Select<SysUser, SysRole>()
                .InnerJoin((u, r) => u.RoleId == r.Id)
                .WhereIf(!string.IsNullOrEmpty(userName), (u, r) => u.UserName.Contains(userName))
                .WhereIf(!string.IsNullOrEmpty(roleName), (u, r) => r.Name.Contains(roleName))
                .WhereIf(!string.IsNullOrEmpty(roleId), (u, r) => u.RoleId == Convert.ToInt32(roleId))
                .OrderByDescending((u, r) => u.Id);

            var result = query
                .Count(out var total)
                .Page(search.PageIndex, search.PageMax)
                .ToList((u, r) => new SysUserDTO
                {
                    Id = u.Id,
                    Password = u.Password,
                    RoleId = r.Id,
                    RoleName = r.Name,
                    UserName = u.UserName
                });
            return new PagedResultDto(total, result);
        }

        public AjaxResponse<object> ResetPassword(ResetPasswordRequest request)
        {
            var user = Table.FirstOrDefault(item => item.Id == request.UserId);
            if (user == null)
                throw new Exception("用户不存在");

            user.Password = "123456".Get32MD5One();

            user = Repository.Update(user);

            return user == null
                ? new AjaxResponse<object>("系统错误,修改密码失败", 500)
                : new AjaxResponse<object>("重置密码成功");
        }

        public AjaxResponse<object> UpdatePersonalInfo(UpdatePersonalInfoRequest request)
        {
            var entity = Table.FirstOrDefault(request.UserId);
            if (entity == null)
                throw new Exception("用户不存在");
            if (!string.IsNullOrEmpty(request.Password))
                entity.Password = request.Password.Get32MD5One();

            entity.UserName = request.UserName;
            entity.Icon = request.Icon;

            entity = Repository.Update(entity);

            return entity == null ? new AjaxResponse<object>("系统错误", 500) : new AjaxResponse<object>(entity, "更新成功");
        }

        public override void Delete(long id)
        {
            var user = FirstOrDefault(id);
            if (id == 1 || user.UserName == "admin")
                throw new Exception("admin管理员不允许被删除");

            base.Delete(id);
        }

        public SysUserService(IObjectMapper objectMapper, IFreeSqlRepository<SysUser> repository) : base(objectMapper, repository)
        {
        }
    }
}
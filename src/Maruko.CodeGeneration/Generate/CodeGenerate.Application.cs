using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Maruko.CodeGeneration.Options;

namespace Maruko.CodeGeneration.Generate
{
    public partial class CodeGenerate
    {
        public static void GenerateApplication(List<Dictionary<string, string>> list, bool ifExsitedCovered = false)
        {
            //todo 动态生成各种实体的仓储
            list.ForEach(item =>
            {
                //todo 生成IService
                var path = CodeGenerateOption.Disk + $"\\GenerateCode\\Application\\{item["FileName"]}";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                var fullPath = $"{path}\\I{item["ClassName"]}Service.cs";
                if (File.Exists(fullPath) && !ifExsitedCovered)
                    return;

                var content = @"
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================


using Maruko.Dependency;
using {solution}.Application.{fileName}.DTO.{domainName};
using {solution}.Domain.{fileName};

namespace {solution}.Application.{fileName}
{
    public interface I{domainName}Service : I{solutionSuffix}CrudAppService<{domainName}, {domainName}Dto, Search{domainName}Dto>, IDependencyTransient
    {
    }
}";
                content = content.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                    .Replace("{domainName}", item["ClassName"])
                    .Replace("{solution}", CodeGenerateOption.SolutionName)
                    .Replace("{fileName}", item["FileName"])
                    .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());
                WriteAndSave(fullPath, content);

                //todo 生成Service
                var impPath = $"{CodeGenerateOption.Disk}" + $"\\GenerateCode\\Application\\{item["FileName"]}\\Imp";
                if (File.Exists(impPath) && !ifExsitedCovered)
                    return;
                if (!Directory.Exists(impPath)) Directory.CreateDirectory(impPath);
                var servicePath = $"{impPath}\\{item["ClassName"]}Service.cs";
                var serviceContent =
                    @"//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using Maruko.ObjectMapping;
using {solution}.Application.{fileName}.DTO.{domainName};
using {solution}.Domain.{fileName};
using {solution}.Domain.{fileName}.IRepos;

namespace {solution}.Application.{fileName}.Imp
{
    public class {domainName}Service : {solutionSuffix}CrudAppService<{domainName}, {domainName}Dto, Search{domainName}Dto>, I{domainName}Service
    {
        public {domainName}Service(IObjectMapper objectMapper, I{domainName}Repos repository) : base(objectMapper, repository)
        {
        }
    }
}";
                serviceContent = serviceContent.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                    .Replace("{domainName}", item["ClassName"])
                    .Replace("{solution}", CodeGenerateOption.SolutionName)
                    .Replace("{fileName}", item["FileName"])
                    .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());

                WriteAndSave(servicePath, serviceContent);

                //todo 生成dto
                var dtoPath = CodeGenerateOption.Disk +
                              $"\\GenerateCode\\Application\\{item["FileName"]}\\DTO\\{item["ClassName"]}";
                if (File.Exists(dtoPath) && !ifExsitedCovered)
                    return;
                if (!Directory.Exists(dtoPath)) Directory.CreateDirectory(dtoPath);

                var dtoModelPath = $"{dtoPath}\\{item["ClassName"]}Dto.cs";
                var searchModelDtoPath = $"{dtoPath}\\Search{item["ClassName"]}Dto.cs";

                var dtoContent = @"//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using Maruko.Application.Servers.Dto;
using Maruko.AutoMapper.AutoMapper;

namespace {solution}.Application.{fileName}.DTO.{domainName}
{
    [AutoMap(typeof(Domain.{fileName}.{domainName}))]
    public class {domainName}Dto : EntityDto
    {
    }
}";
                dtoContent = dtoContent
                    .Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                    .Replace("{domainName}", item["ClassName"])
                    .Replace("{solution}", CodeGenerateOption.SolutionName)
                    .Replace("{fileName}", item["FileName"]);

                WriteAndSave(dtoModelPath, dtoContent);
                var searchConetnDto =
                    @"//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using Maruko.Application.Servers.Dto;
namespace {solution}.Application.{fileName}.DTO.{domainName}
{
    public class Search{domainName}Dto : PageDto
    {
    }
}";
                searchConetnDto = searchConetnDto
                    .Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                    .Replace("{domainName}", item["ClassName"])
                    .Replace("{solution}", CodeGenerateOption.SolutionName)
                    .Replace("{fileName}", item["FileName"]);
                WriteAndSave(searchModelDtoPath, searchConetnDto);
            });

            //todo 生成applicationModule
            var modulePath = CodeGenerateOption.Disk +
                             "\\GenerateCode\\Application\\{solutionSuffix}ApplicationModule.cs";
            modulePath = modulePath
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());
            var moduleContent = @"//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using Maruko.Modules;

namespace {solution}.Application
{
    public class {solutionSuffix}ApplicationModule : MarukoModule
    {
        public override double Order { get; set; } = 9;
    }
}";
            moduleContent = moduleContent.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                .Replace("{solution}", CodeGenerateOption.SolutionName)
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());

            WriteAndSave(modulePath, moduleContent);

            //todo 生成ICurdservice
            var icurdPath = CodeGenerateOption.Disk + "\\GenerateCode\\Application\\I{solutionSuffix}CrudAppService.cs";
            icurdPath = icurdPath
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());
            var icurdContent = @"
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using Maruko.Application.Servers;
using Maruko.Application.Servers.Dto;
using Maruko.AspNetMvc.Service;
using Maruko.Domain.Entities;

namespace {solution}.Application
{
    public interface I{solutionSuffix}CrudAppService<TEntity, TEntityDto, TSearch>
        : ICurdAppService<TEntity, TEntityDto, TEntityDto, TEntityDto>
        where TEntity : class, IEntity<int>
        where TEntityDto : EntityDto<int>
        where TSearch : PageDto
    {
            /// <summary>
            /// 分页查询
            /// </summary>
            /// <param name=""search""></param>
            /// <returns></returns>
            ApiReponse<object> PageSearch(TSearch search);

            /// <summary>
            /// 添加或修改
            /// </summary>
            /// <param name=""model""></param>
            /// <returns></returns>
            ApiReponse<object> CreateOrEdit(TEntityDto model);
    }
}";
            icurdContent = icurdContent.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                .Replace("{solution}", CodeGenerateOption.SolutionName)
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());

            WriteAndSave(icurdPath, icurdContent);

            //todo 生成curd实现
            var curdPath = CodeGenerateOption.Disk + "\\GenerateCode\\Application\\{solutionSuffix}CrudAppService.cs";
            curdPath = curdPath
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());
            var curdContent = CurdContent();
            curdContent = curdContent.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                .Replace("{solution}", CodeGenerateOption.SolutionName)
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());

            WriteAndSave(curdPath, curdContent);
        }

        private static string CurdContent()
        {
            return @"
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Maruko.Application.Servers;
using Maruko.Application.Servers.Dto;
using Maruko.AspNetMvc.Service;
using Maruko.AutoMapper.AutoMapper;
using Maruko.Domain.Entities;
using Maruko.Domain.Repositories;
using Maruko.ObjectMapping;

namespace {solution}.Application
{
    public class {solutionSuffix}CrudAppService<TEntity, TEntityDto, TSearch>
        : CurdAppService<TEntity, TEntityDto, TEntityDto, TEntityDto>,
            IBabyCrudAppService<TEntity, TEntityDto, TSearch>
        where TEntity : class, IEntity<int>
        where TEntityDto : EntityDto<int>
        where TSearch : PageDto
    {
        public {solutionSuffix}CrudAppService(IObjectMapper objectMapper, IRepository<TEntity> repository) : base(objectMapper,
            repository)
        {
        }

        public virtual ApiReponse<object> PageSearch(TSearch search)
        {
            if (SelectorFiled() == null)
            {
                var datas = Repository.PageSearch(out var total, SearchFilter(search), OrderFilter(),
                    search.PageIndex, search.PageSize);
                return new ApiReponse<object>(ConvertToEntities(datas).DataToDictionary(total));
            }

            var result = Repository.PageSearchSelector(out var totalPage, SelectorFiled(), SearchFilter(search),
                OrderFilter(),
                search.PageIndex, search.PageSize);
            return new ApiReponse<object>(ConvertDatas(result).DataToDictionary(totalPage));
        }

        public virtual ApiReponse<object> CreateOrEdit(TEntityDto model)
        {
            TEntity data = null;
            if (model.Id == 0)
            {
                data = Repository.Insert(MapToEntity(model));
            }
            else
            {
                data = Repository.SingleOrDefault(item => item.Id == model.Id);
                data = model.MapTo(data);
                data = Repository.Update(data);
            }

            return data == null
                ? new ApiReponse<object>(""系统异常，操作失败"", ServiceEnum.Failure)
                : new ApiReponse<object>(""操作成功"");
        }

        /// <summary>
        ///     查询条件赛选
        /// </summary>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>> SearchFilter(TSearch search)
        {
            return null;
        }

        /// <summary>
        ///     排序条件筛选
        /// </summary>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, int>> OrderFilter()
        {
            return null;
        }

        /// <summary>
        ///     查询指定字段并返回
        /// </summary>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, dynamic>> SelectorFiled()
        {
            return null;
        }

        /// <summary>
        ///     当重写Selector()之后需要自定义返回时需要重写此方法
        ///     默认返回dto集合
        /// </summary>
        /// <param name=""entities""></param>
        /// <returns></returns>
        protected virtual object ConvertDatas(List<dynamic> entities)
        {
            var list = new List<TEntityDto>();
            entities.ForEach(item => { list.Add(MapToEntityDto(item)); });
            return list;
        }

        /// <summary>
        ///     当不重写Selector()方法时返重写此方法返回自定义数据对象
        ///     默认返回dto集合
        /// </summary>
        /// <param name=""entities""></param>
        /// <returns></returns>
        protected virtual object ConvertToEntities(List<TEntity> entities)
        {
            return entities.MapTo<List<TEntity>>();
        }
    }
}";
        }
    }
}
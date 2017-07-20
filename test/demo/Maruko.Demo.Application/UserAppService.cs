using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Demo.Application.Dto;
using Maruko.Demo.Core;
using Maruko.Domain.Repositories;

namespace Maruko.Demo.Application
{
    public class UserAppService:DemoBaseCurdApp<User,CreateUserDto,UpdateUserDto>,IUserAppService
    {
        public UserAppService(IRepository<User, long> repository) : base(repository)
        {
        }
    }
}

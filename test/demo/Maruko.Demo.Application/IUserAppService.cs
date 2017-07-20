using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Application.Servers;
using Maruko.Demo.Application.Dto;
using Maruko.Demo.Core;
using Maruko.Dependency;

namespace Maruko.Demo.Application
{
    public interface IUserAppService : ICurdAppService<User, long, CreateUserDto, UpdateUserDto>, IDependencyTransient
    {
    }
}

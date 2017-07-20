using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Application.Servers.Dto;
using Maruko.AutoMapper.AutoMapper;
using Maruko.Demo.Core;

namespace Maruko.Demo.Application.Dto
{
    [AutoMap(typeof(User))]
    public class CreateUserDto
    {
    }

    [AutoMap(typeof(User))]
    public class UpdateUserDto : EntityDto
    {

    }
}

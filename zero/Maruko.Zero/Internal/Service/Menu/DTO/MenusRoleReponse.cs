using System.Collections.Generic;
using Maruko.Core.Application.Servers.Dto;

namespace Maruko.Zero
{
    public class MenusRoleResponse : EntityDto
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public string Key { get; set; }
        public List<MenusRoleResponse> Children { get; set; } = new List<MenusRoleResponse>();
    }
}
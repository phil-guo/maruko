using System.Collections.Generic;

namespace Maruko.Zero
{
    public class RoleMenuResponse
    {
        public List<string> MenuIds { get; set; } = new List<string>();
        public List<MenuModel> List { get; set; } = new List<MenuModel>();
    }

    public class MenuModel
    {
        public string Id { get; set; }
        public string Lable { get; set; }
        public List<MenuModel> Children { get; set; } = new List<MenuModel>();
    }
}
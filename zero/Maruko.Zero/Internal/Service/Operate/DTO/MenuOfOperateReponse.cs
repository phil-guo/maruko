using System.Collections.Generic;

namespace Maruko.Zero
{
    public class MenuOfOperateResponse
    {
        public List<string> Datas { get; set; } = new List<string>();
    }

    public class GetMenuOfOperateByRoleResponse
    {
        public List<object> Datas { get; set; } = new List<object>();
    }
}
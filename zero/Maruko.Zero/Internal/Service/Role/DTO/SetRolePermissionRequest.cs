using System.Collections.Generic;

namespace Maruko.Zero
{
    public class SetRolePermissionRequest
    {
        public int RoleId { get; set; }
        public List<string> MenuIds { get; set; } = new List<string>();
    }
}
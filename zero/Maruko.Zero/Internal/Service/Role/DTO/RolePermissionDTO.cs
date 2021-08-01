using System.Collections.Generic;

namespace Maruko.Zero
{
    public class RolePermissionDTO
    {
        public int MenuId { get; set; }

        public List<int> Operates { get; set; } = new List<int>();
    }
}
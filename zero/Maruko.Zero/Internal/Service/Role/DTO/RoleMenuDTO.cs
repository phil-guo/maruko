using System.Collections.Generic;

namespace Maruko.Zero
{
    public class RoleMenuDTO
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

        public string Path { get; set; }
        public string Key { get; set; }

        public List<RoleMenuDTO> Children { get; set; } = new List<RoleMenuDTO>();

        public string Operates { get; set; }
    }
}
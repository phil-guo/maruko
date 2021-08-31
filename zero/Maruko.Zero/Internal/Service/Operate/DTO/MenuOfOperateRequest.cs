namespace Maruko.Zero
{
    public class MenuOfOperateRequest
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
    }

    public class GetMenuOfOperateByRoleRequest
    {
        public int RoleId { get; set; }
        public string Key { get; set; }
    }
}
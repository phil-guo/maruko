namespace Maruko.Core.Domain.Entities
{
    /// <summary>
    ///     软删除接口
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDelete { get; set; }
    }
}
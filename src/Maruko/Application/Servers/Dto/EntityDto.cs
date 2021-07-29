namespace Maruko.Core.Application.Servers.Dto
{
    /// <summary>
    /// 抽象的实体数据传输对象
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    public abstract class EntityDto<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }

    /// <summary>
    /// 抽象的主键是{long}类型的实体数据传输对象
    /// </summary>
    public abstract class EntityDto : EntityDto<long>
    {
    }
}

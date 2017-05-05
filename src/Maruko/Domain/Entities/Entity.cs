namespace Maruko.Domain.Entities
{
    /// <summary>
    ///    主键 <see cref="Entity{TPrimaryKey}" /> long 类型的主键(<see cref="long" />).     
    /// </summary>
    public abstract class Entity : Entity<long>
    {
        
    }

    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}
namespace BP.Infrastructure.Domain
{
    public interface IRepository<AggregateType, IdType>
        : IReadOnlyRepository<AggregateType, IdType> where AggregateType
        : IAggregateRoot
    {
        AggregateType Update(AggregateType aggregate);
        AggregateType Insert(AggregateType aggregate);
        void Delete(AggregateType aggregate);
    }
}

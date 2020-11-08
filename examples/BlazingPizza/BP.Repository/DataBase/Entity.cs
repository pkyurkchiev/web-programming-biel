namespace BP.Repository.DataBase
{
    public abstract class Entity<IdType>
    {
        public IdType Id { get; set; }
    }
}

namespace Core.Common.Entities
{
    public interface IEntityOfContainer
    {
        public void OnRegistered(EntityComponent container);
    }
}
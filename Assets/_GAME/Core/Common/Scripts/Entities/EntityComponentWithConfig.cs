namespace Core.Common.Entities
{
    public class EntityComponentWithConfig<T> : EntityComponent where T : class
    {
        public T Config => Entity.Config?.GetComponent<T>();
    }
}
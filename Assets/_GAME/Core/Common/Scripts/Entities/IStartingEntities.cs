namespace Core.Common.Entities
{
    public interface IStartingEntities<T> where T : EntityConfigWithPrefab
    {
        public T[] StartingEntities { get; }
    }
}
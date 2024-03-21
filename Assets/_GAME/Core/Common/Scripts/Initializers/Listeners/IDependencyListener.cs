namespace Core.Common.Initializers.Listeners
{
    public interface IDependencyListener<in T>
    {
        public void InjectDependency(T value);
    }
}
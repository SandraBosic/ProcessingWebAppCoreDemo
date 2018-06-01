namespace Core.Interfaces.Configuration
{
    public interface IConfigurationSection<out T>
    {
        T Configuration { get; }
    }
}

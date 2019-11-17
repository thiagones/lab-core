namespace lab.infrastructure.data.Settings.Interfaces
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; }

        string DatabaseName { get; }
    }
}
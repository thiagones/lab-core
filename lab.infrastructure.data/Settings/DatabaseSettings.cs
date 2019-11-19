using lab.infrastructure.data.Settings.Interfaces;

namespace lab.infrastructure.data.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public DatabaseSettings(
            string connectionString, 
            string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        public string ConnectionString { get; }

        public string DatabaseName { get; }
    }
}
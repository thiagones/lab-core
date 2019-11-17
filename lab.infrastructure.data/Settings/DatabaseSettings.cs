using lab.infrastructure.data.Settings.Interfaces;

namespace lab.infrastructure.data.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public DatabaseSettings(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            //"mongodb+srv://thiagoborges:ameba200%21%40%23@cluster0-qgjwx.mongodb.net/test?retryWrites=true&w=majority";
            //configuration.GetSection("DatabaseSettings:ConnectionString").Value;
            DatabaseName = databaseName;
            //"lab-database";
            //configuration.GetSection("DatabaseSettings:DatabaseName").Value;
        }

        public string ConnectionString { get; }

        public string DatabaseName { get; }
    }
}
namespace CLogManagement.Configuration
{
    public class CfgInformation
    {
        public CfgInformation(string databaseName, string connectionString)
        {
            DatabaseName = databaseName;
            ConnectionString = connectionString;
        }

        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}

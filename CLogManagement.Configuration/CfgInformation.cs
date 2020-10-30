namespace CLogManagement.Configuration
{
    public class CfgInformation
    {
        public CfgInformation(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}

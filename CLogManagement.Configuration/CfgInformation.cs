namespace CLogManagement.Configuration
{
    public class CfgInformation
    {
        public CfgInformation(string databaseFile)
        {
            DatabaseFile = databaseFile;
        }

        public string DatabaseFile { get; set; }
    }
}

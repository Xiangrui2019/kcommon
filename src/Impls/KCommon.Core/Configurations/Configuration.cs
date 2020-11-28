namespace KCommon.Core.Configurations
{
    public class Configuration
    {
        private static Configuration Instance { get; set; }

        private Configuration() { }

        public static Configuration Create()
        {
            Instance = new Configuration();
            return Instance;
        }
        
        
    }
}
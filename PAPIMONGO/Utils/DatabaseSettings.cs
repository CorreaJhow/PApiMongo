namespace PAPIMONGO.Utils
{
    public class DataBaseSettings : IDatabaseSettings
    {
        public string ClientCollectionName { get; set; }
        public string AddressCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

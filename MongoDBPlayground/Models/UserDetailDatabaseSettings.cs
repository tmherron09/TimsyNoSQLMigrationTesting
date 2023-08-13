namespace MongoDBPlayground.Models
{
    public class UserDetailDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserDetailCollectionName { get; set; } = null!;
    }
}

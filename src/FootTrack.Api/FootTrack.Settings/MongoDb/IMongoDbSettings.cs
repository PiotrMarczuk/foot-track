namespace FootTrack.Settings.MongoDb
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        
        string ConnectionString { get; set; }
    }
}
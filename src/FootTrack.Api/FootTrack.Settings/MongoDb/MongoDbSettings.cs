﻿namespace FootTrack.Settings.MongoDb
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }

        public string ConnectionString { get; set; }
    }
}

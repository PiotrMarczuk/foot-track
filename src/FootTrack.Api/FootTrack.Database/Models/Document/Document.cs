using System;
using MongoDB.Bson;

namespace FootTrack.Database.Models.Document
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}

using System;
using MongoDB.Bson;

namespace FootTrack.Api.Models.Document
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}

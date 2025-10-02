using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Domain.Model;

public class Owner
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Address")]
    public string Address { get; set; }

    [BsonElement("Photo")]
    public string Photo { get; set; }

    [BsonElement("Birthday")]
    public DateTime Birthday { get; set; }
}

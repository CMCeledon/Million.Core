using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Domain.Model;

public class PropertyImage
{
    // No necesita [BsonId] ya que está incrustada
    [BsonElement("file")]
    public string File { get; set; }

    [BsonElement("Enabled")]
    public bool Enabled { get; set; }
}
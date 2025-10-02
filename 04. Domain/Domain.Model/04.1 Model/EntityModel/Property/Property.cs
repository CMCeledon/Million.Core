using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Domain.Model;
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // Representa el _id de MongoDB

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        // Usamos Decimal para mapear a BSON Decimal128
        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonElement("CodeInternal")]
        public string CodeInternal { get; set; }

        [BsonElement("Year")]
        public int Year { get; set; }

        // Referencia al Owner: se almacena como un ObjectId en Mongo, mapeado a string
        [BsonElement("IdOwner")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdOwner { get; set; }

        [BsonElement("Images")]
        public List<PropertyImage> Images { get; set; } = new List<PropertyImage>();

        [BsonElement("Traces")]
        public List<PropertyTrace> Traces { get; set; } = new List<PropertyTrace>();
    }

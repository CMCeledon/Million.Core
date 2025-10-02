using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Domain.Model;

public class PropertyTrace
{
    [BsonElement("DateSale")]
    public DateTime DateSale { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    // Usamos Decimal para mapear a BSON Decimal128 (si el esquema es Decimal128)
    // Nota: Si usaste 'double' en el esquema de Mongo, usa 'double' o 'decimal' aquí.
    [BsonElement("Value")]
    public decimal Value { get; set; }

    [BsonElement("Tax")]
    public decimal Tax { get; set; }
}
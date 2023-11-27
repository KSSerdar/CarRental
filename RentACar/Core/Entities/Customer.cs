using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Customer
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CustomerID { get; set; }
        [BsonElement("customer_identity"), BsonRepresentation(BsonType.String)]
        public string IdentityNumber { get; set; }
        [BsonElement("customer_gsm"), BsonRepresentation(BsonType.String)]
        public string GsmNumber { get; set; }
        [BsonElement("customer_name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        [BsonElement("customer_surname"), BsonRepresentation(BsonType.String)]
        public string SurName { get; set; }
        [BsonElement("customer_birthyear"), BsonRepresentation(BsonType.String)]

        public string BirthYear { get; set; }
    }
}

using Core.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Car
    {
        [BsonId,BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CarID { get; set; }
        [BsonElement("car_brand"),BsonRepresentation(BsonType.String)]
        public string CarBrand { get; set; }
        [BsonElement("car_model"), BsonRepresentation(BsonType.String)]
        public string CarModel { get; set; }
        [BsonElement("car_licence"),BsonRepresentation(BsonType.String)]
        public string LicencePlate { get; set; }
        [BsonElement("car_rentprice"), BsonRepresentation(BsonType.Decimal128)]
        public decimal DailyPrice { get; set; }

        public CarData Situation { get; set; }
    }
}

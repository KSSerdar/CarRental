using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Contract
    {
        [BsonId,BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CompromiseID { get; set; }
        [BsonElement("compromise-begindate"),BsonRepresentation(BsonType.DateTime)]
        public DateTime BeginDate { get; set; }= DateTime.Now;  
        [BsonElement("car_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CarID { get; set; }
        [BsonElement("car_model"), BsonRepresentation(BsonType.String)]
        public string CarModel { get; set; }
        [BsonElement("car_licenec"), BsonRepresentation(BsonType.String)]
        public string CarLicence { get; set; }
        [BsonElement("car_price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal CarDailyPrice { get; set; }
        [BsonElement("customer_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CustomerID { get; set; }
        [BsonElement("customer_name"),BsonRepresentation(BsonType.String)]
        public string CustomerName { get; set; }
        [BsonElement("customer_identity"), BsonRepresentation(BsonType.String)]

        public string CustomerIdentityNumber { get; set; }
        [BsonElement("customer_gsm"), BsonRepresentation(BsonType.String)]

        public string CustomerGsmNumber { get; set; }

    }
}

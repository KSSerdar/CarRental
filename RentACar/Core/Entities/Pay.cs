using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Pay
    {
        [BsonId,BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
        public ObjectId PayID { get; set; }
        [BsonElement("customer_name"), BsonRepresentation(BsonType.String)]
        public string CustomerName { get; set; }
        [BsonElement("customer_gsm"), BsonRepresentation(BsonType.String)]
        public string CustomerGsm { get; set; }
        [BsonElement("customer_identity"), BsonRepresentation(BsonType.String)]
        public string CustomerIdentity { get; set; }
        [BsonElement("car_model"), BsonRepresentation(BsonType.String)]
        public string CarModel { get; set; }
        [BsonElement("car_licence"), BsonRepresentation(BsonType.String)]
        public string CarLicencePlate { get; set; }
        public ObjectId CarID { get; set; }
        public ObjectId CustomerID { get; set; }
        public ObjectId ContractID { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime DeliveryTime { get; set; }= DateTime.Now;
        [BsonElement("pay_paidprice"),BsonRepresentation(BsonType.Decimal128)]
        public decimal PaidPrice { get; set; }
    }
}

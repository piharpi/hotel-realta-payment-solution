namespace HotelRealtaPayment.Domain.Entities
{
    public class Bank
    {
        public int bank_entity_id { get; set; }
        public string bank_code { get; set;}
        public string bank_name { get; set;}
        public DateTime bank_modified_date { get; set;} = DateTime.Now;
    }
}

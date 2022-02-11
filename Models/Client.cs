namespace Models
{
    public class Client
    {
        public int ClientId { get; set; }        
        public string NIF { get; set; }
        public string Name { get; set; }          
        public int? Country_Id { get; set; }
        public Country Country { get; set; }
        public List<Order> Orders { get; set; } // Relación de uno a muchos.
    }
}

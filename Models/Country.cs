namespace Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public List<Client> Clients { get; set;}
    }
}

namespace Models.Helpers
{
    public abstract class Audit
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

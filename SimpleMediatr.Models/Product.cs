namespace SimpleMediatr.Models
{
    public sealed class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
    }
}

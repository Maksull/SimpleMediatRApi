namespace SimpleMediatr.Contracts.Controllers.Products
{
    public sealed class UpdateProductDto 
    {
        public long ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
    }
}

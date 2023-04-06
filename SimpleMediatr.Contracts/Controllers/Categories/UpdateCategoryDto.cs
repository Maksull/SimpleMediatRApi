namespace SimpleMediatr.Contracts.Controllers.Categories 
{
    public sealed class UpdateCategoryDto 
    {
        public long CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

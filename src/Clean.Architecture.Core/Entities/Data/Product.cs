namespace Clean.Architecture.Core.Entities.Data;

public class Product
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Description { get; init; }

    public decimal? Price { get; init; }
}

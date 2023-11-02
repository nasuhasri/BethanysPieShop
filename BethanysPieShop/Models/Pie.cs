namespace BethanysPieShop.Models;

public class Pie
{
    public int PieId { get; set; }
    // set name as empty
    public string Name { get; set; } = string.Empty;
    // ? = nullable = do not require a value = can be null in database
    public string? ShortDescription { get; set; }
    public string? LongDescription { get; set; }
    public string? AllergyInformation { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageThumbnailUrl { get; set; }
    public bool IsPieOfTheWeek { get; set; }
    public bool InStock { get; set; }
    public int CategoryId { get; set; }
    // default! - null forgiving operator
    // used in combination with nullable to declare that category shouldnt be null
    public Category Category { get; set; } = default!;
}

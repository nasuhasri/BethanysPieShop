using BethanysPieShop.Models;

namespace BethanysPieShop.ViewModels;

// a model for a pie list screen
public class PieListViewModel
{
    public IEnumerable<Pie> Pies { get; }
    public string? CurrentCategory { get; }
    
    public PieListViewModel(IEnumerable<Pie> pies, string? currentCategory)
    {
        Pies = pies;
        CurrentCategory = currentCategory;
    }
}

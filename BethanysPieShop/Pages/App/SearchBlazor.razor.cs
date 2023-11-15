// this is the code-behind-based approach
// to make it work, name must be same (filename, class name)
// class need to be partial in order to be associated with blazor component itself
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShop.Pages.App
{
    public partial class SearchBlazor
    {
        public string SearchText = "";
        public List<Pie> FilteredPies { get; set; } = new List<Pie>();

        // in blazor component, we dont use constructor injection, we used inject attribute
        // it search in the registered service for a registration for IPieRepository
        [Inject]
        public IPieRepository? PieRepository { get; set; }

        private void Search()
        {
            FilteredPies.Clear();
            if (PieRepository is not null)
            {
                // triggered the search if search input is more than 3
                if (SearchText.Length >= 3)
                    FilteredPies = PieRepository.SearchPies(SearchText).ToList();
            }
        }
    }
}

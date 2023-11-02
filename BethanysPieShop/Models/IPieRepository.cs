namespace BethanysPieShop.Models;

// going to be used to wrap all the data interaction logic
// make sure consuming classes only talk to the repository
public interface IPieRepository
{
    IEnumerable<Pie> AllPies { get; }
    IEnumerable<Pie> PiesOfTheWeek { get; }
    Pie? GetPieById(int pieId);
}

using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers;

public class PieController : Controller
{
    //public IActionResult Index()
    //{
    //    return View();
    //}

    // local fields
    private readonly IPieRepository _pieRepository;
    private readonly ICategoryRepository _categoryRepository;

    // using constructor injection (the Mock one will be injected)
    // got the concrete instances after the constructor has run
    public PieController (IPieRepository pieRepository, ICategoryRepository categoryRepository)
    { 
        _pieRepository = pieRepository;
        _categoryRepository = categoryRepository;
    }

    public IActionResult List()
    {
        // ViewBag is dynamic - can add whatever property we want (shareable between views and controllers)
        // view able to access the properties inside the ViewBag
        ViewBag.CurrentCategory = "Cheese cakes";

        // the view is being passed as an IEnumerable in pies
        return View(_pieRepository.AllPies);
    }
}

using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
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

    // using constructor injection (the real one will be injected)
    // got the concrete instances after the constructor has run
    public PieController (IPieRepository pieRepository, ICategoryRepository categoryRepository)
    { 
        _pieRepository = pieRepository;
        _categoryRepository = categoryRepository;
    }

    public IActionResult List()
    {
        /*
         * ViewBag is dynamic - can add whatever property we want (shareable between views and controllers)
         * view able to access the properties inside the ViewBag
         * 2 ways passing view; ViewBag and View method
        */
        //ViewBag.CurrentCategory = "Cheese cakes";

        // the view is being passed as an IEnumerable in pies
        //return View(_pieRepository.AllPies);

        PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies, "All Pies");

        return View(pieListViewModel);
    }

    public IActionResult Details(int id)
    {
        var pie = _pieRepository.GetPieById(id);

        if (pie == null) { 
            return NotFound();
        }

        return View(pie);
    }
}

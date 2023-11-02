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
        return View(_pieRepository.AllPies);
    }
}

using BethanysPieShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Api
{
    // ControllerBase is base class for the Controller class
    // can also inherit from Controller class

    // used to indicate that we are using a specific route to route request to this control
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository _pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        // api/search will return a list of pies in json format
        [HttpGet] // this is attributed-based routing
        public IActionResult GetAll()
        {
            var allPies = _pieRepository.AllPies;

            return Ok(allPies); // return data and status code
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!_pieRepository.AllPies.Any(p => p.PieId == id))
            {
                return NotFound();
            }

            return Ok(_pieRepository.AllPies.Where(p => p.PieId == id));
        }

        //[FromBody] include search query value in the body of request
        [HttpPost]
        public IActionResult SearchPies([FromBody] string searchQuery)
        {
            IEnumerable<Pie> pies = new List<Pie>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                pies = _pieRepository.SearchPies(searchQuery);
            }

            return new JsonResult(pies);
        }
    }
}

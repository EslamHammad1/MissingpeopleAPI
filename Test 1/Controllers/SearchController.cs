using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly MissingPersonEntity _context;
        public SearchController(MissingPersonEntity context)
        {
            _context = context;
        }
        [HttpPost("SearchByName")]
        public IActionResult SearchByName([FromQuery] SearchNameDTO searchDTO)
        {
            IQueryable<LostPerson> query = _context.lostPersons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchDTO.Name))
            {
                query = query.Where(p => p.Name.Contains(searchDTO.Name));
            }

         

            var results = query.ToList();

            return Ok(results);
        }
        [HttpPost("SearchByCity")]
        public IActionResult SearchByCity([FromQuery] SearchCityDTO searchDTO)
        {
            IQueryable<LostPerson> query = _context.lostPersons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchDTO.Address_City))
            {
                query = query.Where(p => p.Address_City.Contains(searchDTO.Address_City));
            }

            var results = query.ToList();
            

            return Ok(results);
        }
    }




}


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
        [HttpGet("SearchByNameForLost")]
        public IActionResult SearchByNameForLost([FromQuery] SearchNameDTO searchDTO)
        {
            IQueryable<LostPerson> query = _context.lostPersons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchDTO.Name))
            {
                query = query.Where(p => p.Name.StartsWith(searchDTO.Name));
            }

            var results = query.ToList();

            return Ok(results);
        }
        [HttpGet("SearchByCityForLost")]
        public IActionResult SearchByCityForLost([FromQuery] SearchCityDTO searchDTO)
        {
            IQueryable<LostPerson> query = _context.lostPersons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchDTO.Address_City))
            {
                query = query.Where(p => p.Address_City.Contains(searchDTO.Address_City));
            }

            var results = query.ToList();
            

            return Ok(results);
        }
        [HttpGet("SearchByNameForFound")]
        public IActionResult SearchByNameForFound([FromQuery] SearchNameDTO searchDTO)
        {
            IQueryable<FoundPerson> query = _context.foundPersons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchDTO.Name))
            {
                query = query.Where(p => p.Name.StartsWith(searchDTO.Name));
            }

            var results = query.ToList();

            return Ok(results);
        }
        [HttpGet("SearchByCityForFound")]
        public IActionResult SearchByCityForFound([FromQuery] SearchCityDTO searchDTO)
        {
            IQueryable<FoundPerson> query = _context.foundPersons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchDTO.Address_City))
            {
                query = query.Where(p => p.Address_City.Contains(searchDTO.Address_City));
            }

            var results = query.ToList();


            return Ok(results);
        }
    }




}


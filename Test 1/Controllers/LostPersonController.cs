using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LostPersonController : ControllerBase
    {
        private new List<string> allwoedExtentions = new List<string> { ".jpg , .png" }; // new
        private long MaxallwoedImageSize = 5242880; // new
        private readonly MissingPersonEntity _context;
        public LostPersonController(MissingPersonEntity context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllMissingperson()
        {
            List<LostPerson> missList = _context.lostPersons.ToList();
            return Ok(missList);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetbyID(int id)
        {


            var missPrs = await _context.foundPersons.FindAsync(id);
            FoundPersonWithUserDTO fDTO = new FoundPersonWithUserDTO();
            if (missPrs == null)
                return NotFound();
            else
                return Ok(missPrs);
        }

        [HttpPost]
        public async Task<IActionResult> PostLostperson([FromForm] LostPersonWithUserDTO lDTO)

        {
            if (ModelState.IsValid == true)
            {
                if (lDTO.Image.Length > MaxallwoedImageSize)
                    return BadRequest("Max allowed size for image is 5MB! ");
                using var dataStreem = new MemoryStream();
                await lDTO.Image.CopyToAsync(dataStreem);
                var LostPerson = new LostPerson
                {
                    Name = lDTO.Name,
                    Age = lDTO.Age,
                    Gender = lDTO.Gender,
                    Image = dataStreem.ToArray(), // new
                    Note = lDTO.Note,
                    LostCity = lDTO.LostCity,
                    Address_City = lDTO.Address_City,
                    Date = lDTO.Date,
                    PersonWhoLost = lDTO.PersonWhoLost,
                    PhonePersonWhoLost = lDTO.PhonePersonWhoLost,
                };
                await _context.AddAsync(LostPerson);
                _context.SaveChanges();
                return Ok(LostPerson);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id:int}")]
        //    [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var lostPrs = await _context.lostPersons.FindAsync(id);
            if (lostPrs != null)
            {
                try
                {
                    _context.lostPersons.Remove(lostPrs);
                    _context.SaveChanges();
                    return Ok(lostPrs);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
    }
}

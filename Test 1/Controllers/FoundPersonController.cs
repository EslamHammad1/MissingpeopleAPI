using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Test_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FoundPersonController : ControllerBase
    {
        private new List<string> allwoedExtentions = new List<string> { ".jpg , .png" }; // new
        private long MaxallwoedImageSize = 10485760; // new
        private readonly MissingPersonEntity _context;
        public FoundPersonController(MissingPersonEntity context)
        {
            _context = context;
        }
        [HttpGet]
        public async  Task<IActionResult> GetAllFoundPerson()
        {
            List<FoundPerson> FoundList = await _context.foundPersons.OrderBy(i=>i.Name).ToListAsync();
            return Ok(FoundList);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetbyID(int id )
        {


            var foundPrs = await _context.foundPersons.FindAsync(id);
            FoundPersonWithUserDTO fDTO = new FoundPersonWithUserDTO();
            if (foundPrs == null)
                return NotFound();
            else
                return Ok(foundPrs);
        }
        [HttpPost]
        public async Task<IActionResult> PostFoundperson([FromForm] FoundPersonWithUserDTO fDTO)

        {
            if (ModelState.IsValid == true)
            {
                if (fDTO.Image == null)
                    return BadRequest("Image is Required !");
                if (fDTO.Image.Length > MaxallwoedImageSize)
                    return BadRequest("Max allowed size for image is 10 MB! ");
                using var dataStreem = new MemoryStream();
                await fDTO.Image.CopyToAsync(dataStreem);
                var FoundPerson = new FoundPerson
                {
                    Name = fDTO.Name,
                    Age = fDTO.Age,
                    Gender = fDTO.Gender,
                    Image = dataStreem.ToArray(), // new
                    Note = fDTO.Note,
                    FoundCity = fDTO.FoundCity,
                    Address_City = fDTO.Address_City,
                    Date = fDTO.Date,
                    PersonWhoFoundhim = fDTO.PersonWhoFoundhim,
                    PhonePersonWhoFoundhim = fDTO.PhonePersonWhoFoundhim,
                };
                await _context.AddAsync(FoundPerson);
                _context.SaveChanges();
                return Ok(FoundPerson);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
       public async Task<IActionResult> Update(int id, [FromForm] FoundPersonWithUserDTO fNewDTO)
       {
           if (ModelState.IsValid == true)
           {

               FoundPerson? oldPrs =await _context.foundPersons.FindAsync(id);
               if (oldPrs == null)
                    return NotFound($"Not Found{id}");
                if (fNewDTO.Image != null)
                {
                    if (fNewDTO.Image.Length > MaxallwoedImageSize)
                        return BadRequest("Max allowed size for image is 10 MB! ");
                    using var dataStreem = new MemoryStream();
                    await fNewDTO.Image.CopyToAsync(dataStreem);
                    oldPrs.Image = dataStreem.ToArray();
                }

                   oldPrs.Name = fNewDTO.Name;
                   oldPrs.Gender = fNewDTO.Gender;
                   oldPrs.Address_City = fNewDTO.Address_City;
                   oldPrs.Age = fNewDTO.Age;
                   oldPrs.Date = fNewDTO.Date;
                   oldPrs.FoundCity = fNewDTO.FoundCity;
                   oldPrs.PersonWhoFoundhim = fNewDTO.PersonWhoFoundhim;
                   oldPrs.PhonePersonWhoFoundhim = fNewDTO.PhonePersonWhoFoundhim;
                   _context.SaveChanges();
                   return Ok(fNewDTO);
           }
           return BadRequest(ModelState);
       }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var foundPrs = await _context.foundPersons.FindAsync(id);
            if (foundPrs != null)
            {
                try
                {
                    _context.foundPersons.Remove(foundPrs);
                    _context.SaveChanges();
                    return Ok(foundPrs);
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

using Microsoft.AspNetCore.Mvc;
using VVCRUD_IT_CDN_API_NET8.Models.Dtos.ProfessionalDtos;
using VVCRUD_IT_CDN_API_NET8.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VVCRUD_IT_CDN_API_NET8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly IProfessionalService _professionalService;
        public ProfessionalController(IProfessionalService professionalService)
        {
            _professionalService = professionalService;
        }


        [HttpGet]
        [ResponseCache(CacheProfileName = "CacheForSeconds10")]
        public async Task<ActionResult<IEnumerable<ProfessionalViewDto>>> GetAllAsync([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 2)
        {
            // Validate page number and page size
            if (pageNo <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = $"Invalid page number or page size." });
            }

            var professionalViewDtos = await _professionalService.GetAllAsync(pageNo, pageSize);

            if (professionalViewDtos == null || !professionalViewDtos.Any())
            {
                return NotFound(new { message = $"No Professionals found." });
            }

            return Ok(professionalViewDtos.ToList());
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ResponseCache(CacheProfileName = "CacheForSeconds10")]
        public async Task<ActionResult<IEnumerable<ProfessionalViewDto>>> GetByIdAsync(Guid id)
        {
            var ProfessionalViewDto = await _professionalService.GetByIdAsync(id);
            if (ProfessionalViewDto == null)
            {
                return NotFound(new { message = $"No Professional found with Id {id}." });
            }
            return Ok(ProfessionalViewDto);
        }


        [HttpPost]
        public async Task<ActionResult<ProfessionalViewDto>> Post(ProfessionalCreateDto professionalCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var professionalViewDto = await _professionalService.CreateAsync(professionalCreateDto);
                return Ok(professionalViewDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });
            }
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<ProfessionalViewDto>> Put(Guid id, ProfessionalUpdateDto updateProfessionalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var professionalViewDto = await _professionalService.UpdateAsync(id, updateProfessionalDto);
                if (professionalViewDto == null)
                {
                    return NotFound(new { message = $"No Professional found with Id {id}." });
                }
                return Ok(professionalViewDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });
            }
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isSuucess = await _professionalService.DeleteAsync(id);
            if (!isSuucess)
            {
                return NotFound(new { message = $"No Professional found with Id {id}." });
            }
            return Ok(new { message = $"Record Successfully Removed." });
        }
    }
}

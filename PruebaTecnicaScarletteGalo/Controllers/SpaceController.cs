using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaScarletteGalo.DTos;
using PruebaTecnicaScarletteGalo.Services;

namespace PruebaTecnicaScarletteGalo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SpaceController : ControllerBase
    {
        private readonly SpaceService _spaceService;

        public SpaceController(SpaceService spaceService)
        {
            _spaceService = spaceService;
        }

        [HttpGet("available")]
        public IActionResult GetAvailableSpaces([FromQuery] DateTime date, [FromQuery] TimeSpan startTime, [FromQuery] TimeSpan endTime)
        {
            try
            {
                var spaces = _spaceService.GetAvailableSpaces(date, startTime, endTime);
                return Ok(spaces);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update-state/{spaceId}")]
        public IActionResult UpdateSpaceState(Guid spaceId, [FromBody] UpdateSpaceStateRequestDTO model)
        {
            try
            {
                _spaceService.UpdateSpaceState(spaceId, model.NewState);
                return Ok(new { message = "Space state updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

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
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            this._reservationService = reservationService;
        }

        [HttpPost("create")]
        public IActionResult CreateReservation([FromBody] CreateReservationRequestDTO model)
        {
            try
            {
                var reservation = _reservationService.CreateReservation(
                    model.UserId,
                    model.SpaceId,
                    model.Date,
                    model.StartTime,
                    model.EndTime
                );
                return Ok(reservation);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetReservationsByUser(Guid userId)
        {
            var reservations = _reservationService.GetReservationByUser(userId);
            return Ok(reservations);
        }

        [HttpGet("space/{spaceId}/active")]
        public IActionResult GetActiveReservationsBySpace(Guid spaceId)
        {
            var reservations = _reservationService.GetActiveReservationBySpace(spaceId);
            return Ok(reservations);
        }

        [HttpPost("cancel/{reservationId}")]
        public IActionResult CancelReservation(Guid reservationId)
        {
            try
            {
                _reservationService.CancelReservation(reservationId);
                return Ok(new { message = "Reservation canceled successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}

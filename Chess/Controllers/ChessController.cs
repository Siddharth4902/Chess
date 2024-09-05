using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChessController : ControllerBase
    {
        private readonly IChessDao _chessDao;

        public ChessController(IChessDao chessDao)
        {
            _chessDao = chessDao;
        }
        [HttpGet("GetMatches")]
        public async Task<ActionResult<List<Chess>>> GetMatches()
        {
            var matches1 = await _chessDao.GetMatches();
            if (matches1 == null)
            {
                return NotFound();
            }
            return Ok(matches1);
        }

        [HttpGet("GetPerformances")]
        public async Task<ActionResult<List<Performance>>> GetPerformances()
        {
            var performance = await _chessDao.GetPerformances();
            if (performance == null)
            {
                return NotFound();
            }
            return Ok(performance);
        }

        [HttpGet("GetWinners")]
        public async Task<ActionResult<List<Performance>>> GetWinners()
        {
            var winners = await _chessDao.GetWinners();
            if (winners == null)
            {
                return NotFound();
            }
            return Ok(winners);
        }

        [HttpPost("CreateMatch")]
        public async Task<ActionResult<Chess?>> CreateMatch(Chess match)
        {
            if (match != null)
            {
                if (ModelState.IsValid)
                {
                    int res = await _chessDao.InsertMatch(match);
                    if (res > 0)
                    {
                        return Ok();
                    }
                }
                return BadRequest("Failed to add Match");
            }
            else
            {
                return BadRequest("No match Found");
            }
        }

        [HttpGet("GetPlayerByCountry")]
        public async Task<ActionResult<List<Country>>> GetPlayerByCountry(string country)
        {
            List<Country> list = null;
            list = await _chessDao.GetPlayerByCountry(country);
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return NotFound("No Player Found");
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minimal;
using Minimal.Controllers;
namespace Minimal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly DataContext _context;

        public VideoGameController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
        {
            return Ok(await _context.VideoGames.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<VideoGame>>> GetVideoGames(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game == null)
            return NotFound("No game here. :/");
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<List<VideoGames>>> CreateVideoGame(VideoGame game)
        {
            _context.VideoGames.Add(game);
            await _context.SaveChangesAsync();
            return Ok(await _context.VideoGames.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<VideoGame>>> UpdateVideoGame(VideoGame game,int id)
        {
            var dbGame = await _context.VideoGames.FindAsync(id);
            if (dbGame == null)
                return NotFound("No game here.:/");

            dbGame.Name = game.Name;
            dbGame.Developer = game.Developer;
            dbGame.Release = game.Release;
            await _context.SaveChangesAsync();
            return Ok(await _context.VideoGames.ToListAsync());
        }
            
    }

}

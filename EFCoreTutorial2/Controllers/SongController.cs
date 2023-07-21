using EFCoreTutorial2.DataContext;
using EFCoreTutorial2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EFCoreTutorial2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly MusicContext _context;

        //constructor
        public SongController(MusicContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongModel>>> GetSongs()
        {
            return await _context.Songs.Include(g => g.Genre).Include(a => a.Artist).ToListAsync();
        }

        [HttpGet("{id}")]
        public SongModel GetSongModel(int id)
        {
            SongModel song = _context.Songs.Include(a => a.Artist).Include(g => g.Genre).Where(s => s.Id == id).FirstOrDefault();

            return song;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            SongModel song =  await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
           await _context.SaveChangesAsync();
            
            return NoContent();

        }
    }
    
}

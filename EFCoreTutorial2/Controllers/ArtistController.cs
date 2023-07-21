using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCoreTutorial2.DataContext;
using EFCoreTutorial2.Models;

namespace EFCoreTutorial2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly MusicContext _context;

        public ArtistController(MusicContext context)
        {
            _context = context;
        }

        // GET: api/Artist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistModel>>> GetArtists()
        {
          if (_context.Artists == null)
          {
              return NotFound();
          }
            return await _context.Artists.Include(s => s.Songs).ThenInclude(g => g.Genre).ToListAsync();
        }

        // GET: api/Artist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistModel>> GetArtistModel(int id)
        {
          if (_context.Artists == null)
          {
              return NotFound();
          }
            var artistModel = await _context.Artists.Include(s => s.Songs).ThenInclude(g => g.Genre).Where( a => a.Id == id).FirstOrDefaultAsync();

            if (artistModel == null)
            {
                return NotFound();
            }

            return artistModel;
        }

        // PUT: api/Artist/5 UPdates existing data
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtistModel(int id, ArtistModel artistModel)
        {
            if (id != artistModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(artistModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Artist adds an artist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArtistModel>> PostArtistModel(ArtistModel artistModel)
        {
          if (_context.Artists == null)
          {
              return Problem("Entity set 'MusicContext.Artists'  is null.");
          }
            _context.Artists.Add(artistModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtistModel", new { id = artistModel.Id }, artistModel);
        }

        // DELETE: api/Artist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtistModel(int id)
        {
            if (_context.Artists == null)
            {
                return NotFound();
            }
            var artistModel = await _context.Artists.FindAsync(id);
            if (artistModel == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artistModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistModelExists(int id)
        {
            return (_context.Artists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

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
    public class GenreController : ControllerBase
    {
        private readonly MusicContext _context;

        public GenreController(MusicContext context)
        {
            _context = context;
        }

        // GET: api/Genre
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreModel>>> GetGenres()
        {
          if (_context.Genres == null)
          {
              return NotFound();
          }
            return await _context.Genres.ToListAsync();
        }

        // GET: api/Genre/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreModel>> GetGenreModel(int id)
        {
          if (_context.Genres == null)
          {
              return NotFound();
          }
            var genreModel = await _context.Genres.FindAsync(id);

            if (genreModel == null)
            {
                return NotFound();
            }

            return genreModel;
        }

        // PUT: api/Genre/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenreModel(int id, GenreModel genreModel)
        {
            if (id != genreModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(genreModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreModelExists(id))
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

        // POST: api/Genre
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GenreModel>> PostGenreModel(GenreModel genreModel)
        {
          if (_context.Genres == null)
          {
              return Problem("Entity set 'MusicContext.Genres'  is null.");
          }
            _context.Genres.Add(genreModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenreModel", new { id = genreModel.Id }, genreModel);
        }

        // DELETE: api/Genre/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenreModel(int id)
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }
            var genreModel = await _context.Genres.FindAsync(id);
            if (genreModel == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genreModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenreModelExists(int id)
        {
            return (_context.Genres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

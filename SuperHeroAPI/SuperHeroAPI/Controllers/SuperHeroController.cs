using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private static List<SuperHero> heroes = new List<SuperHero>
            {
               new SuperHero
               {
                   Id = 1,
                   Name ="Spider Man",
                   FirstName="Peter",
                   LastName="Parker",
                   Place="New York City"
               },
               new SuperHero
               {
                   Id = 2,
                   Name ="Iron Man",
                   FirstName="Tony",
                   LastName="Stark",
                   Place="Long Island"
               }
            };

        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.Heroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero Not Found.");
            }
            return Ok(hero);
        }


        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Heroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var Dbhero = await _context.Heroes.FindAsync(request.Id);
            if (Dbhero == null)
            {
                return BadRequest("Hero Not Found.");
            }

            Dbhero.Name = request.Name;
            Dbhero.FirstName = request.FirstName;
            Dbhero.LastName = request.LastName;
            Dbhero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.Heroes.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var Dbhero = await _context.Heroes.FindAsync(id);
            if (Dbhero == null)
            {
                return BadRequest("Hero Not Found.");
            }
            _context.Heroes.Remove(Dbhero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Heroes.ToListAsync());
        }
    }
}

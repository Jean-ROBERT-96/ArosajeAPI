using DataContext;
using Entities;
using Entities.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArosajeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnonceController : ControllerBase
    {
        private readonly IRepository<Annonce> _repo;

        public AnnonceController(IRepository<Annonce> repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<Annonce>> Get(Int64 id)
        {
            return Ok(await _repo.Get(id));
        }

        [HttpGet("filter"), AllowAnonymous]
        public async Task<ActionResult<List<Annonce>>> Get([FromQuery] AnnonceFilter filters)
        {
            return Ok(await _repo.Get(filters));
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<List<Annonce>>> GetAll()
        {
            return Ok(await _repo.GetAll());
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Annonce>> Post(Annonce entity)
        {
            return CreatedAtAction("Post", await _repo.Post(entity));
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<Annonce>> Put(Annonce entity)
        {
            var result = await _repo.Put(entity);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(entity);
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<Annonce>> Delete(Annonce entity)
        {
            var result = await _repo.Delete(entity.Id);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(entity);
        }
    }
}

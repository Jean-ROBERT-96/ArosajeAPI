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
    public class UtilisateurController : ControllerBase
    {
        private readonly IRepository<Utilisateur> _repo;

        public UtilisateurController(IRepository<Utilisateur> repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Utilisateur>> Get(Int64 id)
        {
            return Ok(await _repo.Get(id));
        }

        [HttpGet("filter"), Authorize]
        public async Task<ActionResult<List<Utilisateur>>> Get([FromQuery] UtilisateurFilter filters)
        {
            return Ok(await _repo.Get(filters));
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<List<Utilisateur>>> GetAll()
        {
            return Ok(await _repo.GetAll());
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Utilisateur>> Post(Utilisateur entity)
        {
            return CreatedAtAction("Post", await _repo.Post(entity));
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<Utilisateur>> Put(Utilisateur entity)
        {
            var result = await _repo.Put(entity);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(entity);
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<Utilisateur>> Delete(Utilisateur entity)
        {
            var result = await _repo.Delete(entity.Id);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(entity);
        }
    }
}

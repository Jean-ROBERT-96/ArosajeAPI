﻿using DataContext;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArosajeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IRepository<Conversation> _repo;

        public ConversationController(IRepository<Conversation> repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Conversation>> Get(Int64 id)
        {
            return Ok(await _repo.Get(id));
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<List<Conversation>>> GetAll()
        {
            return Ok(await _repo.GetAll());
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Conversation>> Post(Conversation entity)
        {
            return CreatedAtAction("Post", await _repo.Post(entity));
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<Conversation>> Put(Conversation entity)
        {
            var result = await _repo.Put(entity);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(entity);
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<Conversation>> Delete(Conversation entity)
        {
            var result = await _repo.Delete(entity.Id);
            if (result == null)
                return NotFound("L'objet n'a pas été trouvé.");

            return Ok(entity);
        }
    }
}

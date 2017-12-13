using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using pokelist.Core;
using pokelist.Model;

namespace pokelist.Controllers
{
    [Route("api/[controller]")]
    public class PokemonController : Controller
    {
        private readonly IPokemonService _pokemonService;
        private readonly CacheClient _cacheClient;

        public PokemonController(IPokemonService pokemonService, IDistributedCache cache)
        {
            _pokemonService = pokemonService;
            _cacheClient = new CacheClient(cache);
        }

        [HttpGet]
        public async Task<IEnumerable<Pokemon>> Get()
        {
            return await _cacheClient.ReadOrUpdateCache<IEnumerable<Pokemon>>("pokemon_get", _pokemonService.Get);
        }

        // GET api/pokemon/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pokemon = await _cacheClient.ReadOrUpdateCache<Pokemon, int>($"pokemon_get_{id}", _pokemonService.Get, id);
            if (pokemon == null)
            { 
                return NotFound(id); 
            }
            else 
            {
                return Json(pokemon);
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using pokelist.Core;
using pokelist.Model;
using pokelist.Filters;
namespace pokelist.Controllers
{
    [Route("api/[controller]")]
    public class PokemonController : Controller
    {
        private readonly IPokemonService _pokemonService;
        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        [MyCacheFilter]
        public async Task<IEnumerable<Pokemon>> Get()
        {
            return await _pokemonService.Get();
        }

        // GET api/pokemon/5
        [HttpGet("{id}")]
        [MyCacheFilter]
        public async Task<IActionResult> Get(int id)
        {
            var pokemon = await _pokemonService.Get(id);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokedexApi.Services;

namespace PokedexApi.Controllers
{
    [ApiController]
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonService _service;

        public PokemonController(PokemonService service)
        {
            _service = service;
        }

        [HttpGet("listOfNames")]
        public async Task<IActionResult> BuscarNomes(int limit)
        {
            var result = await _service.GetAllPokemon(limit);
            return Ok("Lista de pokémons até o " + limit + ":\n" + string.Join(",\n", result));
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var result = await _service.GetPokemonById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> BuscarPorNome(string name)
        {
            var result = await _service.GetPokemonByName(name);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> BuscarPorTipo(string type)
        {
            var result = await _service.GetPokemonByType(type);
            if (result == null) return NotFound();
            return Ok("Pokémons do tipo " + type + ": \n" + string.Join(",\n", result));
        }
    }
}
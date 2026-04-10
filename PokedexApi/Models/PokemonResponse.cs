using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexApi.Models
{
    public class PokemonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Height { get; set; }
        public List<string> Types { get; set; } = new List<string>();
        public List<string> Abilities { get; set; } = new List<string>();
    }
}
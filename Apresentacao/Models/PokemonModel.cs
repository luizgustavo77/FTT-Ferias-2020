using Apresentacao.Models.PokeAPI;
using System.Collections.Generic;

namespace Apresentacao.Models
{
    public class PokemonModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isShiny { get; set; }
        public Sprites sprites { get; set; }
        public IList<Forms> forms { get; set; }
        public IList<Abilities> abilities { get; set; }
    }
}
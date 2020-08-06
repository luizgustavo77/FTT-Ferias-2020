using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apresentacao.Models.PokeAPI
{
    public class Abilities
    {
        public Ability ability { get; set; }
        public bool is_hidden { get; set; }
        public string slot { get; set; }
    }
}
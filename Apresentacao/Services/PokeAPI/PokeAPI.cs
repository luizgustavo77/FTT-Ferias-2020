using Apresentacao.Helpers;
using Apresentacao.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Apresentacao.Services.PokeAPI
{
    public class PokeAPI
    {
        public async Task<PokemonModel> GetPokemon(string Id)
        {
            PokemonModel result = null;
            try
            {
                string json = "";
                using (HttpClient client = new HttpClient())
                {
                    json = await client.GetStringAsync("https://pokeapi.co/api/v2/pokemon/" + Id);//pokemon?limit=150&offset=0
                }
                result = new Serializer().GetObject<PokemonModel>(json);
            }
            catch (Exception ex)
            {
                
            }
            return result;
        }
    }
}

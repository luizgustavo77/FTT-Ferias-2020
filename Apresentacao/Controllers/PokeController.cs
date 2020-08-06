using Apresentacao.Entities;
using Apresentacao.Entities.DAO;
using Apresentacao.Helpers;
using Apresentacao.Helpers.Common;
using Apresentacao.Models;
using Apresentacao.Services.PokeAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace Apresentacao.Controllers
{
    [AutorizacaoSession]
    public class PokeController : Controller
    {
        private readonly ILoginDAO _loginDAO;
        private Cache _cache;
        private Session _session;

        public PokeController(ILoginDAO dao, IDistributedCache cache)
        {
            _loginDAO = dao;
            _cache = new Cache(cache);
            _session = new Session();
        }

        public async Task<IActionResult> Index([FromQuery(Name = "id")] string id)
        {
            PokemonModel pokemon = null;
            Login usuario = _session.GetObject<Login>(State.LoginSession);
            pokemon = _cache.Get<PokemonModel>(usuario.Id.ToString());
            if (pokemon == null)
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    PokeAPI api = new PokeAPI();
                    pokemon = await api.GetPokemon(id);
                    _cache.Create<PokemonModel>(usuario.Id.ToString(), pokemon);
                }
            }
            return View(pokemon);
        }

        public IActionResult Details()
        {            
            return PartialView();
        }
    }
}
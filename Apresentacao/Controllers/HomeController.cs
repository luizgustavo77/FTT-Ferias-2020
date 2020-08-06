using Apresentacao.Entities;
using Apresentacao.Entities.Data;
using Apresentacao.Helpers;
using Apresentacao.Helpers.Common;
using Apresentacao.Models;
using Apresentacao.Services.PokeAPI;
using ApresentacaoCore.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apresentacao.Controllers
{
    public class HomeController : Controller
    {
        private PokeAPI _pokeApi;
        private Cache _cache;
        private Session _session;
        public HomeController(IDistributedCache cache)
        {
            _pokeApi = new PokeAPI();
            _cache = new Cache(cache);
            _session = new Session();
        }

        [AutorizacaoSession]
        public async Task<ActionResult> Index()
        {
            IList<PokemonModel> pokemonModels = null;

            ViewData["Title"] = Resource.Home_msg;
            Login usuario = _session.GetObject<Login>(State.LoginSession);
            string cache = _cache.Get(usuario.Id.ToString());
            if (string.IsNullOrWhiteSpace(cache))
                pokemonModels = await EscolherPrimario();
            else
                ViewBag.IsTrainer = true;
            return View(pokemonModels);
        }

        [AutorizacaoSession]
        private async Task<IList<PokemonModel>> EscolherPrimario()
        {
            IList<PokemonModel> result = new List<PokemonModel>();
            string[] iniciais = new string[3] { "1", "4", "7" };

            foreach (string id in iniciais)
            {
                PokemonModel pokemon = await _pokeApi.GetPokemon(id);
                result.Add(pokemon);
            }

            return result;
        }

        #region Segredo
        [HttpPost]
        [AutorizacaoSession]
        public async Task<JsonResult> Search(string Id)
        {
            PokemonModel pokemon = null;
            if (!string.IsNullOrWhiteSpace(Id))
            {
                pokemon = await _pokeApi.GetPokemon(Id);
            }
            else
            {
                pokemon = await _pokeApi.GetPokemon("25");
            }

            return Json(pokemon);
        }
        #endregion

        [AutorizacaoSession]
        public ActionResult About()
        {
            ViewBag.Message = Resource.About_msg;

            return View();
        }

        [AutorizacaoSession]
        public ActionResult Contact()
        {
            return View();
        }

        [Route("[controller]/Error/{statusCode}")]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
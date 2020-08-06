using Apresentacao.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Apresentacao.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            ViewData["Title"] = "BEM VINDO TREINADOR!";
            return View(await EscolherPrimario());
        }

        private async Task<IList<PokemonModel>> EscolherPrimario()
        {
            IList<PokemonModel> result = new List<PokemonModel>();
            string[] iniciais = new string[3] { "1", "4", "7"};

            foreach(string id in iniciais)
            {
                PokemonModel pokemon = await PokeAPI(id);
                result.Add(pokemon);
            }

            return result;
        }

        private async Task<PokemonModel> PokeAPI(string Id)
        {
            PokemonModel result = null;
            try
            {
                string json = "";
                using (HttpClient client = new HttpClient())
                {
                    json = await client.GetStringAsync("https://pokeapi.co/api/v2/pokemon/" + Id);//pokemon?limit=150&offset=0
                }
                result = new JavaScriptSerializer().Deserialize<PokemonModel>(json);
            }
            catch (Exception ex)
            {
                Error();
            }
            return result;
        }

        #region Segredo
        [HttpPost]
        public async Task<JsonResult> Search(string Id = "25")
        {
            PokemonModel pokemon = await PokeAPI(Id);

            return new JsonResult()
            {
                Data = new
                {
                    name = pokemon.name.ToUpper(),
                    sprites = pokemon.sprites.front_default
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }

        #endregion

        public ActionResult About()
        {
            ViewBag.Message = "Essa pagina foi feita com intuito de mostrar diversas ferramentas para desenvolvimento web e tecnologias que são no minimo basicas para inicias na area.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
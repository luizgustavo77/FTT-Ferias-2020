using Apresentacao.Entities;
using Apresentacao.Entities.DAO;
using Apresentacao.Helpers;
using Apresentacao.Helpers.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Apresentacao.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginDAO _loginDAO;

        public LoginController(ILoginDAO dao)
        {
            _loginDAO = dao;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,PublicKey,PrivateKey")] Login login)
        {
            if (ModelState.IsValid)
            {
                _loginDAO.Adicionar(login);
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,Username,Password,PublicKey,PrivateKey")] Login login)
        {
            if (ModelState.IsValid)
            {
                Login result = _loginDAO.Logar(login);
                if (result != null)
                {
                    new Session().Create<Login>(State.LoginSession, result);
                    return RedirectToAction("Index", "Home");
                }
                else
                    return View(login);
            }
            return View(login);
        }

        [AutorizacaoSession]
        public async Task<IActionResult> List()
        {
            return View(_loginDAO.ListLogins());
        }

        [AutorizacaoSession]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = _loginDAO.Localizar(id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        [AutorizacaoSession]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = _loginDAO.Localizar(id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutorizacaoSession]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,PublicKey,PrivateKey")] Login login)
        {
            if (id != login.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _loginDAO.Atualizar(login);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }

        [AutorizacaoSession]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = _loginDAO.Localizar(id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AutorizacaoSession]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var login = _loginDAO.Localizar(id);
            _loginDAO.Remover(login);
            return RedirectToAction(nameof(Index));
        }

        [AutorizacaoSession]
        private bool LoginExists(int id)
        {
            return _loginDAO.Existe(id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Projeto_Cid2.Models;
using Projeto_Cid2.Repositorio;
namespace Projeto_Cid2.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio produtoRepositorio;

        public ProdutoController(ProdutoRepositorio _produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

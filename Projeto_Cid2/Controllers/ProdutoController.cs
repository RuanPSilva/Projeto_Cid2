using Projeto_Cid2.Models;
using Microsoft.AspNetCore.Mvc;
using Projeto_Cid2.Repositorio;

namespace Projeto_Cid2.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public IActionResult Index()
        {
            return View(_produtoRepositorio.TodosProdutos());
        }

        public IActionResult CadastrarProduto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            _produtoRepositorio.Cadastrar(produto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarProduto(int id)
        {
            var produto = _produtoRepositorio.ObterProduto(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarProduto(int id, [Bind("idprod, nomeprod, descricao, preco")] Produto produto)
        {
            if (id != produto.idProd)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_produtoRepositorio.Atualizar(produto))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ouve um erro ao editar.");
                    return View(produto);
                }
            }
            return View(produto);
        }

        public IActionResult ExcluirProduto(int id)
        {
            _produtoRepositorio.ExcluirProduto(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

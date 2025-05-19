using Microsoft.AspNetCore.Mvc;
using Projeto_Cid2.Repositorio;
namespace Projeto_Cid2.Controllers
{
    public class UsuarioController : Controller
    {
        
        public class LoginController : Controller
        {
            private readonly LoginRepositorio _usuarioRepositorio;

            
            public LoginController(LoginRepositorio usuarioRepositorio)
            {             
                _usuarioRepositorio = usuarioRepositorio;
            }


            
            public IActionResult Login()
            {
              
                return View();
            }

           
            [HttpPost]
            public IActionResult Login(string email, string senha)
            {

                var usuario = _usuarioRepositorio.ObterUsuario(email);
               
                if (usuario != null && usuario.senha == senha)
                {
                    
                    return RedirectToAction("Index", "Produto");
                }
               
                ModelState.AddModelError("", "Algo está errado, verifique novamente!!.");
                
                return View();
            }
        }
    }
}

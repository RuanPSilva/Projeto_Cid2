using Microsoft.AspNetCore.Mvc;
using Projeto_Cid2.Repositorio;

namespace Projeto_Cid2.Controllers
{
    public class UsuarioController : Controller
    {
        
            private readonly UsuarioRepositorio _usuarioRepositorio;

            
            public UsuarioController(UsuarioRepositorio usuarioRepositorio)
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
                    
                    return RedirectToAction("Login", "Usuario");
                }
               
                ModelState.AddModelError("", "Algo está errado, verifique novamente!!.");
                
                return View();
            }
        }
    }


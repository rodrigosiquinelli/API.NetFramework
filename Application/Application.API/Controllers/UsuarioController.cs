using Application.Infraestrutura;
using Application.Negocio.UsuarioNegocio;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Application.API.Controllers
{
    public class UsuarioController : ApiController
    {
        IUsuarioNegocio usuarioNegocio;

        public UsuarioController(IUsuarioNegocio _usuarioNegocio)
        {
            this.usuarioNegocio = _usuarioNegocio;
        }

        [HttpGet]
        [Authorize]
        [ActionName("BuscarUsuarios")]
        public HttpResponseMessage BuscarUsuarios()
        {                                         

            try
            {
                var listaUsuarios = usuarioNegocio.BuscaTodosUsuarios();

                return Request.CreateResponse(HttpStatusCode.OK, listaUsuarios);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }

        }

        [HttpPost]
        [Authorize]
        [ActionName("CadastrarUsuario")]
        public HttpResponseMessage CadastrarUsuario(Usuario usuario)
        {
            try
            { 
                usuarioNegocio.SelecionaUsuarioPorLogin(usuario.login);

                Usuario _usuario = usuarioNegocio.SalvaUsuario(usuario);

                return Request.CreateResponse(HttpStatusCode.OK, _usuario);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

       [HttpPut]
       [Authorize]
       [ActionName("AlterarUsuario")]
       public HttpResponseMessage AlterarUsuario(Usuario usuario)
        {
           try
           {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;//atribui o login do usuario atual
               
                usuarioNegocio.VerificaLogin(principal.Identity.Name, usuario.login);//verifica se o usuario atual está tentando alterar um usuario que não é ele
                
                Usuario _usuario = usuarioNegocio.AlteraUsuario(usuario);

                return Request.CreateResponse(HttpStatusCode.OK, _usuario);

            }
           catch (Exception e)
           {
               return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
           }
       }

        [HttpGet]
        [Authorize]
        [ActionName("BuscarUsuarioPorID")]
        public HttpResponseMessage BuscarUsuarioPorID(int id)
        {
            Usuario usuario;

            try
            {
                usuario = usuarioNegocio.SelecionaUsuarioPorID(id);

                return Request.CreateResponse(HttpStatusCode.OK, usuario);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }

        }
    }
}
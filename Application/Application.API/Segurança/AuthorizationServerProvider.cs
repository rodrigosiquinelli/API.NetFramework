using Application.Dados.UsuarioDados;
using Application.Infraestrutura;
using Application.Negocio.UsuarioNegocio;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Application.API.Segurança
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); //valida o TOKEN no contexto que OAuth é responsavel
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //adiciona no cabeçalho a origem da requisicao
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });  

            String login = context.UserName;
            String senha = context.Password;

            try
            {

                var db = new ApplicationdbEntities();
                var usuarioDados = new UsuarioDados(db);
                var usuarioNegocios = new UsuarioNegocio(usuarioDados);
               

                if (!usuarioNegocios.VerificaUsuario(login, senha))//verifica se usuario e senha passados existem
                {
                    return;
                }

                //identifica o usuário
                var identidade = new ClaimsIdentity(context.Options.AuthenticationType);
                identidade.AddClaim(new Claim(ClaimTypes.Name, login));
               

                //atribui Roles
                var roles = new String[] { };
                foreach (var role in roles)
                {
                    identidade.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                GenericPrincipal principal = new GenericPrincipal(identidade, roles.ToArray());
                Thread.CurrentPrincipal = principal; //configura na Thread principal para poder recuperar no controle

                context.Validated(identidade);
            }
            catch (Exception e)
            {
                context.SetError("Falha de Autenticação", "Falha ao realizar Autenticação.\n\n" + e.Message);
            }
        }

    }
}
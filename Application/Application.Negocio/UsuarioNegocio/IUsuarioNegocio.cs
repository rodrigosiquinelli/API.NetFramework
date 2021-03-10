using Application.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Negocio.UsuarioNegocio
{
    public interface IUsuarioNegocio
    {
        IList<Usuario> BuscaTodosUsuarios();

        Usuario SalvaUsuario(Usuario usuario);

        Usuario AlteraUsuario(Usuario usuario);

        Usuario SelecionaUsuarioPorID(int id);

        void SelecionaUsuarioPorLogin(String login);

        void VerificaLogin(String loginAlterado, String loginAtual);

        bool VerificaUsuario(string login, string senha);

    }
}

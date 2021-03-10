using Application.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dados.UsuarioDados
{
    public interface IUsuarioDados
    {
        IList<Usuario> BuscaTodosUsuarios();

        Usuario SalvaUsuario(Usuario usuario);

        Usuario AlteraUsuario(Usuario usuario);

        Usuario SelecionaUsuarioPorID(int id);

        Usuario SelecionaUsuarioPorLogin(String login);

        Usuario VerificaUsuario(string login, string senha);
    }
}

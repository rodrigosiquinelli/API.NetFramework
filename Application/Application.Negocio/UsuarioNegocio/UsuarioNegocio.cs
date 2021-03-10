using Application.Dados.UsuarioDados;
using Application.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Negocio.UsuarioNegocio
{
    public class UsuarioNegocio : IUsuarioNegocio
    {

        #region ATRIBUTOS E CONSTRUTOR
        private IUsuarioDados usuarioDAO = null;

        public UsuarioNegocio(IUsuarioDados usuarioDAO)
        {
            this.usuarioDAO = usuarioDAO;
        }
        #endregion

        #region Usuario
        public virtual IList<Usuario> BuscaTodosUsuarios()
        {
            

            return usuarioDAO.BuscaTodosUsuarios();

        }

        public Usuario SalvaUsuario(Usuario usuario)
        {
            if (usuario != null)

                return usuarioDAO.SalvaUsuario(usuario);

            throw new Exception("Verifique se foram informados os dados do usuario.");
        }

        public Usuario AlteraUsuario(Usuario usuario)
        {
            
                Usuario usuarioDoBanco = usuarioDAO.SelecionaUsuarioPorID(usuario.codUsuario);

                SelecionaUsuarioPorLogin(usuario.login);


                if (usuario != null)
                {

                    if (usuario.login != null)

                        usuarioDoBanco.login = usuario.login;

                    if (usuario.senha != null)

                        usuarioDoBanco.senha = usuario.senha;

                    if (usuarioDoBanco.perfil == "admin")//apenas admin pode alterar perfis e realizar exclusão lógica de usuarios 
                    {
                        if (usuario.perfil != null)

                            usuarioDoBanco.perfil = usuario.perfil;

                        if (usuario.ativo != null)

                            usuarioDoBanco.ativo = usuario.ativo;
                    }

                    return usuarioDAO.AlteraUsuario(usuarioDoBanco);
                }

                throw new Exception("Você não está tentando alterar nenhum dado.");
            
        }

        public void VerificaLogin(String loginAlterado, String loginAtual)
        {

            if (loginAlterado != loginAtual)

                throw new Exception("Você está tentando editar um usuário diferente do seu!");

        }
        

        public Usuario SelecionaUsuarioPorID(int id)
        {
            Usuario usuario;

            if (id > 0)
            {
                usuario = usuarioDAO.SelecionaUsuarioPorID(id);

                if (usuario == null)
                    throw new Exception("Usuario com este ID não foi encontrado.");

                return usuario;
            }

            throw new Exception("Por favor, informe um ID válido.");
        }

        public void SelecionaUsuarioPorLogin(String login)
        {
            Usuario usuario;

            if (!String.IsNullOrEmpty(login))
            {
                usuario = usuarioDAO.SelecionaUsuarioPorLogin(login);

                if (usuario != null)
                    throw new Exception("Este login já existe.");

            }
        }

        public bool VerificaUsuario(string login, string senha)
        {
            
            Boolean isValid = false;
            Usuario usuario = usuarioDAO.VerificaUsuario(login, senha);

            if (usuario != null)
            {
                isValid = true;
            }

            return isValid;
        }

        #endregion
    }
}

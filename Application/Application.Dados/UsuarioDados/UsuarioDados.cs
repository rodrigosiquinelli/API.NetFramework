using Application.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dados.UsuarioDados
{
    public class UsuarioDados : IUsuarioDados
    {
        #region ATRIBUTOS E CONSTRUTOR
        private ApplicationdbEntities db = null;

        public UsuarioDados(ApplicationdbEntities db)
        {
            this.db = db;
        }
        #endregion

        #region Usuario        
        public IList<Usuario> BuscaTodosUsuarios()
        {
            return db.tbUsuario.ToList();
        }

        public Usuario SalvaUsuario(Usuario usuario)
        {
            db.tbUsuario.Add(usuario);
            db.SaveChanges();

            return usuario;
        }

        public Usuario AlteraUsuario(Usuario usuario)
        {
            db.Entry<Usuario>(usuario).State = EntityState.Modified;
            db.SaveChanges();

            return usuario;
        }

        public Usuario SelecionaUsuarioPorID(int id)
        {
            return db.tbUsuario.SingleOrDefault(x => x.codUsuario == id);
        }

        public Usuario SelecionaUsuarioPorLogin(String login)
        {
            return db.tbUsuario.SingleOrDefault(x => x.login == login);
        }

        public Usuario VerificaUsuario(string login, string senha)
        {
            
            Usuario usuario = db.tbUsuario.FirstOrDefault(x => x.login.Equals(login) && x.senha.Equals(senha) && x.ativo == true);

            return usuario;
        }
        #endregion
    }
}


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Infraestrutura;

namespace Application.Dados.PessoaDados
{
    public class PessoaDados : IPessoaDados
    {
        #region ATRIBUTOS E CONSTRUTOR
        private ApplicationdbEntities db = null;

        public PessoaDados(ApplicationdbEntities db)
        {
            this.db = db;
        }
        #endregion

        #region Pessoa
        public IList<Pessoa> BuscaTodasPessoas()
        {
            return db.tbPessoa.ToList();
        }

        public Pessoa SalvaPessoa(Pessoa pessoa)
        {
            
            db.tbPessoa.Add(pessoa);
            db.SaveChanges();

            return pessoa;
        }

        public Pessoa AlteraPessoa(Pessoa pessoa)
        {
            db.Entry<Pessoa>(pessoa).State = EntityState.Modified;
            db.SaveChanges();

            return pessoa;
        }

        public Pessoa BuscaPessoaPorID(int codPessoa)
        {
            return db.tbPessoa.SingleOrDefault(x => x.codPessoa == codPessoa);
        }

        public IEnumerable<Pessoa> SelecionaPessoaPorNome(String nome)
        {
            return db.tbPessoa.Where(x => x.nome.Equals(nome)).ToList();
        }
        #endregion
    }
}

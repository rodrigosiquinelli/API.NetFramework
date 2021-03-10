using Application.Dados.PessoaDados;
using Application.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Negocio.PessoaNegocio
{
    public class PessoaNegocio : IPessoaNegocio
    {
        #region ATRIBUTOS E CONSTRUTOR
        private IPessoaDados pessoaDAO = null; 

        public PessoaNegocio(IPessoaDados pessoaDAO)
        {
            this.pessoaDAO = pessoaDAO;
        }
        #endregion

        #region PESSOA
        public virtual IList<Pessoa> BuscaTodasPessoas()
        {
            return pessoaDAO.BuscaTodasPessoas();
        }

        public Pessoa SalvaPessoa(Pessoa pessoa)
        {

            if (pessoa != null)
            
                return pessoaDAO.SalvaPessoa(pessoa);
        
            throw new Exception("Verifique se foram informados os dados da pessoa.");
        }

        public Pessoa AlteraPessoa(Pessoa pessoa)
        {
            Pessoa pessoaDoBanco = pessoaDAO.BuscaPessoaPorID(pessoa.codPessoa);

            if (pessoa != null)
            {
                //altera apenas os campos que foram informados
                if (pessoa.nome != null)

                    pessoaDoBanco.nome = pessoa.nome;

                if (pessoa.sobrenome != null)

                    pessoaDoBanco.sobrenome = pessoa.sobrenome;

                if (pessoa.dataNascimento != null)

                    pessoaDoBanco.dataNascimento = pessoa.dataNascimento;

                if (pessoa.sexo != null)

                    pessoaDoBanco.sexo = pessoa.sexo;

                if (pessoa.ativo != null)

                    pessoaDoBanco.ativo = pessoa.ativo;


                return pessoaDAO.AlteraPessoa(pessoaDoBanco);
            }

            throw new Exception("Você não está tentando alterar nenhum dado.");
        }

        public Pessoa BuscaPessoaPorID (int codPessoa)
        {
            Pessoa pessoa = null;

            if (codPessoa > 0)

                pessoa = pessoaDAO.BuscaPessoaPorID(codPessoa);

            return pessoa;
        }

        public IEnumerable<Pessoa> SelecionaPessoaPorNome(String nome)
        {
            IEnumerable <Pessoa> pessoa;

            if (!String.IsNullOrEmpty(nome))
            {
                pessoa = pessoaDAO.SelecionaPessoaPorNome(nome);

                if (pessoa != null)

                     return pessoa;

                throw new Exception("Nome de pessoa não encontrado.");
            }

            throw new Exception("Por favor, informe um nome.");
        }
        #endregion
    }
}

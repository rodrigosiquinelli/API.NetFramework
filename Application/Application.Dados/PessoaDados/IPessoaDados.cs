using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Infraestrutura;

namespace Application.Dados.PessoaDados
{
    public interface IPessoaDados
    {
        IList<Pessoa> BuscaTodasPessoas();

        Pessoa SalvaPessoa(Pessoa pessoa);

        Pessoa BuscaPessoaPorID(int codPessoa);

        Pessoa AlteraPessoa(Pessoa pessoa);

        IEnumerable<Pessoa> SelecionaPessoaPorNome(String pessoa);
    }
}

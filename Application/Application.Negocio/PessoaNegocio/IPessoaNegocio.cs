using Application.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Negocio.PessoaNegocio
{
    public interface IPessoaNegocio
    {
        IList<Pessoa> BuscaTodasPessoas();

        Pessoa SalvaPessoa(Pessoa pessoa);

        Pessoa AlteraPessoa(Pessoa pessoa);

        Pessoa BuscaPessoaPorID(int codPessoa);

        IEnumerable<Pessoa> SelecionaPessoaPorNome(String pessoa);
    }
}

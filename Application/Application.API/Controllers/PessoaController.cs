using Application.Infraestrutura;
using Application.Negocio.PessoaNegocio;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Application.API.Controllers
{
    public class PessoaController : ApiController
    {
        IPessoaNegocio pessoaNegocio;

        public PessoaController(IPessoaNegocio _pessoaNegocio)
        {
            this.pessoaNegocio = _pessoaNegocio;
        }

        [HttpGet]
        [Authorize]
        [ActionName("BuscarPessoas")]
        public HttpResponseMessage BuscarPessoas()
        {                                        
            try
            {
                var listaPessoas = pessoaNegocio.BuscaTodasPessoas();

                return Request.CreateResponse(HttpStatusCode.OK, listaPessoas);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }

        }

        [HttpPost]
        [Authorize]
        [ActionName("CadastrarPessoa")]
        public HttpResponseMessage CadastrarPessoa(Pessoa pessoa)
        {
            try
            {
                Pessoa _pessoa = pessoaNegocio.SalvaPessoa(pessoa);

                return Request.CreateResponse(HttpStatusCode.OK, _pessoa);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

       [HttpPut]
       [Authorize]
       [ActionName("AlterarPessoa")]
       public HttpResponseMessage AlterarPessoa(Pessoa pessoa)
        {
            try
            {
                Pessoa _pessoa = pessoaNegocio.AlteraPessoa(pessoa);

                return Request.CreateResponse(HttpStatusCode.OK, _pessoa);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        [ActionName("BuscarPessoaPorID")]
        public HttpResponseMessage BuscarPessoaPorID( int codPessoa)
        {
            try
            {
                Pessoa Pessoa = pessoaNegocio.BuscaPessoaPorID(codPessoa);

                return Request.CreateResponse(HttpStatusCode.OK, Pessoa);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }

        }

        [HttpGet]
        [Authorize]
        [ActionName("BuscarPessoaPorNome")]
        public HttpResponseMessage BuscarPessoaPorNome(String nome)
        {
            IEnumerable<Pessoa> pessoa = null;

            try
            {
                pessoa = pessoaNegocio.SelecionaPessoaPorNome(nome);

                return Request.CreateResponse(HttpStatusCode.OK, pessoa);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Por favor, informe um nome."))
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, pessoa);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }
    }
}
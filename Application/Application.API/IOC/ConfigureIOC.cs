using Application.Dados.PessoaDados;
using Application.Dados.UsuarioDados;
using Application.Infraestrutura;
using Application.Negocio.PessoaNegocio;
using Application.Negocio.UsuarioNegocio;
using Unity;

namespace Application.API.IOC
{
    public class ConfigureIoC
    {
        UnityContainer container = null;

        public ConfigureIoC()
        {
            container = new UnityContainer();
        }

        public UnityContainer Register()
        {
            this.RegiterDB();
            this.RegisterDependencies();

            return container;
        }
        private void RegiterDB()
        {
            container.RegisterFactory<ApplicationdbEntities>(c => { return new ApplicationdbEntities(); }, FactoryLifetime.Hierarchical);
        }

        private void RegisterDependencies()
        {
            container.RegisterType<IPessoaNegocio, PessoaNegocio>(TypeLifetime.Hierarchical);
            container.RegisterType<IPessoaDados, PessoaDados>(TypeLifetime.Hierarchical);
            container.RegisterType<IUsuarioNegocio, UsuarioNegocio>(TypeLifetime.Hierarchical);
            container.RegisterType<IUsuarioDados, UsuarioDados>(TypeLifetime.Hierarchical);
        }
    }
}
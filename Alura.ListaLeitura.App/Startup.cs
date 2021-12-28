using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using static Alura.ListaLeitura.App.Controller.LivrosController;
using static Alura.ListaLeitura.App.Controller.CadastroController;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("Livros/ParaLer", ParaLer);
            builder.MapRoute("Livros/Lendo", Lendo);
            builder.MapRoute("Livros/Lidos", Lidos);
            builder.MapRoute("Livros/Detalhes/{id:int}", Detalhes);
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", NovoLivro);
            builder.MapRoute("Cadastro/ExibirFormulario", ExibirFormulario);
            builder.MapRoute("Cadastro/ProcessarFormulario", ProcessarFormulario);

            var routes = builder.Build();
            
            app.UseRouter(routes);
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using static Alura.ListaLeitura.App.Delegates.LivrosController;
using static Alura.ListaLeitura.App.Delegates.CadastroController;

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
            builder.MapRoute("livros/paraler", LivrosParaLer);
            builder.MapRoute("livros/lendo", LivrosLendo);
            builder.MapRoute("livros/lidos", LivrosLidos);
            builder.MapRoute("livros/detalhes/{id:int}", LivrosDetalhes);
            builder.MapRoute("cadastro/livro/{nome}/{autor}", NovoLivroParaLer);
            builder.MapRoute("cadastro/novolivro", ExibeFormulario);
            builder.MapRoute("cadastro/incluir", ProcessarFormulario);

            var routes = builder.Build();
            
            app.UseRouter(routes);
        }
    }
}
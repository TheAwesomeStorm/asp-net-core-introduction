using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using System;
using Microsoft.AspNetCore.Hosting;

namespace Alura.ListaLeitura.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            var repo = new LivroRepositorioCSV();

            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<StartUp>()
                .Build();
            host.Run();

            ImprimeLista(repo.ParaLer);
            ImprimeLista(repo.Lendo);
            ImprimeLista(repo.Lidos);
        }

        static void ImprimeLista(ListaDeLeitura lista)
        {
            Console.WriteLine(lista);
        }
    }
}

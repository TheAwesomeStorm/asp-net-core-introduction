using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.App.Controller
{
    public class CadastroController
    {
        private static LivroRepositorioCsv _repositorioCsv;

        static CadastroController()
        {
            _repositorioCsv = new LivroRepositorioCsv();
        }
        
        public string IncluirLivro(Livro livro)
        {
            _repositorioCsv.Incluir(livro);
            return "Livro adicionado a partir do formulário";
        }

        public IActionResult NovoLivro()
        {
            return new ViewResult()
            {
                ViewName = "formulario"
            };
        }
    }
}
using System;
using System.Collections.Generic;
using Alura.ListaLeitura.App.Negocio;
using System.IO;
using System.Linq;

namespace Alura.ListaLeitura.App.Repositorio
{
    public class LivroRepositorioCsv : ILivroRepositorio
    {
        private static readonly string nomeArquivoCSV = "Repositorio\\livros.csv";

        private ListaDeLeitura _paraLer;
        private ListaDeLeitura _lendo;
        private ListaDeLeitura _lidos;

        private List<Livro> _arrayParaLer;
        private List<Livro> _arrayLendo;
        private List<Livro> _arrayLidos;

        public LivroRepositorioCsv()
        {
            _arrayParaLer = new List<Livro>();
            _arrayLendo = new List<Livro>();
            _arrayLidos = new List<Livro>();
            UpdateCsv();
        }

        private void UpdateCsv()
        {
            _arrayParaLer.Clear();
            _arrayLendo.Clear();
            _arrayLidos.Clear();
            
            using (var file = File.OpenText(LivroRepositorioCsv.nomeArquivoCSV))
            {
                while (!file.EndOfStream)
                {
                    var textoLivro = file.ReadLine();
                    if (string.IsNullOrEmpty(textoLivro))
                    {
                        continue;
                    }
                    var infoLivro = textoLivro.Split(';');
                    var livro = new Livro
                    {
                        Id = Convert.ToInt32(infoLivro[1]),
                        Titulo = infoLivro[2],
                        Autor = infoLivro[3]
                    };
                    switch (infoLivro[0])
                    {
                        case "para-ler":
                            _arrayParaLer.Add(livro);
                            break;
                        case "lendo":
                            _arrayLendo.Add(livro);
                            break;
                        case "lidos":
                            _arrayLidos.Add(livro);
                            break;
                    }
                }
            }

            _paraLer = new ListaDeLeitura("Para Ler", _arrayParaLer.ToArray());
            _lendo = new ListaDeLeitura("Lendo", _arrayLendo.ToArray());
            _lidos = new ListaDeLeitura("Lidos", _arrayLidos.ToArray());
        }

        public ListaDeLeitura ParaLer
        {
            get
            {
                UpdateCsv();
                return _paraLer;
            }
        }

        public ListaDeLeitura Lendo
        {
            get
            {
                UpdateCsv();
                return _lendo;
            }
        }

        public ListaDeLeitura Lidos
        {
            get
            {
                UpdateCsv();
                return _lidos;
            }
        }

        public IEnumerable<Livro> Todos => _paraLer.Livros.Union(_lendo.Livros).Union(_lidos.Livros);

        public void Incluir(Livro livro)
        {
            var id = Todos.Select(l => l.Id).Max();
            using (var file = File.AppendText(LivroRepositorioCsv.nomeArquivoCSV))
            {
                file.WriteLine($"para-ler;{id+1};{livro.Titulo};{livro.Autor}");
            }
            UpdateCsv();
        }
    }
}

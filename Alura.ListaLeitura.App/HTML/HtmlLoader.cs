using System.IO;

namespace Alura.ListaLeitura.App.HTML
{
    public static class HtmlLoader
    {
        public static string CarregarArquivoHtml(string fileName)
        {
            var filePath = $"HTML/{fileName}.html";
            using (var arquivo = File.OpenText(filePath))
            {
                return arquivo.ReadToEnd();
            }
        }
    }
}
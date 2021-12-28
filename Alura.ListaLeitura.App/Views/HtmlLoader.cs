using System.IO;

namespace Alura.ListaLeitura.App.View
{
    public static class HtmlLoader
    {
        public static string CarregarArquivoHtml(string fileName)
        {
            var filePath = $"View/{fileName}.html";
            using (var arquivo = File.OpenText(filePath))
            {
                return arquivo.ReadToEnd();
            }
        }
    }
}
using IronPdf;
using System.IO;
using System;
using Domain.Interfaces;

namespace Infra.Services.PDFServices
{
    public class PDFWriter : IPDFWriter
    {
        public string Build(string path, string html)
        {
            var pathPDF = "/pdf-nao-gerrado-erro-verifique-o-servidor.pdf";
            try
            {
                //TODO o ideal é enviar para um bucket, porém como é somente um teste ficará no local por enquanto
                pathPDF = $"/Files/File-{DateTime.Now:dd-mm-yyyy}.pdf";
                var renderer = new HtmlToPdf();
                renderer.RenderHtmlAsPdf(html).SaveAs($"{path}/wwwroot{pathPDF}");
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro.Message);
                Console.WriteLine(erro.StackTrace);
            }

            return pathPDF;
        }
    }
}

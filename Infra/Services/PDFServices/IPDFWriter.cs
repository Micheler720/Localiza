using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Services.PDFServices
{
    interface IPDFWriter
    {
        string Build(string path, string body);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfModule.Interfaces
{
    public interface IPdfCreator
    {
        void CreatePdf(Object obj, out string fileName);
        void ClearFolder(string sourceDirectory);
    }
}

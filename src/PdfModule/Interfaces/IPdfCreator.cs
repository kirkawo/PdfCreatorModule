using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfModule.Interfaces
{
    public interface IPdfCreator<T> where T : class
    {
        void CreatePdf(T model, out string fileName, Enum fontSize);
        void CleanFolder(string sourceDirectory);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PdfModule;
using PdfMock;

namespace RunApp
{
    public class Program
    {
        static MockModel _model = new MockModel
        {
            Sender = "Jon Dou",
            Recepient = "Alexey Stupakov",
            Message = "Some order to do. Do it better"
        };
        public static void Main(string[] args)
        {
            string tmp;
            PdfCreator crt = new PdfCreator("temp/");
            crt.CreatePdf(_model, out tmp);

            crt.CleanFolder(@"temp/");
           
        }
    }
}

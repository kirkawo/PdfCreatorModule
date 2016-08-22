using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PdfModule;
using PdfMock;
using System.Reflection;
using BitMiracle.Docotic.Pdf;

namespace RunApp
{
    public class Program
    {
        static MockModel _model = new MockModel
        {
            Sender = "asdasdasd",
            Recepient = "dsfsdfdsfdsf",
            Message = "sdfsdfsdfsdfsdfs dfsdfsdfsdfdfsdfwrw ersdfwefsdf w efs fwefs dfwefdfwer"
        };
        public static void Main(string[] args)
        {
            string tmp;
            PdfCreator crt = new PdfCreator("temp/");
            crt.CreatePdf(_model, out tmp);

            crt.ClearFolder(@"temp/");
           
        }
    }
}

using PdfModule;
using PdfMock;
using BitMiracle.Docotic.Pdf;
using System.Text;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RunApp
{
    public class Program
    {
        static string tmp;
        static MockModel _model = new MockModel
        {
            Sender = "Jon Dou",
            Recepient = "Body tail",
            Message = "Some order to do. Do it better So Soapy left his seat, and walked slowly along the street.Soon he came to a bright restaurant on Broadway."
        };
        public static void Main(string[] args)
        {            
            PdfCreator<MockModel> tst = new PdfCreator<MockModel>("temp/");
            tst.CreatePdf(_model, out tmp, FontSize.Fourteen);
                       
        }
    }
}

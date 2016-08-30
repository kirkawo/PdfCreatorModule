using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PdfMock;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using PdfModule;
using System.Net.Http;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private static MockModel _model;
        private static MockModel _modelNull;
        private readonly string _path = @"pdftemp/";
        private string returnedName;
        private PdfCreator<MockModel> _generator;
        private HtmlToPdfCreator<MockModel> _htmlToPdf;
        private static string _apiKey = "d4223b69-fe9e-47be-91c5-d973cfdc6ab3";
        private static string _value = "http://www.google.com";

        private readonly IHostingEnvironment _appEnvironment;

        public HomeController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;

            _model = new MockModel
            {
                Sender = "KirkAwo",
                Recepient = "Jon Dou",
                Message = "Some order to do. Do it better So Soapy left his seat, and walked slowly along the street."
                +" Soon he came to a bright restaurant on Broadway.Ah!This was all right.He just had to get to a table in the restaurant and sit down.That was all"
            };

            _modelNull = new MockModel();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult GetFile()
        {
            _generator = new PdfCreator<MockModel>( _path);
            _generator.CreatePdf(_model, out returnedName, FontSize.Fourteen);


            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "PdfTemp/" + returnedName);

            string file_type = "application/pdf";

            return PhysicalFile(file_path, file_type, returnedName);
        }

        public IActionResult CleanPdfField()
        {
            _generator = new PdfCreator<MockModel>("");
            _generator.CleanFolder(_path);
            return RedirectToAction("Index");
        }

        public IActionResult Run()
        {
            _htmlToPdf = new HtmlToPdfCreator<MockModel>(_model);
                        
            return File(_htmlToPdf.HtmlToPdfByteArray(), "application/pdf", _htmlToPdf.ReturnNamePdf());                    
        }
    }
}


//System.Collections.Generic.KeyValuePair
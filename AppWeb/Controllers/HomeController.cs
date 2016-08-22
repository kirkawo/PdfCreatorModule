﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PdfModule;
using PdfMock;
using System.Threading;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace AppWeb.Controllers
{
    public class HomeController : Controller
    {
        private static MockModel _model;
        private static MockModel _modelNull;
        private readonly string _path = @"pdftemp/";
        private string returnedName;
        private PdfCreator _generator;

        private readonly IHostingEnvironment _appEnvironment;

        public HomeController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;

            _model = new MockModel
            {
                Sender = "KirkAwo",
                Recepient = "Alexey Stupakov",
                Message = "Do please some thing good!!!"
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
            _generator = new PdfCreator(_path);
            _generator.CreatePdf(_model, out returnedName);

            
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "PdfTemp/" + returnedName);
            
            string file_type = "application/pdf";
            
            return PhysicalFile(file_path, file_type, returnedName);
        }
    }
}
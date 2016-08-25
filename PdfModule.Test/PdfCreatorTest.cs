using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfModule.Interfaces;
using PdfModule;
using PdfMock;
using Xunit;
using System.Reflection;
using System.IO;
using Moq;

namespace PdfModule.Test
{
    public class PdfCreatorTest
    {
        #region Mock object
        static MockModel _model = new MockModel
        {
            Sender = "Jon Dou",
            Recepient = "Body tail",
            Message = "Some order to do. Do it better So Soapy left his seat, and walked slowly along the street.Soon he came to a bright restaurant on Broadway."
        };

        private static MockModel _modelNull = null;
      
        #endregion

        private PdfCreator<MockModel> pdfCreator;
        private static string _path = @"temp/";
        private static string _outName;

        [Fact]
        public void Test_Pdf_Files_That_Cant_Be_Equal()
        {
            var pdf11p = File.ReadAllBytes(_path + "MockModel_11p.pdf");
            var pdf14p = File.ReadAllBytes(_path + "MockModel_14p.pdf");

            Assert.NotEqual(pdf11p, pdf14p);
        }

        [Fact]
        public void Test_for_NullReferenceException()
        {
            pdfCreator = new PdfCreator<MockModel>("");
            Assert.Throws<ArgumentNullException>(() => pdfCreator.CreatePdf(_modelNull, out _outName, FontSize.Eleven));
        }
    }
}

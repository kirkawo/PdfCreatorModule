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
using BitMiracle.Docotic.Pdf;

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
        private static string originalFile11p = @"OriginFiles\MockModel_11_compare.pdf";
        private static string originalFile14p = @"OriginFiles\MockModel_14_compare.pdf";
        private static string fileToCompare11p = @"temp\MockModel_11p.pdf";
        private static string fileToCompare14p = @"temp\MockModel_14p.pdf";

        [Fact]
        public void Test_Pdf_Files_That_Cant_Be_Equal()
        {
            using (PdfDocument original11p = new PdfDocument(originalFile11p))
                original11p.Save("TestResult/original11p.pdf");

            using (PdfDocument toCompare14p = new PdfDocument(fileToCompare14p))
                toCompare14p.Save("TestResult/toCompare14p.pdf");

            using (PdfDocument original14p = new PdfDocument(originalFile14p))
                original14p.Save("TestResult/original14p.pdf");

            using (PdfDocument toCompare11p = new PdfDocument(fileToCompare11p))
                toCompare11p.Save("TestResult/toCompare11p.pdf");

            bool notEquals11pTo14p = PdfDocument.DocumentsAreEqual("TestResult/original11p.pdf", "TestResult/toCompare14p.pdf", "");
            bool notEquals14pTo11p = PdfDocument.DocumentsAreEqual("TestResult/original14p.pdf", "TestResult/toCompare11p.pdf", "");

            //Both value of NotEquals are cant be false
            Assert.Equal(notEquals11pTo14p, notEquals14pTo11p);
        }

        [Fact]
        public void Test_Pdf_Files_That_Can_Be_Equal()
        {
            using (PdfDocument original11p = new PdfDocument(originalFile11p))
                original11p.Save("TestResult/original11p.pdf");

            using (PdfDocument toCompare11p = new PdfDocument(fileToCompare11p))
                toCompare11p.Save("TestResult/toCompare11p.pdf");

            using (PdfDocument original14p = new PdfDocument(originalFile14p))
                original14p.Save("TestResult/original14p.pdf");

            using (PdfDocument toCompare14p = new PdfDocument(fileToCompare14p))
                toCompare14p.Save("TestResult/toCompare14p.pdf");
                                   

            bool equals11pTo11p = PdfDocument.DocumentsAreEqual("TestResult/original11p.pdf", "TestResult/toCompare11p.pdf", "");
            bool equals14pTo14p = PdfDocument.DocumentsAreEqual("TestResult/original14p.pdf", "TestResult/toCompare14p.pdf", "");

            //Both value of Equals are cant be true
            Assert.Equal(equals11pTo11p, equals14pTo14p);
        }

        [Fact]
        public void Test_for_NullReferenceException()
        {
            pdfCreator = new PdfCreator<MockModel>("");
            Assert.Throws<ArgumentNullException>(() => pdfCreator.CreatePdf(_modelNull, out _outName, FontSize.Eleven));
        }
    }
}

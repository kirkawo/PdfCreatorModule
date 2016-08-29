using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PdfModule;
using BitMiracle.Docotic.Pdf;
using PdfMock;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PdfModule.Test
{
    public class HtmlToPdfCreatorTest
    {
        private static MockModel _modelToTest = new MockModel();
        private static HtmlToPdfCreator<MockModel> _creatorHtmlToPdf;
        private static string _strToTest;
        

        [Fact]
        public void Test_HtmlToPdf_String_Must_Be_Equal_GetHtmlToString_Method()
        {
            _creatorHtmlToPdf = new HtmlToPdfCreator<MockModel>(_modelToTest);
            TagBuilder html = new TagBuilder("html");
            _strToTest = "<html></html>";
            string tagToString = _creatorHtmlToPdf.GetHtmlToString(html);

            Assert.Equal(_strToTest, tagToString);
        }
       
    }
}

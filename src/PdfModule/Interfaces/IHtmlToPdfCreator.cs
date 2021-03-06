﻿using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfModule.Interfaces
{
    public interface IHtmlToPdfCreator<T> where T : class
    {
        void SaveToFolder();
        string ReturnHtmlContent();
        string ReturnNamePdf();
        byte[] HtmlToPdfByteArray();
        string GetHtmlToString(IHtmlContent content);
        string GetHtml5ToString(IHtmlContent content);
    }
}

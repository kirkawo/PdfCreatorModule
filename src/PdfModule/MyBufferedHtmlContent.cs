using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfModule
{
    internal class MyBufferedHtmlContent : IHtmlContent
    {
        internal List<IHtmlContent> Entries { get; } = new List<IHtmlContent>();

        public MyBufferedHtmlContent Append(IHtmlContent htmlContent)
        {
            Entries.Add(htmlContent);
            return this;
        }


        public void WriteTo(TextWriter writer, System.Text.Encodings.Web.HtmlEncoder encoder)
        {
            foreach (var entry in Entries)
            {
                entry.WriteTo(writer, encoder);
            }
        }
    }
}

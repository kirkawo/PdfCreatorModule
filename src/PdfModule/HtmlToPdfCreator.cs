using PdfModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace PdfModule
{
    public class HtmlToPdfCreator<T> /* :  IHtmlToPdfCreator<T>*/
    {
        private static T _model;
        private static Type _type;
        private static PropertyInfo[] _propInfo;
        private TagBuilder _body;
        private string _tmp;

        public string ReturnString
        {
            get { return _tmp; }
        }

        public HtmlToPdfCreator(T model)
        {
            _model = model;
            _type = _model.GetType();
            _propInfo = _type.GetProperties();
        }

        public void TestGenerate()
        {
            EntityToHtml();
        }

        private void htmlGenerate(TagBuilder tag)
        {
            TagBuilder html = new TagBuilder("html");
            TagBuilder head = new TagBuilder("head");
            TagBuilder title = new TagBuilder("title");
            _body = new TagBuilder("body");

            html.InnerHtml.AppendHtml(head);
            html.InnerHtml.AppendHtml(title);
            html.InnerHtml.AppendHtml(_body);
            _body.InnerHtml.AppendHtml(tag);

            String content = GetString(html);
            _tmp = content;
            System.IO.File.WriteAllText(@"D:\LOOKATME.html", content);
        }

        

        private void EntityToHtml()
        {
            TagBuilder table = new TagBuilder("table");
            TagBuilder thead = new TagBuilder("thead");
            TagBuilder tbody = new TagBuilder("tbody");
            TagBuilder trThead = new TagBuilder("tr");
            TagBuilder trTbody = new TagBuilder("tr");
            TagBuilder tdThead = new TagBuilder("td");
            TagBuilder tdTbody = new TagBuilder("td");

            for (int i = 0; i < _propInfo.Length; ++i)
            {
                string field = (string)_propInfo[i].Name;
                tdThead.InnerHtml.SetHtmlContent(field);
                trThead.InnerHtml.AppendHtml(/*tdThead*/GetString(tdThead));
            }
            
            thead.InnerHtml.AppendHtml(trThead);

            foreach (var prop in _propInfo)
            {
                string item = (string)prop.GetValue(_model);
                tdTbody.InnerHtml.SetContent(item);
                trTbody.InnerHtml.AppendHtml(GetString(tdTbody));
            }
            tbody.InnerHtml.AppendHtml(trTbody);

            table.InnerHtml.AppendHtml(thead);
            table.InnerHtml.AppendHtml(tbody);

            htmlGenerate(table);
        }

        public static string GetString(IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        private class MyBufferedHtmlContent : IHtmlContent
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
}

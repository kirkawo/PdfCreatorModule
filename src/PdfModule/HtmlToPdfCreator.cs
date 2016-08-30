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
using System.Net.Http;

namespace PdfModule
{
    /// <summary>
    /// Can convert entity to PDF data format with using Html to pdf.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HtmlToPdfCreator<T> : IHtmlToPdfCreator<T> where T : class
    {
        private static T _model;
        private static Type _type;
        private static PropertyInfo[] _propInfo;
        private static TagBuilder _html;
        private static string _path;
        private static string _pathToBootstrap;
        private static string _fileName = "HTML-Template" + "(" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ")" + ".html";
        private static string _column;
        private static string _tableCss;
        private static string _returnContent;
        private static string _apiKey = "d4223b69-fe9e-47be-91c5-d973cfdc6ab3";
        private static string _value;

        /// <summary>
        /// Ctor that taking entity model
        /// </summary>
        /// <param name="model">Entity to convert.</param>        
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>        
        public HtmlToPdfCreator(T model)
        {            
            if (model == null)
                throw new ArgumentNullException($"Object instance is equal null. Can't write data.");

            _model = model;
            _type = _model.GetType();
            _propInfo = _type.GetProperties();
            _path = _fileName;

            _column = "col-md-4 col-md-offset-4";
            _tableCss = "table table-striped";
        }

        /// <summary>
        /// Ctor that taking entity model and path string where will be saving HTML file.
        /// </summary>
        /// <param name="model">Entity to convert.</param>
        /// <param name="path">It is path where will be created html file without name, only path to folder (Name of HTML file will be create automaticly).</param>
        public HtmlToPdfCreator(T model, string path) : this(model)
        {
            if (!String.IsNullOrEmpty(path) & !String.IsNullOrWhiteSpace(path))
                _path = path + _fileName;
            else
                _path = _fileName;
        }

        /// <summary>
        /// Ctor that taking entity model and path string where will be saving HTML file and path where bootstrap.min.css or bootstrap.css.
        /// </summary>
        /// <param name="model">Entity to convert.</param>
        /// <param name="path">It is path where will be created html file without name, only path to folder (Name of HTML file will be create automaticly).</param>
        /// <param name="tableCss">It is string value for customise tag - table. Specify the path to bootstrap.min.css or bootstrap.css</param>
        /// <param name="column">It is string value for customise HTML file. Grid system of bootstrap framework</param>
        public HtmlToPdfCreator(T model, string path, string tableCss, string column) : this(model, path)
        {
            if ((!String.IsNullOrEmpty(tableCss) & !String.IsNullOrWhiteSpace(tableCss)) & (!String.IsNullOrEmpty(column) & !String.IsNullOrWhiteSpace(column)))
            {
                _column = column;
                _tableCss = tableCss;
            }
            else
            {
                _column = "col-md-4 col-md-offset-4";
                _tableCss = "table table-striped";
            }
        }

        /// <summary>
        /// This method is for saving HTML file
        /// </summary>
        public void SaveToFolder()
        {
            EntityToHtml();
            System.IO.File.WriteAllText(_path, _returnContent);
        }

        /// <summary>
        /// This method return full string of HTML
        /// </summary>
        /// <returns>String data of Html content</returns>
        public string ReturnHtmlContent()
        {
            EntityToHtml();
            return _returnContent;
        }

        /// <summary>
        /// The method that build basik HTML file
        /// </summary>
        /// <param name="tag">It is a value that teking TagBuilder object for create complite HTML file</param>
        private void htmlGenerate(TagBuilder tag)
        {
            string h3Content = "This file is generated from " + _type.Name;

            _html = new TagBuilder("html");
            TagBuilder head = new TagBuilder("head");
            TagBuilder title = new TagBuilder("title");
            TagBuilder body = new TagBuilder("body");
            TagBuilder h3 = new TagBuilder("h3");
            TagBuilder divRow = new TagBuilder("div");
            TagBuilder divPageContainer = new TagBuilder("div");            
            string link = "<link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css\">";

            //applying styles
            divRow.AddCssClass("row");
            divPageContainer.AddCssClass(_column);
            tag.AddCssClass(_tableCss);

            h3.InnerHtml.SetContent(h3Content);

            divRow.InnerHtml.AppendHtml(divPageContainer);
            divPageContainer.InnerHtml.AppendHtml(h3);
            divPageContainer.InnerHtml.AppendHtml(tag);
                        
            _html.InnerHtml.AppendHtml(head);
            _html.InnerHtml.AppendHtml(title);
            head.InnerHtml.AppendHtmlLine(link);            
            _html.InnerHtml.AppendHtml(body);
            body.InnerHtml.AppendHtml(divRow);

            String content = GetHtml5ToString(_html);
            _returnContent = content;            
        }

        /// <summary>
        /// This method creating table that containce data of entity to html file
        /// </summary>
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
                trThead.InnerHtml.AppendHtml(GetHtmlToString(tdThead));
            }

            thead.InnerHtml.AppendHtml(trThead);

            foreach (var prop in _propInfo)
            {
                string item = (string)prop.GetValue(_model);
                tdTbody.InnerHtml.SetContent(item);
                trTbody.InnerHtml.AppendHtml(GetHtmlToString(tdTbody));
            }
            tbody.InnerHtml.AppendHtml(trTbody);

            table.InnerHtml.AppendHtml(thead);
            table.InnerHtml.AppendHtml(tbody);

            htmlGenerate(table);
        }

        /// <summary>
        /// This method taking IHtmlContent object and genereting string value.
        /// </summary>
        /// <param name="content">Taking IHtmlContent object (TagBuilder object)</param>
        /// <returns>Return string value of IHtmlContent object</returns>
        public string GetHtmlToString(IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        /// <summary>
        /// This method taking IHtmlContent object and genereting string value of full HTML5.
        /// </summary>
        /// <param name="content">Taking IHtmlContent object (TagBuilder object)</param>
        /// <returns>Return string value of IHtmlContent object</returns>
        public string GetHtml5ToString(IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return "<!DOCTYPE HTML>" + writer.ToString();
        }

        public byte[] HtmlToPdfByteArray()
        {
            EntityToHtml();
            _value = _returnContent;
            if (String.IsNullOrEmpty(_value) & String.IsNullOrWhiteSpace(_value))
            {
                _value = "<h1>Something went wrong</h1>";
            }
            byte[] array;
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[] { new System.Collections.Generic.KeyValuePair<string, string>("apikey", _apiKey), new System.Collections.Generic.KeyValuePair<string, string>("value", _value) });

                var result = client.PostAsync("http://api.html2pdfrocket.com/pdf", content).Result;

                MemoryStream stream = new MemoryStream(result.Content.ReadAsByteArrayAsync().Result);
                array = stream.ToArray();
            }
            return array;
        }
    }
}

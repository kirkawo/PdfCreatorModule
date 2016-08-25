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
        private static string _fileName = "HTML-Template" + "(" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ")" + ".html";
        private string _StyleCss;

        /// <summary>
        /// 
        /// </summary>
        private static string _defaultTableCssCells =
            "table {font-family: \"Lucida Sans Unicode\", \"Lucida Grande\", Sans-Serif; text-align: left; border-collapse: separate; border-spacing: 2px; background: #BDBDBD; color: #212121; border: 30px solid #BDBDBD; border-radius: 60px;}"
            + "th {font-size: 18px; padding: 12px;}"
            + "td {background: #E0E0E0; padding: 11px;}";

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
            _StyleCss = _defaultTableCssCells;
            _path = _fileName;
        }

        /// <summary>
        /// Ctor that taking entity model and path string where will be saving HTML file.
        /// </summary>
        /// <param name="model">Entity to convert.</param>
        /// <param name="path">It is path where will be created html file without name, only path to folder (Name of HTML file will be create automaticly).</param>
        public HtmlToPdfCreator(T model, string path) : this (model)
        {
            if (!String.IsNullOrEmpty(path) & !String.IsNullOrWhiteSpace(path))
                _path = path + _fileName;
            else
                _path = _fileName;

            _StyleCss = _defaultTableCssCells;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">Entity to convert.</param>
        /// <param name="path">It is path where will be created html file without name, only path to folder (Name of HTML file will be create automaticly).</param>
        /// <param name="style">It is string value for customise HTML file that will be create.</param>
        public HtmlToPdfCreator(T model, string path, string style) : this (model, path)
        {
            if (!String.IsNullOrEmpty(style) & !String.IsNullOrWhiteSpace(style))
                _StyleCss = style;
            else
                _StyleCss = _defaultTableCssCells;
        }
                
        /// <summary>
        /// This is method for saving HTML file
        /// </summary>
        public void SaveToFolder()
        {
            EntityToHtml();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag">It is a value that teking TagBuilder object for create complite HTML file</param>
        private void htmlGenerate(TagBuilder tag)
        {
            
            _html = new TagBuilder("html");
            TagBuilder head = new TagBuilder("head");
            TagBuilder title = new TagBuilder("title");
            TagBuilder body = new TagBuilder("body");
            TagBuilder style = new TagBuilder("style");
            style.InnerHtml.AppendHtml(_StyleCss);
            _html.InnerHtml.AppendHtml(head);
            _html.InnerHtml.AppendHtml(title);
            head.InnerHtml.AppendHtml(style);
            _html.InnerHtml.AppendHtml(body);
            body.InnerHtml.AppendHtml(tag);
                        
            String content = GetHtmlToString(_html);
            System.IO.File.WriteAllText(_path, content);            
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
    }
}

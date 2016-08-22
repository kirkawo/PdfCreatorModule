﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BitMiracle.Docotic.Pdf;
using System.IO;
using PdfModule.Interfaces;

namespace PdfModule
{
    /// <summary>
    /// Can convert entity to PDF data format.
    /// </summary>
    public class PdfCreator : IPdfCreator
    {
        private static int PointOne = 70;
        private static int PointTwo = 70;
        private static string pathToFile;
        private static string _fileName;
        private static string _path;

        /// <summary>
        /// Ctor that takes path to folder where will be created PDF-files 
        /// </summary>
        /// <param name="path">May contain the path to the folder or an empty string. When the "path" field is an empty string, all files are created in the root directory</param>
        public PdfCreator(string path)
        {
            if (!String.IsNullOrEmpty(path))
                _path = path;
        }

        /// <summary>
        /// Convert entity to PDF data format.
        /// </summary>
        /// <param name="obj">Entity to convert.</param>
        /// <param name="fileName">The file name is automatically generated by method.</param>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public void CreatePdf(Object obj, out string fileName)
        {
            if (obj == null)
                throw new ArgumentNullException($"Object instance is equal null. Can't write data.");

            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();

            _fileName = type.Name + "(" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ")" + ".pdf";
            fileName = _fileName;

            if (String.IsNullOrWhiteSpace(_path))
            {
                pathToFile = _fileName;
            }
            else
            {
                pathToFile = _path + PdfCreator._fileName;
            }

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.FontSize = 10;
                
                for (int i = 0; i < props.Length; i++)
                {                    
                    canvas.DrawText(props[i].Name, new PdfRectangle(20, PointOne, 100, 100), PdfTextAlign.Left, PdfVerticalAlign.Top);
                    PointOne = PointOne + 10;
                }

                foreach (var prop in props)
                {
                    string item = (string)prop.GetValue(obj);

                    canvas.TextHorizontalScaling = 70;
                    canvas.DrawText(item, new PdfRectangle(120, PointTwo, 400, 400), PdfTextAlign.Left, PdfVerticalAlign.Top);
                    PointTwo = PointTwo + 10;
                }
                
                pdf.Save(pathToFile);
            }
        }
        /// <summary>
        /// Cleans the directory that contains the files .pdf
        /// </summary>
        /// <param name="sourceDirectory">It contains the address of the directory with the files to clean</param>
        public void ClearFolder(string sourceDirectory)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(sourceDirectory);
                IEnumerable<FileInfo> fInfo = di.EnumerateFiles();

                foreach (var item in fInfo)
                {
                    File.Delete(item.FullName);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

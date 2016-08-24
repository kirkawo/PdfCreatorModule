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
    public class PdfCreator<T> /*: IPdfCreator*/
    {
        private static T _model;
        private static Type _type;
        private static PropertyInfo[] _propInfo;
        private static int PointOne = 80;
        private static int PointTwo = 80;
        private static int LinePoint = 92;
        private static string pathToFile;
        private static string _fileName;
        private static string _path;

        /// <summary>
        /// 
        /// </summary>
        public PdfCreator()
        {

        }

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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="path"></param>
        public PdfCreator(T model, string path) : this (path)
        {
            if (model == null)
                throw new ArgumentNullException($"Object instance is equal null. Can't write data.");
            _model = model;
            _type = _model.GetType();
            _propInfo = _type.GetProperties();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fontSize"></param>
        public void CreatePdf(out string fileName, Enum fontSize)
        {
            _fileName = _type.Name + "(" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ")" + ".pdf";
            fileName = _fileName;

            if (String.IsNullOrWhiteSpace(_path))
            {
                pathToFile = _fileName;
            }
            else
            {
                pathToFile = _path + PdfCreator<T>._fileName;
            }

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                canvas.SaveState();

                canvas.Pen.Width = 2;
                canvas.CurrentPosition = new PdfPoint(0, 45);
                canvas.DrawLineTo(660, 45);
                canvas.FontSize = 16;
                PdfFont builtInFont = pdf.AddFont(PdfBuiltInFont.TimesRoman);
                canvas.Font = builtInFont;
                canvas.DrawString("Templeate of " + _type.Name, new PdfRectangle(100, 48, 400, 400), PdfTextAlign.Center, PdfVerticalAlign.Top);

                canvas.RestoreState();

                if (FontSize.Eleven.Equals(fontSize))
                {
                    canvas.FontSize = 11;
                }
                else if(FontSize.Fourteen.Equals(fontSize))
                {
                    canvas.FontSize = 14;
                    LinePoint = 95;
                }
                else
                {
                    canvas.FontSize = 11;
                }

                canvas.SaveState();

                canvas.Brush.Color = new PdfGrayColor(40);

                canvas.CurrentPosition = new PdfPoint(110, 70);
                canvas.DrawLineTo(110, 800);

                for (int i = 0; i < _propInfo.Length; i++)
                {
                    canvas.DrawText(_propInfo[i].Name, new PdfRectangle(20, PointOne, 100, 100), PdfTextAlign.Left, PdfVerticalAlign.Top);
                    canvas.CurrentPosition = new PdfPoint(20, LinePoint);
                    canvas.DrawLineTo(100, LinePoint);
                    LinePoint = LinePoint + 14;
                    PointOne = PointOne + 14;
                }

                canvas.RestoreState();

                foreach (var prop in _propInfo)
                {
                    string item = (string)prop.GetValue(_model);

                    canvas.TextHorizontalScaling = 70;
                    canvas.DrawText(item, new PdfRectangle(120, PointTwo, 400, 400), PdfTextAlign.Left, PdfVerticalAlign.Top);
                    PointTwo = PointTwo + 14;
                }

                canvas.CurrentPosition = new PdfPoint(20, 803);
                canvas.DrawLineTo(575, 803);

                canvas.SaveState();

                canvas.FontSize = 7;
                canvas.Font = builtInFont;
                canvas.Brush.Color = new PdfGrayColor(60);
                canvas.DrawString("Copyright © VOLO, 2016. All rights reserved. ", new PdfRectangle(40, 810, 400, 400), PdfTextAlign.Left, PdfVerticalAlign.Top);

                canvas.RestoreState();

                pdf.Save(pathToFile);
            }
        }

        /// <summary>
        /// Cleans the directory that contains the files .pdf
        /// </summary>
        /// <param name="sourceDirectory">It contains the address of the directory with the files to clean</param>
        public void CleanFolder(string sourceDirectory)
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
    /// <summary>
    /// 
    /// </summary>
    public enum FontSize
    {
        Eleven = 1,
        Fourteen = 2
    }
}

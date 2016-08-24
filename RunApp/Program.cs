using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PdfModule;
using PdfMock;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.Content;
using BitMiracle.Docotic.Pdf;
using System.Reflection;

namespace RunApp
{
    public class Program
    {
        private const double TableWidth = 400.0;
        private const double RowHeight = 30.0;
        private static readonly PdfPoint m_leftTableCorner = new PdfPoint(10, 50);
        private static readonly double[] m_columnWidths = new double[3] { 100, 100, 200 };

        private static Type type = _model.GetType();
        static PropertyInfo[] infoes = type.GetProperties();

        static MockModel _model = new MockModel
        {
            Sender = "Jon Dou",
            Recepient = "Alexey Stupakov",
            Message = "Some order to do. Do it better"
        };
        public static void Main(string[] args)
        {
            //string tmp;
            //PdfCreator crt = new PdfCreator("temp/");
            //crt.CreatePdf(_model, out tmp);

            //crt.CleanFolder(@"temp/");

            

            string pathToFile = "Tables.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                drawHeader(canvas);
                drawTableBody(canvas);

                pdf.Save(pathToFile);
            }
        }

        private static void drawHeader(PdfCanvas canvas)
        {
            canvas.SaveState();
            canvas.Brush.Color = new PdfGrayColor(75);
            PdfRectangle headerBounds = new PdfRectangle(m_leftTableCorner, new PdfSize(TableWidth, RowHeight));
            canvas.DrawRectangle(headerBounds, PdfDrawMode.FillAndStroke);

            PdfRectangle[] cellBounds = new PdfRectangle[3]
            {
                new PdfRectangle(m_leftTableCorner.X, m_leftTableCorner.Y, m_columnWidths[0], RowHeight),
                new PdfRectangle(m_leftTableCorner.X + m_columnWidths[0], m_leftTableCorner.Y, m_columnWidths[1], RowHeight),
                new PdfRectangle(m_leftTableCorner.X + m_columnWidths[0] + m_columnWidths[1], m_leftTableCorner.Y, m_columnWidths[2], RowHeight)
            };

            for (int i = 1; i <= 2; ++i)
            {
                canvas.CurrentPosition = new PdfPoint(cellBounds[i].Left, cellBounds[i].Top);
                canvas.DrawLineTo(canvas.CurrentPosition.X, canvas.CurrentPosition.Y + RowHeight);
            }

            canvas.Brush.Color = new PdfGrayColor(0);
            for (int i = 0; i < infoes.Length - 1; i++)
            {
                canvas.DrawString(infoes[i].Name, cellBounds[i], PdfTextAlign.Center, PdfVerticalAlign.Center);
            }
            

            canvas.RestoreState();
        }

        private static void drawTableBody(PdfCanvas canvas)
        {
            PdfPoint bodyLeftCorner = new PdfPoint(m_leftTableCorner.X, m_leftTableCorner.Y + RowHeight);
            PdfRectangle firstCellBounds = new PdfRectangle(bodyLeftCorner, new PdfSize(m_columnWidths[0], RowHeight * 2));
            canvas.DrawRectangle(firstCellBounds);
            canvas.DrawString("Docotic.Pdf", firstCellBounds, PdfTextAlign.Center, PdfVerticalAlign.Center);

            PdfRectangle[,] cells = new PdfRectangle[2, 2]
            {
                {
                    new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0], bodyLeftCorner.Y, m_columnWidths[1], RowHeight),
                    new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0] + m_columnWidths[1], bodyLeftCorner.Y, m_columnWidths[2], RowHeight)
                },
                {
                    new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0], bodyLeftCorner.Y + RowHeight, m_columnWidths[1], RowHeight),
                    new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0] + m_columnWidths[1], bodyLeftCorner.Y + RowHeight, m_columnWidths[2], RowHeight)
                }
            };

            foreach (PdfRectangle rect in cells)
                canvas.DrawRectangle(rect, PdfDrawMode.Stroke);

            canvas.DrawString("Application License", cells[0, 0], PdfTextAlign.Center, PdfVerticalAlign.Center);
            canvas.DrawString("For end-user applications", cells[0, 1], PdfTextAlign.Center, PdfVerticalAlign.Center);
            canvas.DrawString("Server License", cells[1, 0], PdfTextAlign.Center, PdfVerticalAlign.Center);
            canvas.DrawString("For server-based services", cells[1, 1], PdfTextAlign.Center, PdfVerticalAlign.Center);
        }
    }
}

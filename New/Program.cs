using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PdfModule;
using PdfMock;
using BitMiracle.Docotic.Pdf;
using System.Reflection;

namespace New
{
    public class Program
    {
        private const double TableWidth = 400.0;
        private const double RowHeight = 30.0;
        private const double RowHeightBody = 200.0;
        private static readonly PdfPoint m_leftTableCorner = new PdfPoint(100, 150);
        private static readonly double[] m_columnWidths = new double[3] { 100, 100, 100 };
        private static int counter = 0;
        

        static MockModel _model = new MockModel
        {
            Sender = "Jon Dou",
            Recepient = "Alexey Stupakov",
            Message = "Some order to do. Do it better So Soapy left his seat, and walked slowly along the street.Soon he came to a bright restaurant on Broadway.Ah!This was all right.He just had to get to a table in the restaurant and sit down.That was all"
        };
        public static void Main(string[] args)
        {
            string tmp;
            PdfCreator crt = new PdfCreator("temp/");
            crt.CreatePdf(_model, out tmp);

            //crt.CleanFolder(@"temp/");

            //string pathToFile = @"Temp/Tables.pdf";

            //using (PdfDocument pdf = new PdfDocument())
            //{
            //    PdfCanvas canvas = pdf.Pages[0].Canvas;
            //    drawHeader(canvas);
            //    drawTableBody(canvas);

            //    pdf.Save(pathToFile);
            //}

            //string pathToFile = "GraphicsState.pdf";

            //using (PdfDocument pdf = new PdfDocument())
            //{
            //    PdfCanvas canvas = pdf.Pages[0].Canvas;
            //    canvas.SaveState();

            //    canvas.Pen.Width = 3;
            //    canvas.Pen.DashPattern = new PdfDashPattern(new double[] { 5, 3 }, 2);

            //    canvas.CurrentPosition = new PdfPoint(20, 60);
            //    canvas.DrawLineTo(150, 60);

            //    canvas.RestoreState();

            //    canvas.CurrentPosition = new PdfPoint(20, 80);
            //    canvas.DrawLineTo(150, 80);

            //    pdf.Save(pathToFile);
            //}

        }

        //private static void drawHeader(PdfCanvas canvas)
        //{
        //    Type type = _model.GetType();
        //    PropertyInfo[] infoes = type.GetProperties();

        //    canvas.SaveState();
        //    canvas.Brush.Color = new PdfGrayColor(75);
        //    PdfRectangle headerBounds = new PdfRectangle(m_leftTableCorner, new PdfSize(TableWidth, RowHeight));
        //    canvas.DrawRectangle(headerBounds, PdfDrawMode.FillAndStroke);

        //    PdfRectangle[] cellBounds = new PdfRectangle[3]
        //    {

        //        new PdfRectangle(m_leftTableCorner.X, m_leftTableCorner.Y, m_columnWidths[0], RowHeight),
        //        new PdfRectangle(m_leftTableCorner.X + m_columnWidths[0], m_leftTableCorner.Y, m_columnWidths[1], RowHeight),
        //        new PdfRectangle(m_leftTableCorner.X + m_columnWidths[0] + m_columnWidths[1], m_leftTableCorner.Y, m_columnWidths[1], RowHeight)
        //    };

        //    //for (int i = 1; i <= 2; ++i)
        //    //{
        //    //    canvas.CurrentPosition = new PdfPoint(cellBounds[i].Left, cellBounds[i].Top);
        //    //    canvas.DrawLineTo(canvas.CurrentPosition.X, canvas.CurrentPosition.Y + RowHeight);
        //    //}

        //    canvas.Brush.Color = new PdfGrayColor(0);
        //    for (int i = 0; i < infoes.Length; i++)
        //    {
        //        canvas.DrawString(infoes[i].Name, cellBounds[i], PdfTextAlign.Center, PdfVerticalAlign.Center);
        //    }


        //    canvas.RestoreState();
        //}

        //private static void drawTableBody(PdfCanvas canvas)
        //{
        //    Type type = _model.GetType();
        //    PropertyInfo[] infoes = type.GetProperties();

        //    PdfPoint bodyLeftCorner = new PdfPoint(m_leftTableCorner.X, m_leftTableCorner.Y + RowHeight);
        //    PdfRectangle firstCellBounds = new PdfRectangle(bodyLeftCorner, new PdfSize(m_columnWidths[1], RowHeightBody));
        //    canvas.DrawRectangle(firstCellBounds);

        //    PdfRectangle[] cellBounds = new PdfRectangle[3]
        //    {

        //        new PdfRectangle(bodyLeftCorner.X, bodyLeftCorner.Y, m_columnWidths[1], RowHeightBody),
        //        new PdfRectangle(bodyLeftCorner.X + m_columnWidths[1], bodyLeftCorner.Y, m_columnWidths[2], RowHeightBody),
        //        new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0] + m_columnWidths[1], bodyLeftCorner.Y, m_columnWidths[2], RowHeightBody)
        //    };


        //    foreach (var prop in infoes)
        //    {
        //        string item = (string)prop.GetValue(_model);
        //        canvas.DrawText(item, cellBounds[counter], PdfTextAlign.Center, PdfVerticalAlign.Top);
        //        counter++;
        //    }

        //}
    }
}

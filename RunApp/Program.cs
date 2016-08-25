using PdfModule;
using PdfMock;


namespace RunApp
{
    public class Program
    {
        static string tmp;
        static MockModel _model = new MockModel
        {
            Sender = "Jon Dou",
            Recepient = "Body tail",
            Message = "Some order to do. Do it better So Soapy left his seat, and walked slowly along the street.Soon he came to a bright restaurant on Broadway."
        };
        public static void Main(string[] args)
        {
            PdfCreator<MockModel> tst = new PdfCreator<MockModel>("");
            tst.CreatePdf(_model, out tmp, FontSize.Eleven);
        }
    }
}

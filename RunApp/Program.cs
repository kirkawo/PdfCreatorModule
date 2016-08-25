using PdfModule;
using PdfMock;


namespace RunApp
{
    public class Program
    {
        static MockModel _model = new MockModel
        {
            Sender = "Jon Dou",
            Recepient = "Body tail",
            Message = "Some order to do. Do it better So Soapy left his seat, and walked slowly along the street.Soon he came to a bright restaurant on Broadway."
        };
        public static void Main(string[] args)
        {
            HtmlToPdfCreator<MockModel> tst = new HtmlToPdfCreator<MockModel>(_model);
            tst.SaveToFolder();
        }
    }
}

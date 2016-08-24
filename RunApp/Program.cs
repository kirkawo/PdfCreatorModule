using PdfModule;
using PdfMock;


namespace RunApp
{
    public class Program
    {
        static MockModel _model = new MockModel
        {
            Sender = "Jon Dou",
            Recepient = "Alexey Stupakov",
            Message = "Some order to do. Do it better So Soapy left his seat, and walked slowly along the street.Soon he came to a bright restaurant on Broadway.Ah!This was all right.He just had to get to a table in the restaurant and sit down.That was all"
        };
        public static void Main(string[] args)
        {
            string tmp;
            PdfCreator<MockModel> crt = new PdfCreator<MockModel>(_model, "temp/");
            crt.CreatePdf(out tmp, FontSize.Fourteen);

            //crt.CleanFolder(@"temp/");
        }
    }
}

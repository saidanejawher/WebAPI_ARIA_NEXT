using System.IO;
using CoreHtmlToImage;
using PdfSharp.Drawing;
//using PdfSharp.Pdf;

public /*sealed*/ class PdfHelper
{
    //private PdfHelper()
    //{
    //}

    //public static PdfHelper Instance { get; } = new PdfHelper();

    internal void SaveImageAsPdf(string imageFileName, string pdfFileName, int width = 600, bool deleteImage = false)
    {
        using (var document = new PdfSharp.Pdf.PdfDocument())
        {
            PdfSharp.Pdf.PdfPage page = document.AddPage();
            using (XImage img = XImage.FromFile(imageFileName))
            {
                // Calculate new height to keep image ratio
                var height = (int)(((double)width / (double)img.PixelWidth) * img.PixelHeight);

                // Change PDF Page size to match image
                page.Width = width;
                page.Height = height;

                XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.DrawImage(img, 0, 0, width, height);
            }
            var PtPosition = imageFileName.LastIndexOf('.');
            var PathTemp = imageFileName.Remove(PtPosition);
            var pathPdfFile = PathTemp + ".pdf";
            document.Save(pathPdfFile/*pdfFileName*/);
        }

        if (deleteImage)
            File.Delete(imageFileName);
    }



    internal  void GetMap()
    {
        //string html = File.ReadAllText(@"C:\Work\Upsideo.Agregateur\Templates\Exemlpe.html");
        string html = File.ReadAllText(@"C:\Work\Upsideo.Agregateur\Templates\Exemple2.html");
        var converter = new HtmlConverter();
        var bytes = converter.FromHtmlString(html);
        File.WriteAllBytes(@"C:\Work\Upsideo.Agregateur\Templates\HTMLtoImage.jpg", bytes);




    }

}
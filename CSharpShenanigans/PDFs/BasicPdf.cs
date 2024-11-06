using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace CSharpShenanigans.PDFs;

public static class BasicPdf
{
    public static void CreateBasicPdf()
    {
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Content()
                    .Padding(50)
                    .Text(text => { text.Span("Hello, World!").FontColor(Colors.Red.Accent4); });
            });
        }).ShowInCompanion();
    }

    public static void SaveBasicPdf()
    {
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Content()
                    .Padding(50)
                    .Text(text => { text.Span("Hello, World!").FontColor(Colors.Red.Accent4); });
            });
        }).GeneratePdf("C:\\temp\\example.pdf");
    }

    public static byte[] GetPdfBytes()
    {
        byte[] pdfBytes;

        using (var stream = new MemoryStream())
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Content()
                        .Padding(50)
                        .Text(text => { text.Span("Hello, World!").FontColor(Colors.Red.Accent4); });
                });
            }).GeneratePdf(stream);

            pdfBytes = stream.ToArray();
        }

        return pdfBytes;
    }
}

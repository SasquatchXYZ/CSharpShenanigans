using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CSharpShenanigans.PDFs;

public static class ExamplePdfs
{
    public static void ExamplePdf1()
    {
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.Letter);
                page.Margin(1, Unit.Inch);
                page.PageColor(Colors.Grey.Lighten3);

                page.Content()
                    .Column(column =>
                    {
                        column.Spacing(15);
                        var randomNumber = Random.Shared.Next(0, 2);
                        bool includeSection = randomNumber is 0;
                        if (includeSection)
                        {
                            column.Item().Text("I am the one who knocks.")
                                .Bold()
                                .FontSize(69)
                                .FontColor(Colors.Grey.Darken1);
                        }

                        column.Item().Row(row =>
                        {
                            row.RelativeItem()
                                .Text("Are you talking to me?!")
                                .FontSize(18);

                            row.ConstantItem(100)
                                .Image(Placeholders.Image(420, 690));
                        });
                    });
            });
        }).ShowInCompanion();
    }

    public static void ExamplePdf2()
    {
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header()
                    .Text("I am the head")
                    .SemiBold()
                    .FontSize(30)
                    .FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);
                        x.Item().Text(Placeholders.LoremIpsum());
                        x.Item().Image(Placeholders.Image(200, 100));
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        }).ShowInCompanion();
    }
}

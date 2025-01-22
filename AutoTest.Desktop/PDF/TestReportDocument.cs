using AutoTest.BLL.DTOs.Tests.Test;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace AutoTest.Desktop.PDF;

public class TestReportDocument : IDocument
{
    private readonly TestDto _test;
    public TestReportDocument(TestDto test)
    {
        this._test = test;
    }

    public DocumentMetadata GetMetaData()
        => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(20);

                page.Header()
                    .Text($"{_test.Title}")
                    .FontSize(18)
                    .Bold()
                    .AlignCenter();

                page.Content()
                    .PaddingVertical(10)
                    .Column(column =>
                    {
                        column
                            .Item()
                            .Text($"Daraja: {_test.Level.ToString()}\tYaratuvchi: {_test.User.Firstname + _test.User.Lastname}")
                            .FontSize(12);

                        column
                            .Item()
                            .PaddingVertical(5);

                        foreach(var question in _test.Question)
                        {
                            column
                                .Item()
                                .Text($"{question.Problem}")
                                .FontSize(14)
                                .SemiBold();

                            column
                                .Item()
                                .Column(optionsColumn =>
                                {
                                    foreach(var option in question.Options)
                                    {
                                        optionsColumn
                                            .Item()
                                            .Text($"{option.Text}")
                                            .FontSize(12);
                                    }
                                });

                            column
                                .Item()
                                .PaddingVertical(10);
                        }
                    });

                page
                    .Footer()
                    .AlignCenter()
                    .Text("AutoTest PDF Test")
                    .FontSize(10);  
            });
    }
}

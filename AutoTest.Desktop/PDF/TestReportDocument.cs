using AutoTest.BLL.DTOs.Tests.Test;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace AutoTest.Desktop.PDF;

public class TestReportDocument : IDocument
{
    private readonly TestDto _test;
    private readonly string[] characters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
    public TestReportDocument(TestDto test)
    {
        this._test = test;
    }

    public DocumentMetadata GetMetaData()
        => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        int QuestionNumber = 1;

        container
            .Page(page =>
            {
                page.Margin(20);

                page.Header()
                    .ShowOnce()
                    .Element(header =>
                    {
                        header
                            .Text($"{_test.Title}")
                            .FontSize(24)
                            .FontColor("#1d3557")
                            .Bold()
                            .AlignCenter();
                    });

                page.Content()
                    .PaddingVertical(10)
                    .Column(column =>
                    {
                        column
                             .Item()
                             .Text("AutoTest orqali yaratilgan")
                             .FontSize(9)
                             .FontColor("#888888")
                             .AlignCenter();

                        column
                            .Item()
                            .ShowOnce()
                            .Text($"Yaratuvchi - {_test.User.Firstname} {_test.User.Lastname}\nAsosiy mavzu - {_test.Topics.First()?.Name ?? "Tanlanmagan"}\nDaraja - {_test.Level.ToString()}")
                            .FontSize(9)
                            .FontColor("#888888") 
                            .AlignRight();

                        column
                            .Item()
                            .PaddingVertical(5);

                        foreach (var question in _test.Question)
                        {
                            column
                                .Item()
                                .Element(container =>
                                {
                                    container
                                        .Column(questionColumn =>
                                        {
                                            questionColumn
                                                .Item()
                                                .Text($"{QuestionNumber}) {question.Problem}")
                                                .FontSize(14)
                                                .SemiBold();

                                            questionColumn
                                                .Item()
                                                .PaddingVertical(5);

                                            questionColumn
                                                .Item()
                                                .Column(optionsColumn =>
                                                {
                                                    for (int i = 0; i < question.Options.Count; i++)
                                                    {
                                                        var option = question.Options[i];
                                                        optionsColumn
                                                            .Item()
                                                            .Text($"    {characters[i]}) {option.Text}")
                                                            .FontSize(11);
                                                    }
                                                });
                                        });
                                });

                            column
                                .Item()
                                .PaddingVertical(10);

                            QuestionNumber++;
                        }
                    });

                page
                    .Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.CurrentPageNumber();
                            x.Span(" / ");
                            x.TotalPages();
                        });
            });
    }
}

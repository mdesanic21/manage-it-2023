using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BusinessLogicLayer.Generator
{
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    // Using QuestPDF NuGet package for PDF generating. Regards to the QuestPDF team, and here is the template for the PDF generation, and a link for their project on the GitHub
    // Template: https://github.com/QuestPDF/QuestPDF-ExampleInvoice
    // QuestPDF on GitHub: https://github.com/QuestPDF/QuestPDF
    public class ReportGenerator : IDocument
    {
        public ReportModel reportNew { get;  }
        public List<ReportView> reportViewList { get; }

        // Had to get data in the constructor and bind it to the properties because I wanted this generator to only create a PDF file, not to fetch any data
        public ReportGenerator(ReportModel reportModel, List<ReportView> reportViewListFetched)
        {
            reportNew = reportModel;
            reportViewList = reportViewListFetched;
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
                });
        }

        void ComposeHeader(QuestPDF.Infrastructure.IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column
                        .Item().Text($"Report #{reportNew.ID_Report}")
                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                    column.Item().Text(text =>
                    {
                        text.Span("First date: ").SemiBold();
                        text.Span($"{reportNew.startDate:d}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Last date: ").SemiBold();
                        text.Span($"{reportNew.finishDate:d}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Creator: ");
                        text.Span($"{reportNew.creatorWorker.FirstName} {reportNew.creatorWorker.LastName}").Italic();
                    });
                });
            });
        }

        void ComposeContent(QuestPDF.Infrastructure.IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(20);
                column.Item().Element(ComposeTable);
            });
        }

        void ComposeTable(QuestPDF.Infrastructure.IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold();
            var contentStyle = TextStyle.Default.Size(10);

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(5);
                    columns.RelativeColumn(25);
                    columns.RelativeColumn(10);
                    columns.RelativeColumn(15);
                    columns.RelativeColumn(30);
                    columns.RelativeColumn(15);
                });

                table.Header(header =>
                {
                    header.Cell().Text("#");
                    header.Cell().Text("Work type").Style(headerStyle);
                    header.Cell().Text("Worker").Style(headerStyle);
                    header.Cell().Text("Client").Style(headerStyle);
                    header.Cell().Text("Address").Style(headerStyle);
                    header.Cell().Text("Date").Style(headerStyle);

                    header.Cell().ColumnSpan(6).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });

                QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer styleContainer) => styleContainer.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);


                foreach (var item in reportViewList)
                {
                    var index = reportViewList.IndexOf(item);

                    table.Cell().Element(CellStyle).Text($"{index}").Style(contentStyle);
                    table.Cell().Element(CellStyle).Text($"{item.WorkType}").Style(contentStyle);
                    table.Cell().Element(CellStyle).Text($"{item.Worker}").Style(contentStyle);
                    table.Cell().Element(CellStyle).Text($"{item.Client}").Style(contentStyle);
                    table.Cell().Element(CellStyle).Text($"{item.Address}").Style(contentStyle);
                    table.Cell().Element(CellStyle).Text($"{item.Date}").Style(contentStyle);
                }
            });
        }

        public DocumentSettings GetSettings()
        {
            return new DocumentSettings
            {
                ContentDirection = QuestPDF.Infrastructure.ContentDirection.LeftToRight
            };
        }
    }
}


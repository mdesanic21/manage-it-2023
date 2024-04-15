using EntitiesLayer.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusinessLogicLayer.Generator
{
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    // Using QuestPDF NuGet package for PDF generating. Regards to the QuestPDF team, and here is the template for the PDF generation, and a link for their project on the GitHub
    // Template: https://github.com/QuestPDF/QuestPDF-ExampleInvoice
    // QuestPDF on GitHub: https://github.com/QuestPDF/QuestPDF
    public class RecieptGenerator: IDocument
    {
        public Receipt receiptNew { get; set; }
        public WorkOrder workOrderForReceipt { get; set; }
        public bool isR1 { get; set; }

        // Catching the data for the generation. Because this is an IDocument item, I decided to just forward the data to it, not to fetch it over here.
        public RecieptGenerator(Receipt receipt, WorkOrder workOrder, bool R1) 
        {
            isR1 = R1;
            receiptNew = receipt;
            workOrderForReceipt = workOrder;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.ContinuousSize(3, QuestPDF.Infrastructure.Unit.Inch);
                    page.MarginVertical(5);
                    page.MarginHorizontal(10);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().Element(ComposeFooter);

                });
        }

        void ComposeHeader(QuestPDF.Infrastructure.IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column
                        .Item().Text($"Receipt #{receiptNew.ID_receipt}")
                        .FontSize(14).SemiBold();

                    column.Item().Text(text =>
                    {
                        text.Span("Date of service: ");
                        text.Span($"{workOrderForReceipt.OrderDetail.Date:d}");
                    });
                    column.Item().Text(text =>
                    {
                        text.Span("Receipt type: ");
                        if (isR1)
                        {
                            text.Span("R1");
                        }
                        else text.Span("Normal");
                        
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
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Text("#");
                    header.Cell().Text("Work type").Style(headerStyle);
                    header.Cell().Text("Price").Style(headerStyle);

                    header.Cell().ColumnSpan(3).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });

                QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer styleContainer) => styleContainer.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);

                    table.Cell().Element(CellStyle).Text("1").Style(contentStyle);
                   
                    table.Cell().Element(CellStyle).Text($"{workOrderForReceipt.OrderDetail.WorkType.Name}").Style(contentStyle);

                if (isR1)
                {
                    decimal newPrice = workOrderForReceipt.OrderDetail.WorkType.Price * 0.75m;
                    table.Cell().Element(CellStyle).Text($"{Math.Round(newPrice, 2)}").Style(contentStyle);
                }else table.Cell().Element(CellStyle).Text($"{Math.Round(workOrderForReceipt.OrderDetail.WorkType.Price, 2)}").Style(contentStyle);
            });
        }

        void ComposeFooter(QuestPDF.Infrastructure.IContainer container)
        {
            var contentStyle = TextStyle.Default.Size(10);
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.Span("Creator: ID ").Style(contentStyle);
                        text.Span($"{receiptNew.ID_Worker}").Style(contentStyle);
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Date of creation: ").Style(contentStyle);
                        text.Span($"{receiptNew.Date}").Style(contentStyle);
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Company OIB: ").Style(contentStyle);
                        text.Span($"{receiptNew.OIB}").Style(contentStyle);
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Additional information: ").Style(contentStyle);
                        text.Span($"{receiptNew.Additional_info}").Style(contentStyle);
                    });
                });
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

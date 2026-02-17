using Application.DTOs;
using Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.Services;

public class PdfService : IPdfService
{
    public PdfService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public byte[] GenerateEmployeeListPdf(List<EmployeeDto> employees)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header()
                    .Text("Employee List Report")
                    .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(1.5f);
                            columns.RelativeColumn(1.5f);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Name");
                            header.Cell().Element(CellStyle).Text("NID");
                            header.Cell().Element(CellStyle).Text("Phone");
                            header.Cell().Element(CellStyle).Text("Department");
                            header.Cell().Element(CellStyle).Text("Salary");

                            static IContainer CellStyle(IContainer container)
                            {
                                return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                            }
                        });

                        foreach (var emp in employees)
                        {
                            table.Cell().Element(CellStyle).Text(emp.Name);
                            table.Cell().Element(CellStyle).Text(emp.NID);
                            table.Cell().Element(CellStyle).Text(emp.Phone);
                            table.Cell().Element(CellStyle).Text(emp.Department);
                            table.Cell().Element(CellStyle).Text($"৳{emp.BasicSalary:N2}");

                            static IContainer CellStyle(IContainer container)
                            {
                                return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                            }
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.Span(" of ");
                        x.TotalPages();
                    });
            });
        });

        return document.GeneratePdf();
    }

    public byte[] GenerateEmployeeCvPdf(EmployeeDto employee)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .Text($"Employee CV - {employee.Name}")
                    .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Spacing(10);

                        // Personal Information
                        column.Item().Text("Personal Information").SemiBold().FontSize(16);
                        column.Item().PaddingLeft(10).Column(col =>
                        {
                            col.Item().Text($"Name: {employee.Name}");
                            col.Item().Text($"NID: {employee.NID}");
                            col.Item().Text($"Phone: {employee.Phone}");
                            col.Item().Text($"Department: {employee.Department}");
                            col.Item().Text($"Basic Salary: ৳{employee.BasicSalary:N2}");
                        });

                        // Spouse Information
                        if (employee.Spouse != null)
                        {
                            column.Item().PaddingTop(10).Text("Spouse Information").SemiBold().FontSize(16);
                            column.Item().PaddingLeft(10).Column(col =>
                            {
                                col.Item().Text($"Name: {employee.Spouse.Name}");
                                col.Item().Text($"NID: {employee.Spouse.NID}");
                            });
                        }

                        // Children Information
                        if (employee.Children.Any())
                        {
                            column.Item().PaddingTop(10).Text("Children").SemiBold().FontSize(16);
                            column.Item().PaddingLeft(10).Column(col =>
                            {
                                foreach (var child in employee.Children)
                                {
                                    col.Item().Text($"• {child.Name} (DoB: {child.DateOfBirth:dd MMM yyyy})");
                                }
                            });
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text($"Generated on {DateTime.Now:dd MMM yyyy HH:mm}");
            });
        });

        return document.GeneratePdf();
    }
}

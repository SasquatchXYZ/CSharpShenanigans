// See https://aka.ms/new-console-template for more information

using CSharpShenanigans.EventSourcing;
using CSharpShenanigans.PDFs;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

Console.WriteLine("Hello, World!");

QuestPDF.Settings.License = LicenseType.Community;

ExamplePdfs.ExamplePdf2();

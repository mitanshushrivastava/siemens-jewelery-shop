using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using JeweleryShopApi.Models;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace JeweleryShopApi.Helpers
{
    public static class PdfHelper
    {
        public static MemoryStream GetPdfInvoice(this InvoiceRequest invoiceRequest, bool isDiscountApplicable)
        {
            var totalPrice = invoiceRequest.PricePerGram * invoiceRequest.Weight;
            var finalPrice =  totalPrice - ((totalPrice * invoiceRequest.Discount)/100);
            var lineOffset = 0;
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            graphics.DrawString("Jewelery Shop Invoice", font, PdfBrushes.Red, new PointF(0, lineOffset));
            //Creates a font for adding the heading in the page
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            graphics.DrawString($"Customer Name - {invoiceRequest.CustomerName}", subHeadingFont, PdfBrushes.Black, new PointF(0, lineOffset += 30));
            graphics.DrawString("---------------------------------------------------------", subHeadingFont, PdfBrushes.Black, new PointF(0, lineOffset += 30));
            graphics.DrawString($"Price Per Gram - {invoiceRequest.PricePerGram} INR", subHeadingFont, PdfBrushes.BlueViolet, new PointF(0, lineOffset += 30));
            graphics.DrawString($"Weight - {invoiceRequest.Weight} Grams", subHeadingFont, PdfBrushes.BlueViolet, new PointF(0, lineOffset += 30));
            
            if(isDiscountApplicable)
            {
                graphics.DrawString($"Discount Offered - {invoiceRequest.Discount} %", subHeadingFont, PdfBrushes.BlueViolet, new PointF(0, lineOffset += 30));
                invoiceRequest.Discount = 0;
            }

            graphics.DrawString("---------------------------------------------------------", subHeadingFont, PdfBrushes.Black, new PointF(0, lineOffset+= 30));
            graphics.DrawString($"Aount Payable - {finalPrice} INR", subHeadingFont, PdfBrushes.Green, new PointF(0, lineOffset += 45));
            
            
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            document.Close();
            return stream;
        }
    }
}
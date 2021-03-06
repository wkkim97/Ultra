using HiQPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Template_HtmlToPdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // create the HTML to PDF converter
        HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

        // set browser width
        htmlToPdfConverter.BrowserWidth = 1200;

        // set browser height if specified, otherwise use the default
        htmlToPdfConverter.BrowserHeight = 800;

        // set HTML Load timeout
        htmlToPdfConverter.HtmlLoadedTimeout = 120;

        // set PDF page size and orientation
        htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
        htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Landscape; // GetSelectedPageOrientation();

        // set PDF page margins
        htmlToPdfConverter.Document.Margins = new PdfMargins(0);

        // set a wait time before starting the conversion
        htmlToPdfConverter.WaitBeforeConvert = 2;

        // convert HTML to PDF
        byte[] pdfBuffer = null;


        //// convert URL to a PDF memory buffer
        //string url = @"http://localhost:4042/ultra/Template/HtmlPage.html";
        //pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);

  
        // convert HTML code
        string htmlCode = @"<html>
                            <head><link href='../Styles/Css/style.css' rel='stylesheet' />
                            </head>
                                 < body style='background-color:red;' >
                              <a id='github' style='border: 0px currentColor; border-image: none; top: 0px; right: 0px; position: absolute;' href='https://github.com/szimek/signature_pad'>
                                <img alt='Fork me on GitHub' src='https://s3.amazonaws.com/github/ribbons/forkme_right_gray_6d6d6d.png'>
                              </a>

                              <div class='signature-pad'>
                                <div class='signature-pad--body'>
                                  <canvas id='signature-pad' style='-ms-touch-action: none; touch-action: none;'></canvas>
                                </div>
                                <div class='signature-pad--footer'>
                                  <div class='description'>Sign above</div>

                                  <div class='signature-pad--actions'>
                                    <div>
                                      <button class='button clear' type='button' data-action='clear'>Clear</button>
                                      <button class='button' type='button' data-action='change-color'>Change color</button>
                                      <button class='button' type='button' data-action='undo'>Undo</button>

                                    </div>
                                    <div>
                                      <button class='button save' type='button' data-action='save-png'>Save as PNG</button>
                                      <button id='save-jpg' type='button'>Save as JPG</button>
                                      <button class='button save' type='button' data-action='save-svg'>Save as SVG</button>
                                    </div>
                                  </div>
                                </div>
                                  <img id='imgSigin' src='data:image/jpeg;base64,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'>
                              </div> 
                            </body></html>";
        string baseUrl = "";

        // convert HTML code to a PDF memory buffer
        pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);
    

        // inform the browser about the binary data format
        HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

        // let the browser know how to open the PDF document, attachment or inline, and the file name
        HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename=HtmlToPdf.pdf; size={1}", "attachment", pdfBuffer.Length.ToString()));

        // write the PDF buffer to HTTP response
        HttpContext.Current.Response.BinaryWrite(pdfBuffer);

        // call End() method of HTTP response to stop ASP.NET page processing
        HttpContext.Current.Response.End();
    }
 
}
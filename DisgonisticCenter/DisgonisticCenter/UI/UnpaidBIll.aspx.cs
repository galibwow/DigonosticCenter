using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DisgonisticCenter.BLL;
using DisgonisticCenter.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace DisgonisticCenter.UI
{
    public partial class UnpaidBIll : System.Web.UI.Page
    {

        TestRequestEntry testRequest=new TestRequestEntry();
        TestRequestManager testRequestmanager=new TestRequestManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                loadUnpaidgridview();
                //pdfButton.Visible = false;

            }
        }

        protected void showButton_Click(object sender, EventArgs e)
        {
            try
            {
                string startDate = startDateTimepicker.Value;
                string endDate = endDateTimePicker.Value;


                if (startDate == String.Empty || endDate == String.Empty)
                {
                    messageLabel.Text = "Please select both date";
                    return;
                }

                LoadUnpaidGridViewbydate(startDate, endDate);
            }

            catch (Exception exception)
            {
                messageLabel.Text = exception.Message;

                //generatePdfButton.Visible = false;
                unpaidGridView.Visible = false;
            }
        }

        private void LoadUnpaidGridViewbydate(string startDate, string endDate)
        {
            unpaidGridView.DataSource = testRequestmanager.GeTestRequestsByDate(startDate, endDate);
            unpaidGridView.DataBind();
           
        }

        
        private void loadUnpaidgridview()
        {

            List<TestRequest> testRequestsList = testRequestmanager.GetAllTestRequests();
            if (testRequestsList!=null)
            {
                unpaidGridView.DataSource = testRequestsList;
                unpaidGridView.DataBind();
              
            }
            else
            {
                messageLabel.Text = "No Data found";
                messageLabel.ForeColor=Color.Crimson;
            }
        
        }

        decimal tot = 0;
        int serialNo = 0;
        protected void unpaidGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (serialNo += 1).ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "BillNo").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "MobileNo").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "NameOfThePatient").ToString();
                e.Row.Cells[4].Text = DataBinder.Eval(e.Row.DataItem, "PaymentStatus").ToString();


                tot = tot + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PaymentStatus"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "";
                e.Row.Cells[3].Text = "Total Amount: ";
                e.Row.Cells[4].Text = tot.ToString();
            }
        }

        protected void pdfButton_Click(object sender, EventArgs e)
        {
            GeneratePDF();
        }

        private void GeneratePDF()
        {

            int columnsCount = unpaidGridView.HeaderRow.Cells.Count;
            // Create the PDF Table specifying the number of columns


            PdfPTable pdfTable = new PdfPTable(columnsCount);



            // Loop thru each cell in GrdiView header row
            foreach (TableCell gridViewHeaderCell in unpaidGridView.HeaderRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                //// Set the font color to GridView header row font color
                //font.Color = new BaseColor(unpaidGridView.HeaderStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(unpaidGridView.HeaderStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }

            // Loop thru each datarow in GrdiView
            foreach (GridViewRow gridViewRow in unpaidGridView.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    // Loop thru each cell in GrdiView data row
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        //Font font = new Font();
                        //font.Color = new BaseColor(unpaidGridView.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text));

                        pdfCell.BackgroundColor = new BaseColor(unpaidGridView.RowStyle.BackColor);

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }



            foreach (TableCell gridViewHeaderCell in unpaidGridView.FooterRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                // Set the font color to GridView header row font color
                //font.Color = new BaseColor(unpaidGridView.FooterStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(unpaidGridView.FooterStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }



            // Create the PDF document specifying page size and margins
            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);

            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            string centerName = "                                                         Diagnostic Center";
            string reportName = "                                                               Unpaid Bill Report";


            pdfDocument.Open();

            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph(DateTime.Now.ToString()));
            //pdfDocument.Add(new Paragraph(centerName));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph("\t" + reportName));
            pdfDocument.Add(new Paragraph(" \n\n"));



            pdfDocument.Add(pdfTable);
            pdfDocument.Close();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition",
                "attachment;filename=UnpaidBillList.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        
    }
}
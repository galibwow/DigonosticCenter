using System;
using System.Collections.Generic;
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
    public partial class DateWiseTestTypeReport : System.Web.UI.Page
    {
        TypeWiseReportManager typeManager = new TypeWiseReportManager();
        List<DateWiseTestTypeVm> testReportList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                messageLabel.Text = "";

            }

            pdfButton.Visible = false;
        }


        protected void showButton_Click(object sender, EventArgs e)
        {
            try
            {



                string startDate = startDateTimepicker.Value;
                string endDate = endDateTimePicker.Value;

                testReportList = typeManager.GetDateWiseTypeReport(startDate, endDate);

                if (startDate == String.Empty || endDate == String.Empty)
                {
                    messageLabel.Text = "Please select both date";
                    return;
                }
                loadGridview(testReportList);
            }


            catch (Exception exception)
            {
                //generatePdfButton.Visible = false;

                messageLabel.Text = exception.Message;

                TestsGridView.Visible = false;
                // LoadEmtyGridView();

            }
        }

        private void loadGridview(List<DateWiseTestTypeVm> testReportList)
        {

            List<DateWiseTestTypeVm> testReportListNot = typeManager.GetDateWiseTypeReportNot();

            foreach (var v in testReportListNot)
            {
                testReportList.Add(v);
            }


            if (testReportList.Count > 0)
            {

                TestsGridView.DataSource = testReportList;
                TestsGridView.DataBind();
                pdfButton.Visible = true;
            }
            else
            {
                TestsGridView.DataSource = null;
                TestsGridView.DataBind();

            }
        }

        protected void pdfButton_Click(object sender, EventArgs e)
        {
            GeneratePDF();
        }



        private void GeneratePDF()
        {

            int columnsCount = TestsGridView.HeaderRow.Cells.Count;
            // Create the PDF Table specifying the number of columns


            PdfPTable pdfTable = new PdfPTable(columnsCount);



            // Loop thru each cell in GrdiView header row
            foreach (TableCell gridViewHeaderCell in TestsGridView.HeaderRow.Cells)
            {
                // Create the Font Object for PDF document
                Font font = new Font();
                // Set the font color to GridView header row font color
                font.Color = new BaseColor(TestsGridView.HeaderStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(TestsGridView.HeaderStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }



            // Loop thru each datarow in GrdiView
            foreach (GridViewRow gridViewRow in TestsGridView.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    // Loop thru each cell in GrdiView data row
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        Font font = new Font();
                        font.Color = new BaseColor(TestsGridView.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text, font));

                        pdfCell.BackgroundColor = new BaseColor(TestsGridView.RowStyle.BackColor);

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }



            foreach (TableCell gridViewHeaderCell in TestsGridView.FooterRow.Cells)
            {
                // Create the Font Object for PDF document
                Font font = new Font();
                // Set the font color to GridView header row font color
                font.Color = new BaseColor(TestsGridView.FooterStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(TestsGridView.FooterStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }



            // Create the PDF document specifying page size and margins
            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);

            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            string centerName = "Diagnostic Center";
            string reportName = "Test Type Wise Report";


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
                "attachment;filename=TestTypeWiseReport.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        private int serialNo = 0;
        private decimal tot = 0;
        protected void TestsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (serialNo += 1).ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "TestTypeName").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "TotalTest").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "TotalFee").ToString();

                tot = tot + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalFee"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "Total Amount: ";
                e.Row.Cells[3].Text = tot.ToString();
            }
        }

    


        
       
    }
}
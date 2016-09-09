using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DisgonisticCenter.BLL;
using DisgonisticCenter.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = System.Drawing.Font;

namespace DisgonisticCenter.UI
{
    public partial class TestRequestEntry : System.Web.UI.Page
    {
        TestRequestManager testRequestManager = new TestRequestManager();
        TestSetupManager testSetupManager=new TestSetupManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                LoadTest();
              
            }
            //ViewState["dropdownIndex"] = testDropDown.SelectedIndex;
            messageLabel.Text = "";
        }

        private void LoadTest()
        {
            List<TestSetup> testLists = testSetupManager.GetAllTestSetups();
            testDropDown.DataSource = testLists;
           
            testDropDown.DataValueField = "Id";
            testDropDown.DataTextField = "TestName";
            
            testDropDown.DataBind();

            testDropDown.Items.Insert(0, " ---- Select ---- ");
        }
  
        protected void addButton_Click(object sender, EventArgs e)
        {

            if (testDropDown.SelectedIndex != null)
            
                if (testDropDown.SelectedValue == " ---- Select ---- ")
                {
                    messageLabel.Text = "Please Select Atlest one Test";
                    messageLabel.ForeColor = Color.Brown;
                    return;
                }
                else
                {


                    int testId = Convert.ToInt32(testDropDown.SelectedValue);

 
                    if (ViewState["TestId"] == null)
                    {
                        List<int> testsId = new List<int>();
                        testsId.Add(testId);
                        ViewState["TestId"] = testsId;

                    }
                    else
                    {
                        List<int> testsId = (List<int>) ViewState["TestId"];
                        testsId.Add(testId);
                        ViewState["TestId"] = testsId;
                    }


                    ShowGridView();

                }
            else
                {
                    messageLabel.Text = "Please select A Test";
                    messageLabel.ForeColor = Color.Crimson;
                }
            

        }

        private void ShowGridView()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SerialNo");
            dt.Columns.Add("TestName");
            dt.Columns.Add("TestFee");



            DataRow dr = null;
            if (ViewState["tests"] != null)
            {
                for (int i = 0; i < 1; i++)
                {
                    dt = (DataTable)ViewState["tests"];
                    if (dt.Rows.Count > 0)
                    {

                        

                        dr = dt.NewRow();
                        dr["SerialNo"] = ViewState["SerialNo"];
                        dr["TestName"] = testDropDown.SelectedItem;
                        dr["TestFee"] = feeLabel.Text;

                        dt.Rows.Add(dr);
                        testReqGridView.DataSource = dt;
                        testReqGridView.DataBind();



                    }
                }
            }
            else
            {
                ViewState["SerialNo"] = 1;

                dr = dt.NewRow();
                dr["SerialNo"] = ViewState["SerialNo"];
                dr["TestName"] = testDropDown.SelectedItem;
                dr["TestFee"] = feeLabel.Text;

                dt.Rows.Add(dr);
                testReqGridView.DataSource = dt;
                testReqGridView.DataBind();


            }

            ViewState["SerialNo"] = (int)ViewState["SerialNo"] + 1;
            ViewState["tests"] = dt;
        }


       
       
        protected void testDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (testDropDown.SelectedIndex == 0)
            {
                feeLabel.Text = string.Empty;
                
            }
            else
            {

                int testId = Convert.ToInt32(testDropDown.SelectedValue.ToString());
               
                ViewState["Tests"] =testId;
                
                TestSetup testSetup = testSetupManager.GetAllTestById(testId);
                feeLabel.Text = testSetup.Fee.ToString();
               
            }
        }



        protected void SaveButton_Click(object sender, EventArgs e)
        {

            try
            {

                List<int> testsIdList = (List<int>) ViewState["TestId"];

                if (testsIdList == null)
                {
                    messageLabel.Text = "Please select at least one Test.";
                }

                else
                {
                    TestRequest testRequest = new TestRequest();
                    testRequest.NameOfThePatient = patientNameTextBox.Text;
                    testRequest.Dob = Convert.ToDateTime(dobDateTimepicker.Value);
                    testRequest.MobileNo = mobileNoTextBox.Text;
                    testRequest.TotalAmount = Convert.ToDecimal(ViewState["TotalAmount"]);
                    decimal payment = testRequest.TotalAmount;
                    testRequest.DueDate = DateTime.Now;
                    testRequest.PaymentStatus = payment;


                    if (testRequestManager.Save(testRequest) > 0)
                    {
                        TestRequest getTestRequest = testRequestManager.GetPatientByMobileNo(testRequest.MobileNo);
                        int pateintId = getTestRequest.Id;
                        string requestDate = DateTime.Now.ToShortDateString();
                        foreach (int testId in testsIdList)
                        {
                            testRequestManager.SavePatientTests(pateintId, testId, requestDate);
                        }

                        messageLabel.Text = "Successfully Saved.";


                        if (FillBillLabel(getTestRequest))
                        {
                            messageLabel.Text = "Successfully Saved.";
                            GeneratePDF();

                        }

                        messageLabel.Visible = true;
                        patientNameTextBox.Text = "";
                        mobileNoTextBox.Text = "";
                        testDropDown.SelectedIndex = 0;
                        feeLabel.Text = "";

                    }

                    else
                    {
                        messageLabel.Text = "Save Failed.";
                    }

                }
            }
            catch (Exception exception)
            {
                messageLabel.Text = exception.Message;
            }





    }

        private void GeneratePDF()
        {
            int columnsCount = testReqGridView.HeaderRow.Cells.Count;
            // Create the PDF Table specifying the number of columns


            PdfPTable pdfTable = new PdfPTable(columnsCount);



            // Loop thru each cell in GrdiView header row
            foreach (TableCell gridViewHeaderCell in testReqGridView.HeaderRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                // Set the font color to GridView header row font color
                //font.Color = new BaseColor(testReqGridView.HeaderStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(testReqGridView.HeaderStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }

            // Loop thru each datarow in GrdiView
            foreach (GridViewRow gridViewRow in testReqGridView.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    // Loop thru each cell in GrdiView data row
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        //Font font = new Font();
               

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text));

                        pdfCell.BackgroundColor = new BaseColor(testReqGridView.RowStyle.BackColor);

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }



            foreach (TableCell gridViewHeaderCell in testReqGridView.FooterRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                // Set the font color to GridView header row font color
                //font.Color = new BaseColor(testReqGridView.FooterStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(testReqGridView.FooterStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }



            // Create the PDF document specifying page size and margins
            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);

            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            pdfDocument.Open();

            pdfDocument.Add(new Paragraph(" \n\n"));
            pdfDocument.Add(new Paragraph("Date: " + dateLabel.Text));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph("Patient Name: " + patientNameLabel.Text));
            pdfDocument.Add(new Paragraph("Bill No: " + billNoLabel.Text));
            pdfDocument.Add(new Paragraph(" \n\n"));

            pdfDocument.Add(pdfTable);
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition",
                "attachment;filename=" + patientNameLabel.Text + "_Test.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        decimal tot = 0;
        protected void testReqGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tot = tot + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TestFee"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "Total : ";
                e.Row.Cells[2].Text = tot.ToString();

            }

            ViewState["TotalAmount"] = tot;
        }


        private bool FillBillLabel(TestRequest getTestRequest)
        {
            patientNameLabel.Text = getTestRequest.NameOfThePatient;
            billNoLabel.Text = getTestRequest.BillNo;
            dateLabel.Text = DateTime.Now.ToString();

            return true;
        }

    }
}
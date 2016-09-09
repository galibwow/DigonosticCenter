using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using DisgonisticCenter.BLL;
using DisgonisticCenter.Model;
using Newtonsoft.Json;

namespace DisgonisticCenter.UI
{
    public partial class Payment : System.Web.UI.Page
    {
        TestSetupManager testSetupManager = new TestSetupManager();
        TestRequestManager testRequestManager = new TestRequestManager();

        PaymentManager paymentManager = new PaymentManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadAllTestData();
            }
        }

        //private double GetDueAmount()
        //{
        //    TestRequest testRequest=new TestRequest();
        //    decimal total = testRequest.TotalAmount;
        //    double amount= Convert.ToDouble(amountTextBox.Text);
        //    double totalamount = total - amount;
        //    return totalamount;
        //}
        private void loadAllTestData()
        {
            paymentGridView.DataSource = testSetupManager.GetAllTestSetups();
            paymentGridView.DataBind();
            paidAmount.Text = "";
            dueAmountLabel.Text = "";
        }


        private void LoadByBillNoInitial()
        {
            string bill = billNoTextBox.Text;

            TestRequest testRequest = new TestRequest();
            testRequest = testRequestManager.GetallTestRequestByBillNo(bill);
            ViewState["TestRequest"] = testRequest;

            if (testRequest == null)
            {
                messageLabel.Text = "No Data found";
                messageLabel.ForeColor=Color.Crimson;
                ;
                loadAllTestData();
            }
            else
            {

                //List<SearchVIew> searches = paymentManager.GetAllBillInfo(bill);
                billdateLabel.Text = testRequest.DueDate.ToShortDateString();
                totalFeeLabel.Text = testRequest.TotalAmount.ToString();

                paidAmount.Text = testRequest.PaidAmount.ToString();
                dueAmountLabel.Text = testRequest.PaymentStatus.ToString();


                paymentGridView.DataSource = loadDataByTestId();
                paymentGridView.DataBind();
            } //dueAmountLabel.Text = GetDueAmount().ToString();


        }

        private List<TestSetup> loadDataByTestId()
        {
            TestRequest testRequest = (TestRequest)ViewState["TestRequest"];

            PatientsAndTest patientsAndTest = new PatientsAndTest();
            patientsAndTest.PaitentId = testRequest.Id;
            PatientsAndTest patients = testSetupManager.GetAllpaitentBytestreqId(patientsAndTest.PaitentId);
            List<TestSetup> testSetups = testSetupManager.GetlistoftestbytestId(patients.TestId);
            return testSetups;
        }

        protected void payButton_Click(object sender, EventArgs e)
        {
            TestRequest testRequest = (TestRequest)ViewState["TestRequest"];


            if (amountTextBox.Text == String.Empty)
            {
                messageLabel.Text = "Please provide Bill No or Mobile No";
                messageLabel.ForeColor = Color.Crimson;
                ;
                return;
            }
            else
            {
                if (testRequest.PaidAmount != 0)
                {
                    decimal paid = Convert.ToDecimal(amountTextBox.Text);
                    decimal Due = testRequest.PaymentStatus-paid;

                    testRequest.PaymentStatus = Due;
                    testRequest.PaidAmount+= paid;
                    ViewState["Due"] = testRequest.PaymentStatus;
                    if (paymentManager.UpdatePaymentStatus(testRequest.BillNo, testRequest.MobileNo,
                        testRequest.PaymentStatus, testRequest.PaidAmount) > 0)
                    {
                        messageLabel.Text = "Payment successful.";
                        amountTextBox.Text = "";
                        paidAmount.Text = testRequest.PaidAmount.ToString();
                        dueAmountLabel.Text = testRequest.PaymentStatus.ToString();
                        totalFeeLabel.Text = testRequest.TotalAmount.ToString();

                    }

                }
            }

        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            if (billNoTextBox.Text == string.Empty)
            {
                messageLabel.Text =
                "PLease insert bill/mobile no in textbox";
                messageLabel.ForeColor=Color.Crimson;
                ;
            }
            else
            {
                LoadByBillNoInitial();
            }

        }

        public List<string> GetBillNo(string billNo)
        {
          List<string>testRequests=  testRequestManager.Getbillnobysearch(billNo);
            return testRequests;
        } 


    }
}
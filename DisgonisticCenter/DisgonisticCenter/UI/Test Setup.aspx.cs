using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DisgonisticCenter.BLL;
using DisgonisticCenter.Model;

namespace DisgonisticCenter.UI
{
    public partial class Test_Setup : System.Web.UI.Page
    {
        TestSetupManager testSetupManager = new TestSetupManager();
        TestTypeManager testTypeManager = new TestTypeManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropDown();
                LoadTestData(testSetupManager);
            }

          
        }

        private void LoadDropDown()
        {
            testTypeDropDownList.DataSource = testTypeManager.GetAllTestTypes();
            testTypeDropDownList.DataTextField = "TestTypeName";
            testTypeDropDownList.DataValueField = "Id";

            testTypeDropDownList.DataBind();

            testTypeDropDownList.Items.Insert(0, " ---- Select ---- ");
        }

        private void LoadTestData(TestSetupManager testSetupManager)
        
        {
            testNameGridView.DataSource = testSetupManager.GetAllTestNames();

            testNameGridView.DataBind();
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                TestSetupManager testSetupManager = new TestSetupManager();
                TestSetup testSetup = new TestSetup();
                testSetup.TestName = testNameTextBox.Text;
                testSetup.Fee = Convert.ToDouble(feeTextBox.Text);
                if (testTypeDropDownList.SelectedValue == " ---- Select ---- ")
                {
                    messageLabel.Text = "please Select a test";
                    messageLabel.ForeColor = Color.Red;
                }
                testSetup.TestTypeId = Convert.ToInt32(testTypeDropDownList.SelectedValue);
               

                if (testSetupManager.IsTestNameExist(testSetup))
                {
                    messageLabel.Text = "Test Name Exist";
                    messageLabel.Visible = true;
                    messageLabel.ForeColor=Color.Crimson;
                }
                else
                {
                    testSetupManager.SaveTestType(testSetup);
                    messageLabel.Text = "Save Successfully";
                    messageLabel.Visible = true;
                    LoadTestData(testSetupManager);
                    messageLabel.ForeColor=Color.DarkGreen;
                    
                }
           
            }
            catch (Exception exception)
            {
                
                messageLabel.Text = "Please Select Test Type";
                messageLabel.Visible = true;
                ;messageLabel.ForeColor=Color.Red;
                ;
               return;
            }
            
            
            
           


        }


    }
}
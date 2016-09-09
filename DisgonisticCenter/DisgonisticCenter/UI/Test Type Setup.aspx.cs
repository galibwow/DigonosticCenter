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
    public partial class Test_Type_Setup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TestTypeManager testTypeManager=new TestTypeManager();
            testTypeGridview.DataSource = testTypeManager.GetAllTestTypes();
            testTypeGridview.DataBind();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {



                if (testTypeTextBox.Text == string.Empty)
                {
                    messageLabel.Text = "Please Insert TestType";
                    messageLabel.ForeColor=Color.Red;
                }


                else
                {
                    TestType testType = new TestType();
                    testType.TestTypeName = testTypeTextBox.Text;

                    TestTypeManager testTypeManager = new TestTypeManager();
                    if (testTypeManager.IsTestTypeExist(testType))
                    {
                        messageLabel.Text = "Type Exist";
                        messageLabel.ForeColor = Color.Red;
                    }

                    else if (testTypeManager.SaveTestType(testType))
                    {
                        messageLabel.Text = "Save Successfully";
                        messageLabel.ForeColor=Color.Green;
                        testTypeGridview.DataSource = testTypeManager.GetAllTestTypes();
                        testTypeGridview.DataBind();
                    }
                }
            }
            catch (Exception exception)
            {
                messageLabel.Text = exception.Message;
            }


        }
    }
}
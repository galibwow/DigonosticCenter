using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DisgonisticCenter.Model;

namespace DisgonisticCenter.DAL
{
    public class TestwishReportGateway
    {
        private string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagonosticDbConnectionString"].ConnectionString;


        internal List<DateWishTestReport> GetDateWiseTestReport(string startDate, string endDate)
        {
            
            SqlConnection connection=new SqlConnection(connectionString);
            string query = @"SELECT TestName, SUM(TestCount) as TestCount,SUM(TotalFee) as TotalFee  from DateWiseTestsReport WHERE RequestDate BETWEEN '" + startDate + "' AND '" + endDate + "' group by TestName";
           SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<DateWishTestReport> testWiseReportList = new List<DateWishTestReport>();

            while (reader.Read())
            {
                DateWishTestReport testReport = new DateWishTestReport();

                testReport.TestName = reader["TestName"].ToString();
                testReport.TotalTest = Convert.ToInt32(reader["TestCount"].ToString());
                testReport.TotalFee = Convert.ToInt32(reader["TotalFee"].ToString());

                testWiseReportList.Add(testReport);
            }



            reader.Close();
            connection.Close();

            return testWiseReportList;
        }



        internal List<DateWishTestReport> GetDateWiseTestReportNot(){
        

            SqlConnection connection=new SqlConnection(connectionString);
            string query = @"select Id,TestName from TestSetup where Id not in 
(select t.Id from TestSetup t,PatientTest pt where  t.Id=pt.TestId)";
          SqlCommand  command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<DateWishTestReport> testWiseReportList = new List<DateWishTestReport>();

            while (reader.Read())
            {
                DateWishTestReport testReport = new DateWishTestReport();

                testReport.TestName = reader["TestName"].ToString();
                testReport.TotalTest = 0;
                testReport.TotalFee = 0;

                testWiseReportList.Add(testReport);
            }



            reader.Close();
            connection.Close();

            return testWiseReportList;
        }

    }
}
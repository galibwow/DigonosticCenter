using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DisgonisticCenter.Model;

namespace DisgonisticCenter.DAL
{
    public class TestTypeWishReportGateway
    {
        private string connectionString =
           WebConfigurationManager.ConnectionStrings["DiagonosticDbConnectionString"].ConnectionString;


        internal List<DateWiseTestTypeVm> GetDateWiseTypeReport(string startDate, string endDate)
        {
            SqlConnection connection=new SqlConnection(connectionString);

            string query = @"SELECT TestType, SUM(TestCount) as TestCount,COALESCE(SUM(TotalFee),0) as TotalFee FROM DateWiseTestTypesReport 
WHERE RequestDate BETWEEN '" + startDate + "' AND '" + endDate + "' group by TestType";


          SqlCommand  command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<DateWiseTestTypeVm> testWiseReportList = new List<DateWiseTestTypeVm>();

            while (reader.Read())
            {
                DateWiseTestTypeVm testReport = new DateWiseTestTypeVm();

                testReport.TestTypeName = reader["TestType"].ToString();
                testReport.TotalTest = Convert.ToInt32(reader["TestCount"].ToString());
                testReport.TotalFee = Convert.ToInt32(reader["TotalFee"].ToString());

                testWiseReportList.Add(testReport);
            }
            reader.Close();
            connection.Close();
            return testWiseReportList;
        }

        internal List<DateWiseTestTypeVm> GetDateWiseTypeReportNot()
        {
            SqlConnection connection=new SqlConnection(connectionString);
            string query = @"select Id,TestType from TestType where Id not in 
(select tt.Id from TestType tt,TestSetup t where  tt.Id=t.Id)";


          SqlCommand  command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<DateWiseTestTypeVm> testWiseReportList = new List<DateWiseTestTypeVm>();

            while (reader.Read())
            {
                DateWiseTestTypeVm testReport = new DateWiseTestTypeVm();

                testReport.TestTypeName = reader["TestType"].ToString();
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
using DisgonisticCenter.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Configuration;

namespace DisgonisticCenter.DAL
{
    public class TestTypeGateway
    {
        private string ConntectionString = WebConfigurationManager.ConnectionStrings["DiagonosticDbConnectionString"].ConnectionString;
        public int SaveTestType(TestType testType)
        {
           SqlConnection connection= new SqlConnection(ConntectionString);

           string quary = "Insert Into TestType VALUES ('" + testType.TestTypeName + "')";
            SqlCommand command=new SqlCommand(quary,connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }

        public bool IsTestTypeExist(TestType testType)
        {
            SqlConnection connection=new SqlConnection(ConntectionString);
            string quary = "Select * from TestType Where TestType='" + testType.TestTypeName + "' ";
            SqlCommand command=new SqlCommand(quary,connection);
            bool IsExist = true;
            connection.Open();
           
            SqlDataReader reader = command.ExecuteReader();
            while (reader.HasRows)
            {
                reader.Read();
                TestType type=new TestType();
                type.Id = (int) reader["Id"];
                type.TestTypeName = reader["TestType"].ToString();
                connection.Close();
                reader.Close();
                return IsExist;
            }
            connection.Close();
            reader.Close();
            return IsExist=false;
        }

        public List<TestType> GetAllTestTypes()
        {
            SqlConnection connection = new SqlConnection(ConntectionString);
            string quary = "Select * from TestType ORDER BY TestType ASC";
            SqlCommand command = new SqlCommand(quary, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<TestType>testTypeList=new List<TestType>();
            while (reader.Read())
            {
                TestType type=new TestType();
                type.Id = Convert.ToInt32(reader["Id"].ToString());
                type.TestTypeName = reader["TestType"].ToString();
                testTypeList.Add(type);
            }
            connection.Close();
            reader.Close();
            return testTypeList;

        } 

    }
}
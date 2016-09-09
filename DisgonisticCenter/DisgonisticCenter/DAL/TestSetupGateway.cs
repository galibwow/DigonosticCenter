using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Configuration;
using DisgonisticCenter.Model;

namespace DisgonisticCenter.DAL
{
    public class TestSetupGateway
    {

        private string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagonosticDbConnectionString"].ConnectionString;



        public int SaveTestName(TestSetup testSetup)
        {
            SqlConnection connection=new SqlConnection(connectionString);
            string quary = "Insert Into TestSetup (TestName,Fee,TestTypeId) Values('" + testSetup.TestName + "','" +
                           testSetup.Fee + "','" + testSetup.TestTypeId + "')";
            SqlCommand command=new SqlCommand(quary,connection);
            connection.Open();
            int save = command.ExecuteNonQuery();
            connection.Close();
            return save;
        }

        public bool IsTestNameExist(TestSetup testSetup)
        {
            SqlConnection connection=new SqlConnection(connectionString);
            string quary = "Select * from TestSetup where TestName='" + testSetup.TestName + "' ";
            SqlCommand command=new SqlCommand(quary,connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
             while (reader.HasRows)
             {
                reader.Read();
                TestSetup setup=new TestSetup();
                //setup.Id = Convert.ToInt32(reader["Id"].ToString());
                setup.TestName = reader["TestName"].ToString();
                setup.Fee = Convert.ToDouble(reader["Fee"].ToString());
                setup.TestTypeId = Convert.ToInt32(reader["TestTypeId"].ToString());
                connection.Close();
                reader.Close();
                 return true;

             }
            return false;
        }


        public List<TestSetupVM> GetAllTestSetupwithType()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string quary = "Select * from TestVM";
            SqlCommand command = new SqlCommand(quary, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<TestSetupVM> testSetups = new List<TestSetupVM>();

            while (reader.Read())
            {
                TestSetupVM testSetup = new TestSetupVM();
                //testSetup.Id = Convert.ToInt32(reader["Id"].ToString());
                testSetup.TestName = reader["TestName"].ToString();
                testSetup.Fee = Convert.ToDouble(reader["Fee"].ToString());
                testSetup.TestTypeName = reader["TestType"].ToString();
                testSetups.Add(testSetup);

            }
            reader.Close();
            connection.Close();
            return testSetups;


        }

        public List<TestSetup> GetAllTestSetup()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string quary = "Select * from TestSetup";
            SqlCommand command = new SqlCommand(quary, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<TestSetup> testSetupList = new List<TestSetup>();
            while (reader.Read())
            {
                TestSetup type = new TestSetup();
                type.Id = Convert.ToInt32(reader["Id"].ToString());
                type.TestName = reader["TestName"].ToString();
                type.Fee = Convert.ToDouble(reader["Fee"].ToString());
                type.TestTypeId = Convert.ToInt32(reader["TestTypeId"].ToString());
                

                testSetupList.Add(type);
            }
            connection.Close();
            reader.Close();
            return testSetupList;
        }

        public TestSetup GetAllTestSetupByTestReqId(int Id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string quary = "Select * from TestSetup Where Id='"+Id+"'";
            SqlCommand command = new SqlCommand(quary, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            TestSetup setup=new TestSetup();
            while (reader.Read())
            {
                TestSetup type = new TestSetup();
                type.Id = Convert.ToInt32(reader["Id"].ToString());
                type.TestName = reader["TestName"].ToString();
                type.Fee = Convert.ToDouble(reader["Fee"].ToString());
                type.TestTypeId = Convert.ToInt32(reader["TestTypeId"].ToString());

                setup = type;
            }
            connection.Close();
            reader.Close();
            return setup;
        }

        public PatientsAndTest GetAllpaitentBytestreqId(int Id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string quary = "Select * from PatientTest Where TestReqid='" + Id + "'";
            SqlCommand command = new SqlCommand(quary, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            PatientsAndTest patients=new PatientsAndTest();
            while (reader.Read())
            {
                PatientsAndTest patientsAndTest=new PatientsAndTest();
                patientsAndTest.PaitentId = Convert.ToInt32(reader["TestReqid"].ToString());
                patientsAndTest.TestId = Convert.ToInt32(reader["TestId"].ToString());
                patients = patientsAndTest;



            }
            connection.Close();
            reader.Close();
            return patients;
        }




        public List<TestSetup> GetlistofTests(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string quary = "Select * from TestSetup Where Id='" + id + "'";
            SqlCommand command = new SqlCommand(quary, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<TestSetup>testSetups=new List<TestSetup>();
            while (reader.Read())
            {
                TestSetup type = new TestSetup();
                type.Id = Convert.ToInt32(reader["Id"].ToString());
                type.TestName = reader["TestName"].ToString();
                type.Fee = Convert.ToDouble(reader["Fee"].ToString());
                type.TestTypeId = Convert.ToInt32(reader["TestTypeId"].ToString());

                testSetups.Add(type);
            }
            connection.Close();
            reader.Close();
            return testSetups;
        }
    }
}
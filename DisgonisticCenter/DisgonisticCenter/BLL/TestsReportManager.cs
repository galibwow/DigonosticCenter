﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DisgonisticCenter.DAL;
using DisgonisticCenter.Model;

namespace DisgonisticCenter.BLL
{
    public class TestsReportManager
    { TestwishReportGateway testGateway=new TestwishReportGateway();

    internal bool ValidateInput(string startDate, string endDate)
    {
        if (startDate == String.Empty)
        {
            throw new Exception("Select a Date");
        }

        else if (endDate == String.Empty)
        {
            throw new Exception("Select a Date");
        }

        else if (Convert.ToDateTime(startDate) > DateTime.Now)
        {
            throw new Exception("Search Date Cannot Go Beyond Current Date!");
        }

        else if (Convert.ToDateTime(endDate) > DateTime.Now)
        {
            throw new Exception("Search Date Cannot Go Beyond Current Date!");
        }

        return true;
    }

        internal List<DateWishTestReport> GetDateWiseTestReport(string startDate, string endDate)
        {
            try
            {
                return testGateway.GetDateWiseTestReport(startDate, endDate);
            }

            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        internal List<DateWishTestReport> GetDateWishTestReports(string startDate, string endDate)
        {
            try
            {
                return testGateway.GetDateWiseTestReport(startDate, endDate);
            }

            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        internal List<DateWishTestReport> GetDateWiseTestReportNot()
        {
            return testGateway.GetDateWiseTestReportNot();
        }

    }
}
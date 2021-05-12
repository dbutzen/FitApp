using AspNetCore.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCT.Utilities.Reporting.Models;

namespace TCT.Utilities.Reporting
{
    public static class UserReportManager
    {
        public static byte[] Print(UserReport data, string reportType)
        {
            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}\UserReport.rdlc";
            var parameters = new Dictionary<string, string>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1252");
            var report = new LocalReport(path);
            parameters.Add("Name", data.Name);
            parameters.Add("CalorieGoal",data.CalorieGoal.ToString());
            parameters.Add("ProteinGoal", data.ProteinGoal.ToString());
            report.AddDataSource("UserData", data.UserDataList);
            var result = report.Execute(GetRenderType(reportType.ToLower()), 1, parameters);

            return result.MainStream;

        }

        private static RenderType GetRenderType(string reportType)
        {
            var renderType = RenderType.Pdf;

            if (reportType == "doc" || reportType == "docx")
                renderType = RenderType.Word;
            else if (reportType == "xls" || reportType == "xlsx")
                renderType = RenderType.Excel;
            else
                renderType = RenderType.Pdf;

            return renderType;
        }
    }
}

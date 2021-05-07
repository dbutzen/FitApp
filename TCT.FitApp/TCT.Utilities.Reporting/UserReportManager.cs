using AspNetCore.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCT.Utilities.Reporting.Models;

namespace TCT.Utilities.Reporting
{
    public enum ReportType { Pdf, Word, Excel }
    public static class UserReportManager
    {
        public static byte[] Print(UserReport data, ReportType reportType)
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
            var result = report.Execute(GetRenderType(reportType), 1, parameters);

            return result.MainStream;

        }

        private static RenderType GetRenderType(ReportType reportType)
        {
            var renderType = RenderType.Pdf;
            switch (reportType)
            {
                default:
                case ReportType.Pdf:
                    renderType = RenderType.Pdf;
                    break;
                case ReportType.Word:
                    renderType = RenderType.Word;
                    break;
                case ReportType.Excel:
                    renderType = RenderType.Excel;
                    break;
            }

            return renderType;
        }
    }
}

using DevExpress.XtraCharts.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;

namespace TestTrendIndicators
{
    public partial class TrendIndicators : ChartBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DbStuff dsf = new DbStuff();
            var device = dsf.GetDevices();

            ddlDevices.DataSource = device;
            ddlDevices.DataValueField = "DeviceId";
            ddlDevices.DataTextField = "DeviceAllias";
            ddlDevices.DataBind();

            WebChartControl1.DataSource = FinancialData.GetUSDJPYData();
            WebChartControl1.DataBind();
        }

        protected void ddlDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbStuff dsf = new DbStuff();
            int.TryParse(ddlDevices.SelectedValue, out int deviceID);
            var sensors = dsf.GetDeviceSensors(deviceID);
        }
    }

    public class FinancialPoint
    {
        string argument;
        DateTime dateTimeArgument;
        double highValue;
        double lowValue;
        double openValue;
        double closeValue;

        public string Argument { get { return argument; } set { argument = value; } }
        public DateTime DateTimeArgument { get { return dateTimeArgument; } set { dateTimeArgument = value; } }
        public double HighValue { get { return highValue; } set { highValue = value; } }
        public double LowValue { get { return lowValue; } set { lowValue = value; } }
        public double OpenValue { get { return openValue; } set { openValue = value; } }
        public double CloseValue { get { return closeValue; } set { closeValue = value; } }
    }

    public class FinancialData
    {
        public static List<FinancialPoint> GetUSDJPYData()
        {
            return CsvReader.ReadFinancialData("USDJPYDaily.csv");
        }
    }

    public static class CsvReader
    {
        public static List<FinancialPoint> ReadFinancialData(string fileName)
        {
            string longFileName = string.Empty;
            StreamReader reader;
            var dataSource = new List<FinancialPoint>();
            using (Stream stream = File.OpenRead(HttpContext.Current.Server.MapPath("~/App_Data/" + fileName)))
            {
                try
                {
                    reader = new StreamReader(stream);
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        var values = line.Split(',');
                        var point = new FinancialPoint();
                        point.DateTimeArgument = DateTime.ParseExact(values[0], "yyyy.MM.dd", null);
                        point.OpenValue = double.Parse(values[1], CultureInfo.InvariantCulture);
                        point.HighValue = double.Parse(values[2], CultureInfo.InvariantCulture);
                        point.LowValue = double.Parse(values[3], CultureInfo.InvariantCulture);
                        point.CloseValue = double.Parse(values[4], CultureInfo.InvariantCulture);
                        dataSource.Add(point);
                    }
                }
                catch
                {
                    throw new Exception("It's impossible to load " + fileName);
                }
            }
            return dataSource;
        }
    }

}
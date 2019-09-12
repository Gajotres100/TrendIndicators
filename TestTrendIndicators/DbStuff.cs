using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace TestTrendIndicators
{
    public class DbStuff
    {
        public List<Devices> GetDevices()
        {
            string conString = "User Id=FMS_PLUS; Password=manager; Data Source=10.70.54.44:1521/ORCL; Pooling =false;";
            OracleConnection con = new OracleConnection();
            List<Devices> list = new List<Devices>();

            try
            {

                con.ConnectionString = conString;
                con.Open();


                string cmdtxt = "select * from V_DEVICES WHERE USER_ID = 19418";


                OracleCommand oraCommand = new OracleCommand(cmdtxt, con);

                OracleDataReader oraReader = null;
                oraReader = oraCommand.ExecuteReader();

                if (oraReader.HasRows)
                {
                    while (oraReader.Read())
                    {
                        Devices obj = new Devices();
                        obj.DeviceId = Convert.ToInt32(oraReader["DEVICE_ID"]);
                        obj.DeviceAllias = oraReader["DEVICE_ALIAS"].ToString();

                        list.Add(obj);
                    }
                }

                oraReader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return list;
        }
        public List<Sensors> GetDeviceSensors(int deviceId)
        {
            string conString = "User Id=FMS_PLUS; Password=manager; Data Source=10.70.54.44:1521/ORCL; Pooling =false;";
            OracleConnection con = new OracleConnection();
            List<Sensors> list = new List<Sensors>();

            try
            {

                con.ConnectionString = conString;
                con.Open();


                string cmdtxt = "select * from v_DEVICE_SENSORS WHERE DEVICE_ID = " + deviceId; // :D zato što sam bahatlija

                OracleCommand oraCommand = new OracleCommand(cmdtxt, con);

                OracleDataReader oraReader = null;
                oraReader = oraCommand.ExecuteReader();

                if (oraReader.HasRows)
                {
                    while (oraReader.Read())
                    {
                        Sensors obj = new Sensors();
                        obj.DeviceSensorId = Convert.ToInt32(oraReader["DEVICE_SENSOR_ID"]);
                        obj.SensorName = oraReader["SENSOR_NAME"].ToString();
                        list.Add(obj);
                    }
                }

                oraReader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return list;
        }        
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace WirelessTagConnector
{
    class Program
    {
        static System.Timers.Timer processTimer;
        static void Main(string[] args)
        {
            ThreadStart t1 = new ThreadStart(repeatProcess);
            Thread childMeterThread = new Thread(t1);
            childMeterThread.Start();
            Console.WriteLine("Process Started..");
        }

        private static void repeatProcess()
        {
            try
            {
                processTimer = new System.Timers.Timer(60000);
                processTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerElapsed);
                processTimer.Start();
                Console.ReadLine();
            }
            catch (Exception e)
            {

                Console.WriteLine("Exception Occured  in repeatProcess:: " + e.Message);
            }
        }

        private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                HttpManager.GetInstance().GenerateToken();
                DateTime timeStamp = DateTime.UtcNow;
                Console.WriteLine("*****************Updating Time for ===== " + timeStamp.ToString());
                var sensors = GetUpdatedSensorInfo();
                foreach (var sensor in sensors)
                {
                    if(ConfigurationSetting.IsPosterService)
                    {
                        PutSensorDataToUFLServer(sensors.IndexOf(sensor).ToString(),sensor, timeStamp);
                        Console.WriteLine("             Updated value for Sensor :: " + sensor.name +"  and Sensor iD"+ sensors.IndexOf(sensor).ToString());
                    }
                    else
                    {
                        AddSensorData(sensor, timeStamp);
                        Console.WriteLine("             Updated value for Sensor :: " + sensor.name);
                    }                   
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception occured in main method :: " + ex.Message);
            }

        }

        static List<D> GetUpdatedSensorInfo()
        {
            var instance = HttpManager.GetInstance();
            string jsonResponse = instance.Post(instance.CLIENT_SUB_ADDRESS, instance.GET_TAG_LIST);
            return JsonConvert.DeserializeObject<SensorsDataModel>(jsonResponse).d;
        }

        static void AddSensorData(D sensorInfo,DateTime timeStamp)
        {
            string sensorDataInsertQuery = "INSERT INTO SensorData([Wireless Tag Template],[TimeStamp],[Brightness],[Humidity],[Name],[Temperature],[PIIntTSTicks],[PIIntShapeID]) VALUES (@WirelessTagTemplate,@TimeStamp,@Brightness,@Humidity,@Name,@Temperature,@PIIntTSTicks,@PIIntShapeID)";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationSetting.ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand(sensorDataInsertQuery, sqlConnection, sqlTransaction))
                        {

                            cmd.Parameters.AddWithValue("@WirelessTagTemplate", sensorInfo.name);
                            cmd.Parameters.AddWithValue("@TimeStamp", timeStamp);
                            cmd.Parameters.AddWithValue("@Brightness",sensorInfo.lux);
                            cmd.Parameters.AddWithValue("@Humidity", sensorInfo.cap);
                            cmd.Parameters.AddWithValue("@Name", sensorInfo.name);
                            cmd.Parameters.AddWithValue("@Temperature", ConvertCelsiusToFahrenheit(sensorInfo.temperature));
                            cmd.Parameters.AddWithValue("@PIIntTSTicks", 0);
                            cmd.Parameters.AddWithValue("@PIIntShapeID", 0);
                            cmd.ExecuteNonQuery();
                        }
                        sqlTransaction.Commit();
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured in AddSensorData() " + ex.Message);
            }
        }

        static void PutSensorDataToUFLServer(string Id ,D sensorInfo, DateTime timeStamp)
        {
            string posterData = "" + sensorInfo.name + "," + Id + "," + sensorInfo.temperature + "," + sensorInfo.cap + "," + sensorInfo.lux;
            Console.WriteLine(" Poster Data string is ::: " + posterData);
            HttpManager.GetInstance().PutDataToUFLConnector(posterData);

        }
        public static double ConvertCelsiusToFahrenheit(double c)
        {
            return ((9.0 / 5.0) * c) + 32;
        }
    }
}

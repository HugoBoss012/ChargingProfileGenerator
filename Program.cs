using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace ChargingProfileGenerator
{
    internal class Program
    {
        public static string src = @"C:\Users\27832\Desktop\JedlixCodingExercise\JsonFiles\inputJsonFile.json";

        private static readonly IFormatProvider cultureInfo;

        private static void Main(string[] args)
        {
            using (System.IO.StreamReader r = new System.IO.StreamReader(src))
            {
                string json = r.ReadToEnd();
                JSONModel item = Newtonsoft.Json.JsonConvert.DeserializeObject<JSONModel>(json);

                Console.WriteLine(isChargingAsync(item));
            }
        }

        /// <summary>
        /// provides the is charging value for wheather or not the car should be charging at this time
        /// </summary>
        /// <param name ="item">list of JSON data being passed through</param>
        /// <returns>returns a boolean value </returns>
        private static async Task<bool> isChargingAsync(JSONModel item)
        {
            bool charging = false;
            /// Calculating the battery percentage by taking the current battery level dividing it by the battery Capacity and multiplying it by 100
            /// to see if it falls in the direct charging percentage value
            decimal batteryPerc = (item.carData.currentBatteryLevel / item.carData.batteryCapacity) * 100;
            /// Calculating the needed kW (kilowatt) needed to match desired state of charge
            decimal kwNeeded = (item.carData.batteryCapacity * item.userSettings.desiredStateOfCharge / 100) - item.carData.currentBatteryLevel;
            /// Calculating the time needed to charge to the desired state of charge
            decimal TimeToCharge = (kwNeeded / item.carData.chargePower);

            if (batteryPerc < 20)
            {
                return true;
            }

            /// Searching within the list for a tariff that has the lowest energy price as well as is the soonest time available.
            decimal lowesttariff = item.userSettings.tariffs.Min(tariffcompare => tariffcompare.energyPrice);

            int firstMatchingValue = 0;
            foreach (var tariffdata in item.userSettings.tariffs)
            {
                var currentDate = DateTime.Now;
                var shortDateValue = currentDate.ToShortDateString();

                if (tariffdata.energyPrice == lowesttariff && firstMatchingValue == 0) /// add && (tariffdata.endTime - tariffdata.startTime) >= TimeToCharge
                {
                    string location = @"C:\Users\27832\Desktop\JedlixCodingExercise\JsonFiles";
                    string outputFile = System.IO.Path.Combine(location, "OutputJsonFile.json");

                    JsonDataWrite jsonwrite = new JsonDataWrite();

                    jsonwrite.startTime = shortDateValue + ("T") + tariffdata.startTime + (":00Z");
                    jsonwrite.endTime = shortDateValue + ("T") + tariffdata.endTime + (":00Z");
                    jsonwrite.isCharging = true;

                    var opt = new JsonSerializerOptions() { WriteIndented = true };
                    string jsonstring = System.Text.Json.JsonSerializer.Serialize(jsonwrite, opt);

                    File.WriteAllText(outputFile, jsonstring);

                    firstMatchingValue = 1;
                }/// add an else here to display all lowest tarriffs
            }

            return charging;
        }

        /// <summary>
        /// method to supply end date variable
        /// </summary>
        /// <param name="item"> item  is passed list parameter to access values </param>
        /// <returns> returns date string for final json file </returns>
        public static string EndTime(JSONModel item)
        {
            //variables
            string endTime = "";

            //splits date
            string date = item.startingTime.Substring(0, item.startingTime.IndexOf('T'));

            //splits time
            int pFrom = item.startingTime.IndexOf('T') + "T".Length;
            int pTo = item.startingTime.LastIndexOf('Z');
            String time = item.startingTime.Substring(pFrom, pTo - pFrom);

            //joined to compare date time values
            DateTime dateTime = DateTime.Parse(date + " " + time, cultureInfo);
            DateTime dateTimeNow = DateTime.Now;

            int icatch = DateTime.Compare(dateTimeNow, dateTime);

            if (icatch < 0) //earlier than
            {
                endTime = date + "T" + item.userSettings.leavingTime + ":00Z";
            }
            else if (icatch > 0)
            { //later than make next day
                DateTime startdatetime = DateTime.Parse(date);
                startdatetime = startdatetime.AddDays(1);
                string finaldate = startdatetime.ToString().Substring(0, startdatetime.ToString().IndexOf(' '));
                endTime = finaldate.ToString().Replace("/", "-") + "T" + item.userSettings.leavingTime + ":00Z";
            }
            else
            { //same time
                DateTime startdatetime = DateTime.Parse(date);
                startdatetime = startdatetime.AddDays(1);
                string finaldate = startdatetime.ToString().Substring(0, startdatetime.ToString().IndexOf(' '));
                endTime = finaldate.ToString().Replace("/", "-") + "T" + item.userSettings.leavingTime + ":00Z";
            }

            return endTime;
        }
    }
}
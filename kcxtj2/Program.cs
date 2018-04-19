using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace SimulatedDevice
{
    class Ride
    {
        private string trainId;
        private string gpsid;

        public Ride(string parentId, string id)
        {
            trainId = parentId;
            gpsid = id;
        }

        public Message RideStart(string rideId, ref string messageString)
        {
            var message = Events(rideId, "RideStart", ref messageString);

            return message;
        }
        public Message RideEnd(string rideId, ref string messageString)
        {
            var message = Events(rideId, "RideEnd", ref messageString);

            return message;
        }
        public Message PhotoTriggered(string rideId, ref string messageString)
        {
            var message = Events(rideId, "PhotoTriggered", ref messageString);

            return message;
        }

        private Message Events(string rideId, string eventType, ref string messageString)
        {
            Random rand = new Random();
            int pcount = rand.Next() % 10;

            var RideDataPoint = new
            {
                rideId = rideId,
                trainId = "6FA812B3-FAE8-4875-8D80-62B037CD3528",
                correlationId = gpsid,
                passengerCount = pcount,
                eventType = eventType,
                deviceTime = DateTime.Now//"2017-12-06T20:23:43.9790000Z"
            };
            messageString = JsonConvert.SerializeObject(RideDataPoint);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            return message;
        }
    }

    class Gps
    {
        private string trainId;
        private string gpsid;

        public Gps(string parentId, string id)
        {
            trainId = parentId;
            gpsid = id;
        }

        public Message GpsMessages(string rideId, ref string messageString)
        {
            Random rand = new Random();
            var GPSDataPoint = new
            {
                //messageId = messageId++,
                //deviceId = "tshDevice",
                //temperature = currentTemperature,
                //humidity = currentHumidity,
                rideId = rideId,
                trainId = "6FA812B3-FAE8-4875-8D80-62B037CD3528",
                correlationId = gpsid,
                la = rand.NextDouble(),
                _long = rand.NextDouble(),
                alt = rand.NextDouble(),
                speed = rand.NextDouble(),
                vertAccuracy = "4",
                horizAccuracy = "10",
                deviceTime = DateTime.Now//"2017-12-06T20:23:43.9790000Z"
            };
            messageString = JsonConvert.SerializeObject(GPSDataPoint);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            return message;
        }
    }

    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "ParkGate.azure-devices.net";
        static string deviceKey = "XWOkvme/RfEQQTDGpoSrRNCBONyclUvMsO/yGkTfbpQ=";

        static string trainId = Guid.NewGuid().ToString();
        static string rideId = Guid.NewGuid().ToString();
        // create Ride
        static Ride ride = new Ride(trainId, rideId);
        // create gps
        static Gps gps1 = new Gps(trainId, rideId);
        static Gps gps2 = new Gps(trainId, rideId);
        static Gps gps3 = new Gps(trainId, rideId);

        private static async void SendDeviceToCloudMessagesAsync()
        {
            double minTemperature = 20;
            double minHumidity = 60;
            Random rand = new Random();
            int count = 0;

            while (true)
            {
                double currentTemperature = minTemperature + rand.NextDouble() * 15;
                double currentHumidity = minHumidity + rand.NextDouble() * 20;                

                var messageString = "";
                Message message = null;
                if (count == 0)
                {
                    rideId = Guid.NewGuid().ToString();
                    //end
                    message = ride.RideEnd(rideId, ref messageString);
                    await deviceClient.SendEventAsync(message);
                    Console.WriteLine("{0} > End Sending message: {1}", DateTime.Now, messageString);
                    await Task.Delay(5000);
                    //start
                    message = ride.RideStart(rideId, ref messageString);
                    await deviceClient.SendEventAsync(message);
                    Console.WriteLine("{0} > Start Sending message: {1}", DateTime.Now, messageString);
                    await Task.Delay(5000);
                }

                if (count / 3 == 0)
                {
                    //photo
                    message = ride.PhotoTriggered(rideId, ref messageString);
                    await deviceClient.SendEventAsync(message);
                    Console.WriteLine("{0} > photo Sending message: {1}", DateTime.Now, messageString);
                    await Task.Delay(5000);
                }

                //message = gps1.GpsMessages(rideId, ref messageString);
                //await deviceClient.SendEventAsync(message);
                //Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                //await Task.Delay(5000);
                //message = gps2.GpsMessages(rideId, ref messageString);
                //await deviceClient.SendEventAsync(message);
                //Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                //await Task.Delay(5000);
                //message = gps3.GpsMessages(rideId, ref messageString);
                //await deviceClient.SendEventAsync(message);
                //Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                //await Task.Delay(5000);

                count++;
                if (count > 10)
                {
                    count = 0;
                }
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Simulated device\n");

            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("tshDevice", deviceKey), TransportType.Mqtt);
            deviceClient.ProductInfo = "HappyPath_Simulated-CSharp";
            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }
    }
}

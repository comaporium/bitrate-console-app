using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Skyline
{
    public class NIC
    {
        public string Device { get; set; }
        public string MAC { get; set; }
        public string Timestamp { get; set; }
        public string Rx { get; set; }
        public string Tx { get; set; }
    }

    public class device
    {
        public string Device { get; set; }
        public string Model { get; set; }
        public List<NIC> NIC { get; set; }
    }

    public class Program
    {
        //declaring JSON
        static string recivedAPIObject = @"{
	    ""Device"": ""Arista"",
	    ""Model"": ""X-Video"",
	    ""NIC"": [{
		    ""Description"": ""Linksys ABR"",
		    ""MAC"": ""14:91:82:3C:D6:7D"",
		    ""Timestamp"": ""2020-03-23T18:25:43.511Z"",
		    ""Rx"": ""3698574500"",
		    ""Tx"": ""122558800""
	        }]
	    }";

        static long calculateBitRate(string octets)
        {
            //pooling rate of 2 Hz
            long hz = 2;
            //octets are in string so we need to parse them to int
            long o = Int64.Parse(octets);
            //octets have pooling rate of 2Hz
            long bitRate = (o * hz);
            //converting bps to Mbps
            long Mbitrate = bitRate / 1000000;
            return Mbitrate;
        }

        static void Main(string[] args)
        {
            //parsing JSON
            var parsedJSON = JsonSerializer.Deserialize<device>(recivedAPIObject);
            //foreach is here so we can get rx and tx informations
            foreach (var x in parsedJSON.NIC)
            {
                Console.WriteLine($"Rx octets: {x.Rx}, Bitrate: " + calculateBitRate($"{ x.Rx}") + " MBps");
                Console.WriteLine($"Tx octets: {x.Tx}, Bitrate: "+ calculateBitRate($"{ x.Tx}") + " MBps");
            }
        }
    }
}

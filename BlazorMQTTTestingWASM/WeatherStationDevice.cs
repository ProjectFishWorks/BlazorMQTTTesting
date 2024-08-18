using MQTTnet.Client.Publishing;
using MQTTnet.ClientLib;

using System;

namespace BlazorMQTTTestingWASM.Models
{
    public class WeatherStationDevice : Device
    {
        private int nodeID;

        public WeatherStationDevice(MQTTnet.ClientLib.MqttService mqttService, int systemID,int basestationID,int nodeID) : base(mqttService, systemID, basestationID)
        {
            this.nodeID = nodeID;
        }

        public int? RTDError
        {
            get
            {
                return (int?)getMessageData(nodeID, 16384);
            }
        }

        public float? TemperatureBME
        {
            get
            {
                return (float?)Math.Round((double)getMessageDataFloat(nodeID, 41216),2);
            }
        }
        
        public float? TemperatureRTD
        {
            get
            {
                return (float?)Math.Round((double)getMessageDataFloat(nodeID, 41217), 2);
            }
        }

        public float? Humidity
        {
            get
            {
                return (float?)Math.Round((double)getMessageDataFloat(nodeID, 41472), 2);
            }
        }

        public float? Pressure
        {
            get
            {
                return (float?)Math.Round((double)getMessageDataFloat(nodeID, 41728), 2);
            }
        }

        public float? WindSpeedAverage
        {
            get
            {
                return (float?)Math.Round((double)getMessageDataFloat(nodeID, 41984), 2);
            }
        }

        public float? WindSpeedMax
        {
            get
            {
                return (float?)Math.Round((double)getMessageDataFloat(nodeID, 41985), 2);
            }
        }

        public Dictionary<int,float?> WindDirections 
        { 
            get
            {
                Dictionary<int,float?> values = new Dictionary<int,float?>();
                values.Add(0, getMessageDataFloat(nodeID, 42241));
                values.Add(45, getMessageDataFloat(nodeID, 42242));
                values.Add(90, getMessageDataFloat(nodeID, 42243));
                values.Add(135, getMessageDataFloat(nodeID, 42244));
                values.Add(180, getMessageDataFloat(nodeID, 42245));
                values.Add(225, getMessageDataFloat(nodeID, 42246));
                values.Add(270, getMessageDataFloat(nodeID, 42247));
                values.Add(315, getMessageDataFloat(nodeID, 42248));
                return values;
            }
        }


        public float? Rainfall
        {
            get
            {
                return (float?)Math.Round((double)getMessageDataFloat(nodeID, 42496), 3);
            }
        }

    }
}

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
                return (int?)getMessagePayload(nodeID, 16384).data;
            }
        }

        public float? TemperatureBME
        {
            get
            {
                return (float?)Math.Round((double)getMessagePayload(nodeID, 41216).dataFloat,2);
            }
        }
        
        public float? TemperatureRTD
        {
            get
            {
                return (float?)Math.Round((double)getMessagePayload(nodeID, 41217).dataFloat, 2);
            }
        }

        public float? Humidity
        {
            get
            {
                return (float?)Math.Round((double)getMessagePayload(nodeID, 41472).dataFloat, 2);
            }
        }

        public float? Pressure
        {
            get
            {
                return (float?)Math.Round((double)getMessagePayload(nodeID, 41728).dataFloat / 1000, 2);
            }
        }

        public float? WindSpeedAverage
        {
            get
            {
                return (float?)Math.Round((double)getMessagePayload(nodeID, 41984).dataFloat, 2);
            }
        }

        public float? WindSpeedMax
        {
            get
            {
                return (float?)Math.Round((double)getMessagePayload(nodeID, 41985).dataFloat, 2);
            }
        }

        public int WindDirectionMax
        {
            get
            {
                return (int)getMessagePayload(nodeID, 42240).data;
            }
        }

        public Dictionary<int,float?> WindDirections 
        { 
            get
            {
                Dictionary<int,float?> values = new Dictionary<int,float?>();
                values.Add(0, getMessagePayload(nodeID, 42241).dataFloat);
                values.Add(45, getMessagePayload(nodeID, 42242).dataFloat);
                values.Add(90, getMessagePayload(nodeID, 42243).dataFloat);
                values.Add(135, getMessagePayload(nodeID, 42244).dataFloat);
                values.Add(180, getMessagePayload(nodeID, 42245).dataFloat);
                values.Add(225, getMessagePayload(nodeID, 42246).dataFloat);
                values.Add(270, getMessagePayload(nodeID, 42247).dataFloat);
                values.Add(315, getMessagePayload(nodeID, 42248).dataFloat);
                return values;
            }
        }


        public float? RainfallLast10Minutes
        {
            get
            {
                return (float?)Math.Round((double)getMessagePayload(nodeID, 42496).dataFloat, 2);
            }
        }

        public float? RainfallLastHour
        {
            get
            {
                return (float?)Math.Round((double)getMessagePayload(nodeID, 42497).dataFloat, 2);
            }
        }
        public float? RainfallLastDay
        {
            get
            {
                return (float?)Math.Round((double)getMessagePayload(nodeID, 42498).dataFloat, 2);
            }
        }
        

    }
}

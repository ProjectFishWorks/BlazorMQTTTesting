using System;

namespace BlazorMQTTTestingWASM.Models
{
    public class MQTTData
    {
        public MQTTData()
        {
            data = null;
            time = null;
        }
        public ulong? data { get; set; }
        public ulong? time { get; set; }

        public DateTime? TimeUTC {
            get
            {
                if (time == null)
                {
                    return null;
                }
                return DateTimeOffset.FromUnixTimeSeconds((long)time).UtcDateTime;
            }
        }

        public DateTime? TimeLocal {
            get
            {
                if (time == null)
                {
                    return null;
                }
                return DateTimeOffset.FromUnixTimeSeconds((long)time).LocalDateTime;
            }
        }

        public float? dataFloat {
            get
            {
                if (data == null)
                {
                    return null;
                }
                return BitConverter.ToSingle(BitConverter.GetBytes(data.Value), 0);
            }
        }
    }
}

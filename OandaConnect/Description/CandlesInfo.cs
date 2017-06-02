using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace OandaConnect.Description
{
    public class CandlesInfo
    {
        public string Instrument { get; set; }
        public string Granularity { get; set; }
        public List<AskBidCandle> Candles { get; set; }
    }

    public class AskBidCandle
    {
        public bool Complete { get; set; }
        public DateTime Time { get; set; }
        public float Volume { get; set; }
        public Candle Ask { get; set; }
        public Candle Bid { get; set; }
    }

    public class Candle
    {
        [DeserializeAs(Name ="o")]
        public float Open { get; set; }
        [DeserializeAs(Name = "h")]
        public float High { get; set; }
        [DeserializeAs(Name = "l")]
        public float Low { get; set; }
        [DeserializeAs(Name = "c")]
        public float Close { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WirelessTagConnector
{
    public class SensorsDataModel
    {
        public List<D> d { get; set; }
    }
    public class D
    {
        public string __type { get; set; }
        public object notificationJS { get; set; }
        public string name { get; set; }
        public string uuid { get; set; }
        public string comment { get; set; }
        public int slaveId { get; set; }
        public int tagType { get; set; }
        public object lastComm { get; set; }
        public bool alive { get; set; }
        public int signaldBm { get; set; }
        public double batteryVolt { get; set; }
        public bool beeping { get; set; }
        public bool lit { get; set; }
        public bool migrationPending { get; set; }
        public int beepDurationDefault { get; set; }
        public int eventState { get; set; }
        public int tempEventState { get; set; }
        public bool OutOfRange { get; set; }
        public double lux { get; set; }
        public double temperature { get; set; }
        public double tempCalOffset { get; set; }
        public int capCalOffset { get; set; }
        public object image_md5 { get; set; }
        public double cap { get; set; }
        public int capRaw { get; set; }
        public int az2 { get; set; }
        public int capEventState { get; set; }
        public int lightEventState { get; set; }
        public bool shorted { get; set; }
        public object thermostat { get; set; }
        public object playback { get; set; }
        public int postBackInterval { get; set; }
        public int rev { get; set; }
        public int version1 { get; set; }
        public int freqOffset { get; set; }
        public int freqCalApplied { get; set; }
        public int reviveEvery { get; set; }
        public int oorGrace { get; set; }
        public double LBTh { get; set; }
        public bool enLBN { get; set; }
        public int txpwr { get; set; }
        public bool rssiMode { get; set; }
        public bool ds18 { get; set; }
        public int v2flag { get; set; }
        public double batteryRemaining { get; set; }
    }


}

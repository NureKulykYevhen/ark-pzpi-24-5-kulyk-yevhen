using System.Collections.Generic;
using System;
using PowerMonitor.Models;
using System.Linq;
namespace powermonitor
{
    public class batteryMonitor
    {
        public List<Battery> l = new List<Battery>();
        public String Check(int ID, double Load)
        {
            var b = l.FirstOrDefault(x => x.Id == ID);
            if (b == null) return "not found";
            double e = b.CapacityKwh * b.ChargePercent / 100
                * (1 - b.WearPercent / 100);
            double h = e / Load;
            if (h < 2) { return "LOW: " + h + " h"; }
            else return "OK: " + h + " h";
        }
    }
}

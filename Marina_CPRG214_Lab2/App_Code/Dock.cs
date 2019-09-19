using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    public class Dock
    {
        public int DockId { get; set; }
        public string Name { get; set; }
        public bool WaterService { get; set; }
        public bool ElectricalService { get; set; }

        public Dock(int dockId, string name, bool waterService, bool electricalService)
        {
            DockId = dockId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            WaterService = waterService;
            ElectricalService = electricalService;
        }
    }
}
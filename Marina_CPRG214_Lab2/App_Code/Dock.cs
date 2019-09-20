using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    public class Dock
    {
        public int DockId { get; set; } // Dock Id
        public string Name { get; set; } // Name of dock
        public bool WaterService { get; set; } // Is there water service?
        public bool ElectricalService { get; set; } // Is there electrical service?

        /// <summary>
        /// Creates a dock
        /// </summary>
        /// <param name="dockId">Id of dock</param>
        /// <param name="name">Name of dock</param>
        /// <param name="waterService">Is there water service?</param>
        /// <param name="electricalService">Is there electrical service?</param>
        public Dock(int dockId, string name, bool waterService, bool electricalService)
        {
            DockId = dockId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            WaterService = waterService;
            ElectricalService = electricalService;
        }
    }
}
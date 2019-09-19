using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    public class Slip
    {
        public int SlipId { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public Dock Dock;

        public Slip(int slipId, int width, int length)
        {
            SlipId = slipId;
            Width = width;
            Length = length;
        }

        public Slip(int slipId, int width, int length, Dock dock) : this(slipId, width, length)
        {
            Dock = dock ?? throw new ArgumentNullException(nameof(dock));
        }
    }
}
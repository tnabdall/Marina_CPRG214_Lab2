using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    public class Slip
    {
        public int SlipId { get; set; } // Id of slip
        public int Width { get; set; } // Width
        public int Length { get; set; } // Length
        public Dock Dock; // Dock assigned to slip

        /// <summary>
        /// Creates a slip
        /// </summary>
        /// <param name="slipId">Slip Id</param>
        /// <param name="width">Width</param>
        /// <param name="length">Length</param>
        public Slip(int slipId, int width, int length)
        {
            SlipId = slipId;
            Width = width;
            Length = length;
        }

        /// <summary>
        /// Creates a slip
        /// </summary>
        /// <param name="slipId">Slip Id</param>
        /// <param name="width">Width</param>
        /// <param name="length">Length</param>
        /// <param name="dock">Dock assigned to slip</param>
        public Slip(int slipId, int width, int length, Dock dock) : this(slipId, width, length)
        {
            Dock = dock ?? throw new ArgumentNullException(nameof(dock));
        }
    }
}
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;
using System.IO;

namespace IRIS.Presentation.Helpers
{
    public static class ControlCapture
    {
        public static byte[] CapturePng(Control c)
        {
            using var bmp = new Bitmap(c.Width, c.Height);
            c.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

            using var ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }
}

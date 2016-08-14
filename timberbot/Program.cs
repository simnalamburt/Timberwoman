using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace timberbot
{
    class Program
    {
        static int Main(string[] args)
        {
            // Retrieve windows handle
            var handle = FindWindow("UnityWndClass", "Timberman");
            if ((long)handle == 0)
            {
                Console.Out.WriteLine("Please run Timberman first.");
                return 1;
            }

            // Get window rect from handle
            RECT rect;
            if (!GetWindowRect(handle, out rect))
            {
                MessageBox.Show("We somehow failed at `GetWindowRect`");
                return 1;
            }

            // RECT -> Rectangle
            Rectangle bounds = new Rectangle();
            bounds.X = rect.Left;
            bounds.Y = rect.Top;
            bounds.Width = rect.Right - rect.Left + 1;
            bounds.Height = rect.Bottom - rect.Top + 1;

            // Capture
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left,bounds.Top), Point.Empty, bounds.Size);
                }
                bitmap.Save("capture.png", ImageFormat.Png);
            }
            return 0;
        }

        //
        // PInvokes
        //

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }
    }
}

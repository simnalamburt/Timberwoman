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
            var handle = FindWindow("UnityWndClass", "Timberman");
            if ((long)handle == 0) {
                Console.Out.WriteLine("Please run Timberman first.");
                return 1;
            }

            Console.Out.WriteLine(handle);
            return 0;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}

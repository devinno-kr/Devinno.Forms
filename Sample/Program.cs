using Devinno.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var ENCODER_DOWNSET_VALUE = 1000000000;
            var v1 = 1000297790;
            var v2 = 0999659700;
            
            var EncoderFinalUP = 1000297773;
            var EncoderFinalDN = ENCODER_DOWNSET_VALUE;

            var r1 = ((v1 - ENCODER_DOWNSET_VALUE) % (EncoderFinalUP - EncoderFinalDN)) + ENCODER_DOWNSET_VALUE;
            var r2 = ((v2 - ENCODER_DOWNSET_VALUE) % (EncoderFinalUP - EncoderFinalDN)) + ENCODER_DOWNSET_VALUE;


            var rr1 = 3000318 % 300032;
            var rr2 = -3000318 % 300032;
            DVLIB.Preload();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}

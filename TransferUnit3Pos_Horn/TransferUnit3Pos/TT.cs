using System;
using System.Timers;

namespace TransferUnit3Pos
{
    class TT
    {
        Timer t = new Timer();
        public void start()
        {
            t.Enabled = true;
            t.Interval = 50000;

        }

        public void stop()
        {
            t.Enabled = false;
        }

    }
}


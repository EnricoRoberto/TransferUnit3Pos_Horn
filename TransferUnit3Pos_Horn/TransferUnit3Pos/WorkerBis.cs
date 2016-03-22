using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferUnit3Pos
{
    public class WorkerBis
    {
        public static int inc = 0;
        private Dati data = new Dati();
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;
        // This method will be called when the thread is started.

        public void DoWork()
        {
            while (!_shouldStop)
            {
                Console.WriteLine(Convert.ToString(inc++));
                //   Console.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId + "ciaooo");
                if (inc > 5000)
                {
                    inc = 0;
                }
                ///////////////////////// inserire qui le assegnazioni degli input/output da trasferire alla scheda                          
                loadConfig();
                ////

            }
            Console.WriteLine("worker thread: terminating gracefully.");
        }
        public void RequestStop()
        {
            _shouldStop = true;

        }
        private void loadConfig()
        {
          
        }
    }
}

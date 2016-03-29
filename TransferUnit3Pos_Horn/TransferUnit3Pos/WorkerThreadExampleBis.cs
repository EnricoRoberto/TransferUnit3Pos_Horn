using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TransferUnit3Pos
{
    public class WorkerThreadExampleBis
    {
        private WorkerBis workerObject;
        private Thread workerThread;

        public void Main()
        {
            // Create the thread object. This does not start the thread.
            workerObject = new WorkerBis();
            workerThread = new Thread(workerObject.DoWork);

            // Start the worker thread.
            workerThread.Start();
            Console.WriteLine("main thread: Starting worker thread...");

            // Loop until worker thread activates.
            while (!workerThread.IsAlive) ;

            // Put the main thread to sleep for 1 millisecond to
            // allow the worker thread to do some work:
            Thread.Sleep(10);
            // Request that the worker thread stop itself:
            //*            workerObject.RequestStop();
            // Use the Join method to block the current thread 
            // until the object's thread terminates.
            //*            workerThread.Join(1);
            Console.WriteLine("main thread: Worker thread has terminated.");
        }
        public void stop()
        {
            if (workerObject != null)
            {
                workerObject.RequestStop();

            }

            if (workerThread != null)
            {
                workerThread.Join();
            }


        }
    }
}

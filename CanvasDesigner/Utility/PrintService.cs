using System;
using System.IO;

namespace CanvasDesigner.Utility
{
    /// <summary>
    /// TODO: Integrate this service to solution
    /// redirect output according to creation.
    /// </summary>
    [Obsolete("Not used anymore", true)]
    public class PrintService :IDisposable
    {
        public PrintService(TextWriter writer=null)
        {
            if (writer != null)
            {
                Console.SetOut(writer);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            }
        }

        public void Print(string s)
        {
            Console.Write(s);
        }

        public void PrintLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}

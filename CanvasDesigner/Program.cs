using System;

namespace CanvasDesigner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var inputFileProcessorAndResponseHandler = new InputFileProcessorAndResponseHandler();
             inputFileProcessorAndResponseHandler.Start();
           /* inputFileProcessorAndResponseHandler.ParseAndProcessData("C 20 4");
            inputFileProcessorAndResponseHandler.ParseAndProcessData("L 1 2 6 2");


            inputFileProcessorAndResponseHandler.ParseAndProcessData("L 6 3 6 4");
            inputFileProcessorAndResponseHandler.ParseAndProcessData("R 14 1 18 3");
            inputFileProcessorAndResponseHandler.ParseAndProcessData("B 10 3 o");
            */
            Console.ReadLine();
        }
    }
}

using CanvasDesigner.Exceptions;
using CanvasDesigner.Utility;
using System;
using System.Text.RegularExpressions;

namespace CanvasDesigner
{
    public class InputFileProcessorAndResponseHandler
    {
        public void Start()
        {
            ProcessCommands();
        }

        public void ProcessCommands()
        {
            
            while (true)
            {
                Console.Write("Enter Command:");
                var line = Console.ReadLine();
                try
                {
                    ParseAndProcessData(line);
                }
                catch (UnInitailizedCanvasException)
                {
                    Console.WriteLine("Uninitialized Canvas Error: Please create canvas first...");
                }
                catch (InvalidSizeException ex)
                {
                    Console.WriteLine("Invalid Size Exception: {0}", ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }

        }


        public void ParseAndProcessData(string line)
        {
            line = Regex.Replace(line, @"\s+", " ");
            //TODO:Can add all patterns of statement in a list and 
            //loop through to find match instead of following
            
            foreach (var pattern in InputPatternsAndCommandHandlersMap.GetinputPatterns())
            {
                if (Regex.IsMatch(line, pattern,RegexOptions.IgnoreCase))
                {
                    var handler = InputPatternsAndCommandHandlersMap.GetCommandHandler(pattern);
                    handler.ProcessCommand(line);
                    return;
                }
            }
            throw new InvalidCommandException();
            

        }
    }
}

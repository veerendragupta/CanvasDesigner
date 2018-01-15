using CanvasDesigner.Interface;
using System;
using System.Text;

namespace CanvasDesigner.CommandHandlers
{
    public class HelpCommandHandler : ICanvasCommandHandler
    {
        public void ProcessCommand(string line)
        {
            StringBuilder helpText = new StringBuilder();
            helpText.AppendLine("Supported command and their syntax are following");
            helpText.AppendLine("1. Create canvas :C w h ");
            helpText.AppendLine("   Example: C 20 4");

            helpText.AppendLine("2. Draw new horizontal or verticle Line between point (x1,y1) and (x2,y2): L x1 y1 x2 y2");
            helpText.AppendLine("   Example- horizontal line: L 1 2 6 2");
            helpText.AppendLine("   Example- verticle line: L 6 3 6 4");
            
            helpText.AppendLine("3. Draw rectangle whose upper left corner is (x1,y1) and lower right corner is (x2,y2) : R x1 y1 x2 y2");
            helpText.AppendLine("   Example- draw rectangle:  R 14 1 18 3");
            

            helpText.AppendLine("4. bucket fill command - Fill the entire area connected to (x,y) with colour c: B x y c");
            helpText.AppendLine("   Example- point(10,3), color('o'): B 10 3 o");


            helpText.AppendLine("5.  Clear everything: CLEAR");
            helpText.AppendLine("6.  Quit:Q");

            helpText.AppendLine("7.  Help:HELP");
            Console.Write(helpText.ToString());
        }
    }
}

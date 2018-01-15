using CanvasDesigner.Interface;

namespace CanvasDesigner.CommandHandlers
{
    public class QuitCommandHandler : ICanvasCommandHandler
    {
        public void ProcessCommand(string line)
        {
            System.Environment.Exit(0);
        }
    }
}

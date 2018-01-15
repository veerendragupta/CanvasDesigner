using CanvasDesigner.Interface;

namespace CanvasDesigner.CommandHandlers
{
    public class ClearCanvasCommandHandler :ICanvasCommandHandler
    {
        Canvas canvas = Canvas.GetInstance();

        public void ProcessCommand(string line)
        {
            canvas.Clear();
            canvas.AddCommandHistoryToCanvas(line);
            canvas.PrintCanvas();
        }
    }
}

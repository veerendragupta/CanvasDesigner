using CanvasDesigner.Interface;
using System;

namespace CanvasDesigner
{
    public class DrawLineCommandHandler : ICanvasCommandHandler
    {
        Canvas canvas = Canvas.GetInstance();
        int row1, row2, column1, column2;

        public void ProcessCommand(string line)
        {
            ParseInput(line);
            VerifyCommandArguments(row1,  column1,  row2, column2);
            canvas.FillLineInCanvas(row1, column1, row2, column2);
            canvas.AddCommandHistoryToCanvas(line);
            canvas.PrintCanvas();
        }

        protected void ParseInput(string line)
        {
            var args = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            int.TryParse(args[1], out column1);
            int.TryParse(args[2], out row1);
            int.TryParse(args[3], out column2);
            int.TryParse(args[4], out row2);
        }

        protected void VerifyCommandArguments(int row1,int column1,int row2, int column2)
        {
            if (!canvas.DoesPointLiesInsideCanvas(row1, column1) )
                throw new Exception(String.Format("Point ({1}, {0}) are outside of canvas. Canvas dimensions are - width:{2} height:{3}",row1,column1,canvas.getWidth(),canvas.getHeight()));
            if (!canvas.DoesPointLiesInsideCanvas(row2, column2))
                throw new Exception(String.Format("Point ({1}, {0}) is outside of canvas. Canvas dimensions are - width:{2} height:{3}", row2, column2, canvas.getWidth(), canvas.getHeight()));
            if (row1 != row2 && column1 != column2)
                throw new Exception("Only horizontal and verticle lines are supported");
            if (!canvas.IsLineSpaceEmpty(row1, column1, row2, column2))
                throw new Exception("Line can not cross or override existing drawings");
        }

        

        

    }
}

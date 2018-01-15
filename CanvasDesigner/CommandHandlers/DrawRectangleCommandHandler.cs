using CanvasDesigner.Interface;
using System;

namespace CanvasDesigner
{
    public class DrawRectangleCommandHandler : ICanvasCommandHandler
    {
        int point1Row, point2Row, point1Column, point2Column;
        Canvas canvas = Canvas.GetInstance();

        public void ProcessCommand(string line)
        {
            ParseArguments(line);
            VerifyCommandArguments();
            DrawLinesOnCanvas();
            canvas.AddCommandHistoryToCanvas(line);
            canvas.PrintCanvas();
        }

        protected void ParseArguments(string line)
        {
            var args = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);           
            int.TryParse(args[1], out point1Column);
            int.TryParse(args[2], out point1Row);
            int.TryParse(args[3], out point2Column);
            int.TryParse(args[4], out point2Row);
        }

        protected void VerifyCommandArguments() {
            if (!canvas.DoesPointLiesInsideCanvas(point1Row, point1Column))
                throw new Exception(String.Format("Point ({1}, {0}) are outside of canvas. Canvas dimensions are - width:{2} height:{3}", point1Row, point1Column, canvas.getWidth(), canvas.getHeight()));
            if (!canvas.DoesPointLiesInsideCanvas(point2Row, point2Column))
                throw new Exception(String.Format("Point ({1}, {0}) is outside of canvas. Canvas dimensions are - width:{2} height:{3}", point2Row, point2Column, canvas.getWidth(), canvas.getHeight()));

            if (!(canvas.IsLineSpaceEmpty(point1Row, point1Column, point2Row, point1Column)&&
                  canvas.IsLineSpaceEmpty(point2Row, point1Column, point2Row, point2Column)&&
                  canvas.IsLineSpaceEmpty(point1Row, point2Column, point2Row, point2Column)&&
                  canvas.IsLineSpaceEmpty(point1Row, point1Column, point1Row, point2Column)))
                throw new Exception("Rectangle's sides can not cross existing drawings");

            if (point1Column >= point2Column)
            {
                throw new Exception("Point1 should be on left side of point2");
            }
            if (point1Row >= point2Row)
            {
                throw new Exception("Point1 should be on upper side of point2");
            }
        }

        protected void DrawLinesOnCanvas(){
            canvas.FillLineInCanvas(point1Row, point1Column, point1Row, point2Column);
            canvas.FillLineInCanvas(point2Row, point1Column, point2Row, point2Column);
            canvas.FillLineInCanvas(point1Row+1, point1Column, point2Row-1, point1Column);            
            canvas.FillLineInCanvas(point1Row+1, point2Column, point2Row-1, point2Column);
            
        }
    }
}

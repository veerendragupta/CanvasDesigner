using CanvasDesigner.Interface;
using System;

namespace CanvasDesigner
{
    public class FillConnectedAreaCommandHandler : ICanvasCommandHandler
    {
        int column, row;
        char color;
        Canvas canvas = Canvas.GetInstance();
        public void ProcessCommand(string line)
        {
            ParseInput(line);
            VerifyCommandArguments();
            FillPointAndNeighbours(row,column, color);
            canvas.AddCommandHistoryToCanvas(line);
            canvas.PrintCanvas();
        }

        private void ParseInput(string line)
        {
            var args = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            int.TryParse(args[1], out column);
            int.TryParse(args[2], out row);
            char.TryParse(args[3], out color);
        }

        protected void VerifyCommandArguments()
        {
            if (!canvas.DoesPointLiesInsideCanvas(row, column) )
                throw new Exception("Point is outside of canvas");
            if (canvas.IsCanvasCellFilled(row, column))
                throw new Exception("Point is already on existing shape, no area will be filled.");
        }

            private void FillPointAndNeighbours(int row, int column, char color)
        {
            if (!canvas.DoesPointLiesInsideCanvas(row, column) || canvas.IsCanvasCellFilled(row, column))
                return;

            canvas.FillCanvasCell(row, column, color);

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = column - 1; j <= column + 1; j++)
                {
                    FillPointAndNeighbours(i, j, color);
                }
            }
        }


    }
}

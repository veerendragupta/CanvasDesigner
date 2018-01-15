using CanvasDesigner.Exceptions;
using CanvasDesigner.Interface;
using System;
using System.Numerics;

namespace CanvasDesigner
{
    public class CreateCanvasCommandHandler : ICanvasCommandHandler
    {
        Canvas canvas = Canvas.GetInstance();
        int w, h = -1;
        public void ProcessCommand(string line)
        {
            ParseInput(line);
            VerifyCommandParameters(w, h); 
            canvas.InitializeCanvas(w, h);
            canvas.PrintCanvas();
        }

        private void ParseInput(string line)
        {
            var args = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            var c= int.TryParse(args[1], out w);
            if (!c)
            {
                throw new InvalidSizeException(string.Format("width should  be less than {0}", int.MaxValue));
            }
            c=int.TryParse(args[2], out h);
            if (!c)
            {
                throw new InvalidSizeException(string.Format("width should  be less than {0}", int.MaxValue));
            }
        }

        /// <summary>
        /// Throws Exception if height or width is invalid 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        private void VerifyCommandParameters(int w, int h)
        {
            if (h <= 0)
                throw new InvalidSizeException("Height should be greater than 0");

            if(w<=0)
                throw new InvalidSizeException("Width should be greater than 0");
            

            try
            {
                checked
                {
                    int product = w*h;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidSizeException(string.Format("Canvas area can not be that big!! Please keep width*height less than {0}",int.MaxValue));
            }
        }
    }
}

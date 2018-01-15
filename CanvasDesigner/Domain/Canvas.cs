using CanvasDesigner.Exceptions;
using System;
using System.Collections.Generic;

namespace CanvasDesigner
{
    /// <summary>
    /// Stores actual canvas object  
    /// Instance will be singleton during the whole lifecycle of application.
    /// </summary>
    public class Canvas
    {

        private int _width = -1;



        private int _height = -1;

        private char[,] _canvasMatrix;
        private Stack<string> _commandHistory;

        private Canvas()
        {
            _commandHistory = new Stack<string>();
        }
        private static Canvas _instance;
        /// <summary>
        /// Return singleton instance of canvas
        /// </summary>
        /// <returns></returns>
        public static Canvas GetInstance()
        {
            if (_instance == null)
                _instance = new Canvas();
            return _instance;
        }

        /// <summary>
        /// Width of canvas
        /// </summary>
        /// <returns>width of canvas</returns>
        public int getWidth()
        {
            if (_width == -1)
                throw new UnInitailizedCanvasException();
            return _width;
        }

        /// <summary>
        /// Height of canvas
        /// </summary>
        /// <returns>Height of canvas</returns>
        public int getHeight()
        {
            if (_width == -1)
                throw new UnInitailizedCanvasException();
            return _height;
        }

        /// <summary>
        /// Creates a canvas of width w and height h
        /// </summary>
        /// <param name="h">height of canvas</param>
        /// <param name="w">width of canvas</param>
        public void InitializeCanvas(int w, int h)
        {
            _height = h;
            _width = w;
            _canvasMatrix = new char[h + 2, w + 2];

            for (int i = 0; i < h + 2; i++)
            {
                for (int j = 0; j < w + 2; j++)
                {
                    _canvasMatrix[i, j] = AppConstants.BACKGROUND_COLOR;
                }
            }


            for (int i = 0; i <= w + 1; i++)
            {
                FillCanvasCell(0, i, AppConstants.HORIZONTAL_BOUNDRY);
                FillCanvasCell(h + 1, i, AppConstants.HORIZONTAL_BOUNDRY);
            }

            for (int i = 1; i <= h; i++)
            {
                FillCanvasCell(i, 0, AppConstants.VERTICLE_BOUNDRY);
                FillCanvasCell(i, w + 1, AppConstants.VERTICLE_BOUNDRY);
            }
        }

        /// <summary>
        /// Prints current canvas design on console 
        /// </summary>
        public void PrintCanvas()
        {
            for (int i = 0; i < _height + 2; i++)
            {
                for (int j = 0; j < _width + 2; j++)
                {
                    Console.Write(_canvasMatrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Removes all the shapes from canvas
        /// </summary>
        public void Clear()
        {
            for (int i = 1; i <= _height; i++)
            {
                for (int j = 1; j <= _width ; j++)
                {
                    _canvasMatrix[i, j] = AppConstants.BACKGROUND_COLOR;
                }
            }
        }

        /// <summary>
        /// Fills a cell with character c at position (x,y) of canvas
        /// returns true if successfully filled else false
        /// Throws Exception if cell if already filled
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool FillCanvasCell(int row, int column, char c)
        {

            if (_canvasMatrix == null)
                throw new Exception("Canvas is not initialized");
            if (_canvasMatrix[row, column] != AppConstants.BACKGROUND_COLOR)
                throw new Exception(string.Format("Canvas is already filled at location" +  " " + column.ToString() +" "+ row.ToString() ));
            if (row < 0 || row > _height + 1 | column < 0 || column > _width + 1)
                throw new Exception("Can not fill beyond canvas borders");

            _canvasMatrix[row, column] = c;
            return true;
        }


        /// <summary>
        /// Check if a point(row,column) is filled.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>True if already filled otherwise false</returns>
        public bool IsCanvasCellFilled(int row, int column)
        {
            if (_canvasMatrix == null)
                throw new Exception("Canvas is not initialized");
            if (row < 0 || row > _height + 1 | column < 0 || column > _width + 1)
                throw new Exception("point lies beyond canvas borders");
            if (_canvasMatrix[row, column] != AppConstants.BACKGROUND_COLOR)
                return true;
            return false;
        }

        /// <summary>
        /// Check if point(row,column) lies inside canvas or not.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>false if point is outside of canvas otherwise returns true</returns>
        public bool DoesPointLiesInsideCanvas(int row, int column)
        {
            if (row < 1 || row > _height | column < 1 || column > _width)
                return false;
            return true;
        }

        /// <summary>
        /// Add all the commands applied on canvas sucessfully so far;
        /// </summary>
        /// <param name="shapeDetails"></param>
        public void AddCommandHistoryToCanvas(string command)
        {
            this._commandHistory.Push(command);
        }

        /// <summary>
        /// Returns true If line connecting from point1(row1,column1) to point2(row2,column2) not filled already
        /// Only horizontal and verticle lines are considered
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="column1"></param>
        /// <param name="row2"></param>
        /// <param name="column2"></param>
        /// <returns>False if already  filled</returns>
        public bool IsLineSpaceEmpty(int row1, int column1, int row2, int column2)
        {
            var filled = false;
            if (column1 == column2)
            {
                for (int i = row1; i < row2; i++)
                    filled = filled || IsCanvasCellFilled(i, column1);
            }
            else if (row1 == row2)
            {
                for (int i = column1; i < column2; i++)
                    filled = filled || IsCanvasCellFilled(row1, i);
            }
            return !filled;
        }

        /// <summary>
        /// Fill all the points verticle or horizontal line in canvas
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="column1"></param>
        /// <param name="row2"></param>
        /// <param name="column2"></param>
        public void FillLineInCanvas(int row1, int column1, int row2, int column2)
        {
            if (column1 == column2)
            {
                for (int i = row1; i <= row2; i++)
                {
                    FillCanvasCell(i, column1, AppConstants.LINE_COLOR);
                }
            }
            else if (row1 == row2)
            {
                for (int i = column1; i <= column2; i++)
                {
                    FillCanvasCell(row1, i, AppConstants.LINE_COLOR);
                }
            }
        }


    }
}

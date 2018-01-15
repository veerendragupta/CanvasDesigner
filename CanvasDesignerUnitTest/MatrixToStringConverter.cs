namespace CanvasDesignerUnitTest
{

    public class MatrixToStringConverter 
    {
        /// <summary>
        /// Prints current canvas design on console 
        /// </summary>
        public static string Convert(char[,] matrix)
        {
            string outString = string.Empty;
            for (int i = 0; i < matrix.GetLength(0) ; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) ; j++)
                {
                    outString += matrix[i, j];
                }
                outString += "\r\n"; 
            }
            return outString;
        }
    }
}

using System;

namespace CanvasDesigner.Exceptions
{
    [Serializable]
    public class InvalidSizeException : System.Exception
    {
        public InvalidSizeException(string message):base(message)
        {
        }

    }
}

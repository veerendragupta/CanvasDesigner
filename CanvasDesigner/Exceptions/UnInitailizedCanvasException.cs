using System;

namespace CanvasDesigner.Exceptions
{
    [Serializable]
    public class UnInitailizedCanvasException : System.Exception
    {
        public UnInitailizedCanvasException():base("Canvas is not initialized")
        {

        }
    }
}

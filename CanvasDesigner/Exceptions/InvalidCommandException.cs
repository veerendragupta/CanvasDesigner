using System;

namespace CanvasDesigner.Exceptions
{
    [Serializable]
    public class InvalidCommandException :Exception
    {
        public InvalidCommandException():base("unknown command or invalid parameters,Please type <HELP> to know about supported commands and parameters")
        {

        }
    }
}

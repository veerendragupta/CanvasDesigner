using CanvasDesigner.Interface;
using System;

namespace CanvasDesigner
{
    [Obsolete("Not used anymore", true)]
    public interface IHandlerFactory
    {
        ICanvasCommandHandler CreateHandler(string handlertype);
    }
}
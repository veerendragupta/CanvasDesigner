using CanvasDesigner.Interface;
using System;

namespace CanvasDesigner
{
    [Obsolete("Not used anymore", true)]
    public interface IHandler
    {
        void ProcessLine(string line);
    }

    [Obsolete("Not used anymore", true)]
    public class HandlerFactory : IHandlerFactory
    {
            public ICanvasCommandHandler CreateHandler(string handlertype)
            {
                switch (handlertype)
                {
                    //TODO: each handler can be created as singleton.
                    case AppConstants.CREATE_CANVAS_COMMAND_PATTERN:
                        return new CreateCanvasCommandHandler();
                    case AppConstants.DRAW_LINE_COMMAND_PATTERN:
                         return new DrawLineCommandHandler();
                    case AppConstants.DRAW_RECTANGLE_COMMAND_PATTERN:
                         return new DrawRectangleCommandHandler();
                    case AppConstants.FILL_CONNECTED_AREA_COMMAND_PATTERN:
                        return new FillConnectedAreaCommandHandler();
            }
                return null;
            }
        }
}
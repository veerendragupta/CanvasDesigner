using CanvasDesigner.CommandHandlers;
using CanvasDesigner.Exceptions;
using CanvasDesigner.Interface;
using System;
using System.Collections.Generic;

namespace CanvasDesigner.Utility
{
    public static class InputPatternsAndCommandHandlersMap
    {
        static Dictionary<string, ICanvasCommandHandler> Map;

        static InputPatternsAndCommandHandlersMap()
        {
            Map = new Dictionary<string, ICanvasCommandHandler>
            {
                { AppConstants.CREATE_CANVAS_COMMAND_PATTERN, new CreateCanvasCommandHandler() },
                { AppConstants.DRAW_LINE_COMMAND_PATTERN, new DrawLineCommandHandler() },
                { AppConstants.DRAW_RECTANGLE_COMMAND_PATTERN, new DrawRectangleCommandHandler() },
                { AppConstants.FILL_CONNECTED_AREA_COMMAND_PATTERN, new FillConnectedAreaCommandHandler() },
                { AppConstants.QUIT_COMMAND_PATTERN, new QuitCommandHandler() },
                { AppConstants.HELP_COMMAND_PATTERN, new HelpCommandHandler() },
                { AppConstants.CLEAR_COMMAND_PATTERN, new ClearCanvasCommandHandler() }
            };
        }

        public static IEnumerable<String> GetinputPatterns()
        {
            return Map.Keys; 
        }

        public static ICanvasCommandHandler GetCommandHandler(string inputPattern)
        {
            if (!Map.ContainsKey(inputPattern))
                throw new InvalidCommandException();
            return Map[inputPattern];
        }
    }
}

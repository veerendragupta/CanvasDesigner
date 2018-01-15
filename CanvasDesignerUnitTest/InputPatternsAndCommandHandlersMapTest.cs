using CanvasDesigner;
using CanvasDesigner.CommandHandlers;
using CanvasDesigner.Exceptions;
using CanvasDesigner.Utility;
using NUnit.Framework;
using System.Collections.Generic;

namespace CanvasDesignerUnitTest
{

    [TestFixture]
    class InputPatternsAndCommandHandlersMapTest
    {
        IEnumerable<string> commandPatterns;

        [SetUp]
        public void InputPatternsAndCommandHandlersMapTestSetup()
        {
            commandPatterns = InputPatternsAndCommandHandlersMap.GetinputPatterns();
        }

        [Test]
        public void TestValidCommandsMapping()
        {
            Assert.AreEqual(InputPatternsAndCommandHandlersMap.GetCommandHandler(AppConstants.CREATE_CANVAS_COMMAND_PATTERN).GetType(),typeof(CreateCanvasCommandHandler));
            Assert.AreEqual(InputPatternsAndCommandHandlersMap.GetCommandHandler(AppConstants.DRAW_LINE_COMMAND_PATTERN).GetType(), typeof(DrawLineCommandHandler));
            Assert.AreEqual(InputPatternsAndCommandHandlersMap.GetCommandHandler(AppConstants.DRAW_RECTANGLE_COMMAND_PATTERN).GetType(), typeof(DrawRectangleCommandHandler));
            Assert.AreEqual(InputPatternsAndCommandHandlersMap.GetCommandHandler(AppConstants.FILL_CONNECTED_AREA_COMMAND_PATTERN).GetType(), typeof(FillConnectedAreaCommandHandler));
            Assert.AreEqual(InputPatternsAndCommandHandlersMap.GetCommandHandler(AppConstants.HELP_COMMAND_PATTERN).GetType(), typeof(HelpCommandHandler));
            Assert.AreEqual(InputPatternsAndCommandHandlersMap.GetCommandHandler(AppConstants.QUIT_COMMAND_PATTERN).GetType(), typeof(QuitCommandHandler));
        }

        [Test]
        public void TestInValidCommandsMapping()
        {
            Assert.Throws<InvalidCommandException>(()=>InputPatternsAndCommandHandlersMap.GetCommandHandler("invalid command"));
            
        }

        }
}

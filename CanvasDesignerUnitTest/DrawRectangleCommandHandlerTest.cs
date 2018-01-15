using CanvasDesigner;
using CanvasDesigner.Interface;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace CanvasDesignerUnitTest
{
    [TestFixture]
    public class DrawRectangleCommandHandlerTest 
    {

        private TextWriter originalOut;
        private StringWriter stringWriter;
        private ICanvasCommandHandler rectangleCommandHandler;
        private string emptyCanvasString;

        [SetUp]
        public void DrawRectangleCommandHandlerTestSetUp()
        {
            originalOut = Console.Out;
            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var createCanvas = new CreateCanvasCommandHandler();
            createCanvas.ProcessCommand("C 3 4");
            rectangleCommandHandler = new DrawRectangleCommandHandler();
            emptyCanvasString = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', ' ', ' ', ' ', '|' }, { '|', ' ', ' ', ' ', '|' }, { '|', ' ', ' ', ' ', '|' }, { '|', ' ', ' ', ' ', '|' }, { '-', '-', '-', '-', '-' }, });

        }

        [TestCase("R 1 1 3 3")]
        public void TestDrawValidRectangleCommand(string command)
        {
            Assert.NotNull(stringWriter.ToString());
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString);

            rectangleCommandHandler.ProcessCommand(command);
            Assert.AreEqual(stringWriter.ToString().Count(c => c.Equals('x')), 8);

            var canvasStringWithRectangle = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', 'x', 'x', 'x', '|' }, { '|', 'x', ' ', 'x', '|' }, { '|', 'x', 'x', 'x', '|' }, { '|', ' ', ' ', ' ', '|' }, { '-', '-', '-', '-', '-' }, });

            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + canvasStringWithRectangle);


        }

        [TestCase("R 1 1 8 3")]
        [TestCase("R -1 0 2 2")]
        [TestCase("R 1 1 5 5")]
        public void TestOutSidePointDrawRectangleCommand(string command)
        {
            Assert.Throws<Exception>(() => rectangleCommandHandler.ProcessCommand(command));
        }

        [TestCase("R 1 1 3 3","R 1 1 3 3")]
        [TestCase("R 1 1 3 3", "R 3 1 4 3")]
        [TestCase("R 1 1 3 3", "R 3 3 4 4")]
        public void TestCrossingRectanglesCommand(string command1,string command2)
        {
            rectangleCommandHandler.ProcessCommand(command1);
            Assert.Throws<Exception>(() => rectangleCommandHandler.ProcessCommand(command2));
        }

        [TestCase("R 3 2 2 2")]
        [TestCase("R 2 3 1 1")]
        public void TestPoint1OnRightOfPoint2Command(string command)
        {
            Assert.Throws<Exception>(() => rectangleCommandHandler.ProcessCommand(command));
        }

        [TestCase("R 3 3 4 2")]
        [TestCase("R 2 3 1 1")]
        public void TestPoint2OnUpperSideOdPoint1Command(string command)
        {
            Assert.Throws<Exception>(() => rectangleCommandHandler.ProcessCommand(command));
        }
    }
}

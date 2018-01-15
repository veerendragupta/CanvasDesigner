using CanvasDesigner;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using CanvasDesigner.Interface;

namespace CanvasDesignerUnitTest
{
    [TestFixture]
    public class DrawLineCommandHandlerTest
    {
        private TextWriter originalOut;
        private StringWriter stringWriter;
        private ICanvasCommandHandler lineHandler;
        private string emptyCanvasString;

        [SetUp]
        public void DrawLineCommandHandlerTestSetUp()
        {
            originalOut = Console.Out;
            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var createCanvas = new CreateCanvasCommandHandler();
            createCanvas.ProcessCommand("C 3 2");
            lineHandler = new DrawLineCommandHandler();
            emptyCanvasString = MatrixToStringConverter.Convert(new char[4, 5] { { '-', '-', '-', '-', '-' }, { '|', ' ', ' ', ' ', '|' }, { '|', ' ', ' ', ' ', '|' }, { '-', '-', '-', '-', '-' }, });


        }

        [TestCase("L 1 2 3 2")]
        public void TestDrawHorizontalLinesCommand(string command)
        {
            Assert.NotNull(stringWriter.ToString());
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString);
            lineHandler.ProcessCommand(command);
            Assert.AreEqual(stringWriter.ToString().Count(c => c.Equals('x')), 3);

            var lineContainingCanvas = MatrixToStringConverter.Convert(new char[4, 5] { { '-', '-', '-', '-', '-' }, { '|', ' ', ' ', ' ', '|' }, { '|', 'x', 'x', 'x', '|' }, { '-', '-', '-', '-', '-' }, });
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + lineContainingCanvas);

        }

        [TestCase("L 1 1 1 2")]
        public void TestDrawVerticleLineCommand(string command)
        {
            Assert.NotNull(stringWriter.ToString());
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString);

            lineHandler.ProcessCommand(command);
            Assert.AreEqual(stringWriter.ToString().Count(c => c.Equals('x')), 2);

            var lineContainingCanvas = MatrixToStringConverter.Convert(new char[4, 5] { { '-', '-', '-', '-', '-' }, { '|', 'x', ' ', ' ', '|' }, { '|', 'x', ' ', ' ', '|' }, { '-', '-', '-', '-', '-' }, });


            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + lineContainingCanvas);

        }

        [TestCase("L 1 1 2 2")]
        [TestCase("L 2 2 3 3")]
        [TestCase("L 1 1 3 3")]
        public void TestDrawDiagonalLineCommand(string command)
        {
            Assert.Throws<Exception>(() => lineHandler.ProcessCommand(command));
        }

        [TestCase("L 1 1 4 2")]
        [TestCase("L 5 1 2 2")]
        [TestCase("L 6 1 6 2")]
        public void TestOutOfCanvasPoints(string command)
        {
            Assert.Throws<Exception>(() => lineHandler.ProcessCommand(command));
        }

        [TestCase("L 1 2 2 2", "L 1 2 2 2")]
        [TestCase("L 1 2 2 2","L 2 2 3 2")]
        [TestCase("L 1 2 2 2", "L 2 2 2 3")]
        public void TestCrossExistingLine(string command1,string command2)
        {
            lineHandler.ProcessCommand(command1);
            Assert.Throws<Exception>(() => lineHandler.ProcessCommand(command2));
        }

        [TearDown]
        public void DrawLineCommandHandlerTestTearDown()
        {
            Console.SetOut(originalOut);
            stringWriter.Dispose();
        }

    }
    
}

using System;
using NUnit.Framework;
using CanvasDesigner;
using CanvasDesigner.Interface;
using System.IO;
using System.Linq;

namespace CanvasDesignerUnitTest
{
    [TestFixture]
    public class MultipleCommandsTests
    {
        private TextWriter originalOut;
        private StringWriter stringWriter;
        private ICanvasCommandHandler lineCommandHandler;
        private ICanvasCommandHandler rectangleCommandHandler;
        private ICanvasCommandHandler bucketFillCommandHandler;
        private string emptyCanvasString;

        [SetUp]
        public void MultipleCommandsTestsSetUp()
        {
            originalOut = Console.Out;
            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var createCanvas = new CreateCanvasCommandHandler();
            createCanvas.ProcessCommand("C 3 4");

            lineCommandHandler = new DrawLineCommandHandler();
            rectangleCommandHandler = new DrawRectangleCommandHandler();
            bucketFillCommandHandler = new FillConnectedAreaCommandHandler();
            emptyCanvasString = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', ' ', ' ', ' ', '|' }, { '|', ' ', ' ', ' ', '|' }, { '|', ' ', ' ', ' ', '|' }, { '|', ' ', ' ', ' ', '|' }, { '-', '-', '-', '-', '-' } });

        }

        [TestCase("L 3 1 3 4", "R 1 1 2 2")]
        public void TestValidLineAndRectangleCommand(string lineCommand,string rectangleCommand)
        {
            lineCommandHandler.ProcessCommand(lineCommand);
            var lineOnCanvasString = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', ' ', ' ', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '-', '-', '-', '-', '-' }, });
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + lineOnCanvasString);

            rectangleCommandHandler.ProcessCommand(rectangleCommand);
            var lineAndRectangleOnCanvasString = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', 'x', 'x', 'x', '|' }, { '|', 'x', 'x', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '-', '-', '-', '-', '-' }, });
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + lineOnCanvasString + lineAndRectangleOnCanvasString);
        }


        [TestCase("L 3 1 3 4", "R 1 1 3 3")]
        public void TestLineAndCrossingRectangleCommand(string lineCommand, string rectangleCommand)
        {
            lineCommandHandler.ProcessCommand(lineCommand);
            var lineOnCanvasString = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', ' ', ' ', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '-', '-', '-', '-', '-' }, });
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + lineOnCanvasString);
            Assert.Throws<Exception>(()=> rectangleCommandHandler.ProcessCommand(rectangleCommand));
           
        }

        [TestCase("R 1 1 3 4","B 2 2 p")]
        public void TestDrawRectangleAndBucketFillCommand(string rectangleCommand,string fillCommand)
        {
            Assert.NotNull(stringWriter.ToString());
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString);

            rectangleCommandHandler.ProcessCommand(rectangleCommand);
            Assert.AreEqual(stringWriter.ToString().Count(c => c.Equals('x')), 10);
             var canvasStringWithRectangle = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', 'x', 'x', 'x', '|' }, { '|', 'x', ' ', 'x', '|' }, { '|', 'x', ' ', 'x', '|' }, { '|', 'x', 'x', 'x', '|' }, { '-', '-', '-', '-', '-' }, });

             Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + canvasStringWithRectangle);

            bucketFillCommandHandler.ProcessCommand(fillCommand);
            var canvasStringWithRectangleAndBucketFilled = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', 'x', 'x', 'x', '|' }, { '|', 'x', 'p', 'x', '|' }, { '|', 'x', 'p', 'x', '|' }, { '|', 'x', 'x', 'x', '|' }, { '-', '-', '-', '-', '-' }, });

            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + canvasStringWithRectangle + canvasStringWithRectangleAndBucketFilled);



        }

        [TestCase("L 3 1 3 4", "R 1 1 2 2","B 1 3 o")]
        public void TestValidLineRectangleAndFillBucketCommand(string lineCommand, string rectangleCommand,string fillBucketCommand)
        {
            lineCommandHandler.ProcessCommand(lineCommand);
            var lineOnCanvasString = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', ' ', ' ', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '-', '-', '-', '-', '-' }, });
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + lineOnCanvasString);

            rectangleCommandHandler.ProcessCommand(rectangleCommand);
            var lineAndRectangleOnCanvasString = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', 'x', 'x', 'x', '|' }, { '|', 'x', 'x', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '|', ' ', ' ', 'x', '|' }, { '-', '-', '-', '-', '-' }, });
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + lineOnCanvasString + lineAndRectangleOnCanvasString);

            bucketFillCommandHandler.ProcessCommand(fillBucketCommand);
            var lineAndRectangleOnCanvasWithBucketFillString = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', 'x', 'x', 'x', '|' }, { '|', 'x', 'x', 'x', '|' }, { '|', 'o', 'o', 'x', '|' }, { '|', 'o', 'o', 'x', '|' }, { '-', '-', '-', '-', '-' }, });
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + lineOnCanvasString + lineAndRectangleOnCanvasString+ lineAndRectangleOnCanvasWithBucketFillString);


        }




    }
}

using System;
using NUnit.Framework;
using CanvasDesigner;
using CanvasDesigner.Interface;
using System.IO;


namespace CanvasDesignerUnitTest
{
    /// <summary>
    /// Summary description for FillConnectedAreaCommandHandlerTest
    /// </summary>
    [TestFixture]
    public class FillConnectedAreaCommandHandlerTest
    {
        private TextWriter originalOut;
        private StringWriter stringWriter;
        private ICanvasCommandHandler bucketFillCommandHandler;
        private string emptyCanvasString;

        [SetUp]
        public void FillConnectedAreaCommandHandlerTestSetUp()
        {
            originalOut = Console.Out;
            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var createCanvas = new CreateCanvasCommandHandler();
            createCanvas.ProcessCommand("C 3 4");
            bucketFillCommandHandler = new FillConnectedAreaCommandHandler();
            emptyCanvasString = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', ' ', ' ', ' ', '|' }, { '|', ' ', ' ', ' ', '|' }, { '|', ' ', ' ', ' ', '|' }, { '|', ' ', ' ', ' ', '|' }, { '-', '-', '-', '-', '-' } });

        }

        [TestCase("B 2 2 o")]
        public void TestFillCompleteCanvasWhenEmpty(string Command)
        {
            bucketFillCommandHandler.ProcessCommand(Command);
            var filledCanvasString = MatrixToStringConverter.Convert(new char[6, 5]{ { '-', '-', '-', '-', '-' }, { '|', 'o', 'o', 'o', '|' }, { '|', 'o', 'o', 'o', '|' }, { '|', 'o', 'o', 'o', '|' }, { '|', 'o', 'o', 'o', '|' }, { '-', '-', '-', '-', '-' } });
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + filledCanvasString);
        }

        [TestCase("B 2 2 M")]
        public void TestFillCompleteCanvasWhenEmptyAndColorChanged(string Command)
        {
            bucketFillCommandHandler.ProcessCommand(Command);
            var filledCanvasString = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', 'M', 'M', 'M', '|' }, { '|', 'M', 'M', 'M', '|' }, { '|', 'M', 'M', 'M', '|' }, { '|', 'M', 'M', 'M', '|' }, { '-', '-', '-', '-', '-' }, });
            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + filledCanvasString);
        }

        [TestCase("B 8 8 o")]
        [TestCase("B -1 8 o")]
        [TestCase("B 1 -8 o")]
        public void TestWhenFilledOutsideCanvas (string Command)
        {
            Assert.Throws<Exception>(() => bucketFillCommandHandler.ProcessCommand(Command));
            
        }


        [TestCase("R 1 1 3 3", "B 3 2 M")]
        [TestCase("R 1 1 3 3", "B 1 1 o")]
        public void TestWhenPointIsOnExistingRectangle(string rectangleCommand,string fillCommand)
        {
            var rectCommandHandler = new DrawRectangleCommandHandler();
            rectCommandHandler.ProcessCommand(rectangleCommand);
            var canvasStringWithRectangle = MatrixToStringConverter.Convert(new char[6, 5] { { '-', '-', '-', '-', '-' }, { '|', 'x', 'x', 'x', '|' }, { '|', 'x', ' ', 'x', '|' }, { '|', 'x', 'x', 'x', '|' }, { '|', ' ', ' ', ' ', '|' }, { '-', '-', '-', '-', '-' }, });

            Assert.AreEqual(stringWriter.ToString(), emptyCanvasString + canvasStringWithRectangle);
            Assert.Throws<Exception>(() => bucketFillCommandHandler.ProcessCommand(fillCommand));

        }

    }
}

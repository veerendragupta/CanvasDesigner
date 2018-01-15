using System;
using NUnit.Framework;
using CanvasDesigner;
using System.IO;
using CanvasDesigner.Exceptions;

namespace CanvasDesignerUnitTest
{
    [TestFixture]
    public class CreateCanvasCommandHandlerTest
    {
        private CreateCanvasCommandHandler createCanvasCommandHandler;
        //private StringWriter stringWriter;

        [SetUp]
        public void CreateCanvasCommandHandlerTestSetup()
        {
             createCanvasCommandHandler = new CreateCanvasCommandHandler();
        }
        
        [TestCase("C -1 0")]
        [TestCase("C 0 -5")]
        public void TestNegativeDimentionsInput(string input)
        {
            NUnit.Framework.Assert.Throws<InvalidSizeException>(()=>createCanvasCommandHandler.ProcessCommand(input));
        }
        
        [TestCase("C 0 0")]
        public void TestZeroDimentionsInput(string input)
        {
            NUnit.Framework.Assert.Throws<InvalidSizeException>(() => createCanvasCommandHandler.ProcessCommand(input));
        }

        [TestCase("C 1000,000,000 1")]
        public void TestLargeWidthInput(string input)
        {
            NUnit.Framework.Assert.Throws<InvalidSizeException>(() => createCanvasCommandHandler.ProcessCommand(input));
        }

        [TestCase("C 1 100000000")]
        public void TestLargeheightInput(string input)
        {
            NUnit.Framework.Assert.Throws<InvalidSizeException>(() => createCanvasCommandHandler.ProcessCommand(input));
        }

        [TestCase("C 10000000 10000")]
        public void TestLargeAreaInput(string input)
        {
            NUnit.Framework.Assert.Throws<InvalidSizeException>(() => createCanvasCommandHandler.ProcessCommand(input));
        }


        [TestCase("C 1 1")]
        public void TestOneByOneDimentionsInput(string input)
        {
            using (StringWriter sw = new StringWriter())
            {
             //   var originalOutput = Console.Out;
                Console.SetOut(sw);
                createCanvasCommandHandler.ProcessCommand(input);
                var outputMatrixString = MatrixToStringConverter.Convert(new char[3, 3] { {'-','-','-'},{ '|', ' ', '|' },{ '-', '-', '-' } });
                NUnit.Framework.Assert.AreEqual(outputMatrixString, sw.ToString());
            //    Console.SetOut(originalOutput);
            //    Console.WriteLine("Expected Output Matrix"+"/r/n" + outputMatrixString.ToString());
             //   Console.WriteLine("Original output was" + "/r/n"+ sw.ToString());
            }

        }

        [Test]
        public void TestNByMDimentionsInput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                createCanvasCommandHandler.ProcessCommand("C 3 2");
                var outputMatrixString = MatrixToStringConverter.Convert(new char[4,5] { { '-', '-', '-','-','-' },  { '|', ' ', ' ', ' ', '|' }, { '|', ' ', ' ', ' ', '|' },  { '-', '-', '-', '-', '-' }, });
                NUnit.Framework.Assert.AreEqual(outputMatrixString, sw.ToString());
            }
        }


    }
}

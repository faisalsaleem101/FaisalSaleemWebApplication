using DocumentGenerator.PowerPoint;
using System;
using Xunit;

namespace UnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var TableGeneratorPP = new TableGeneratorPP();
            TableGeneratorPP.Run();
        }
    }
}

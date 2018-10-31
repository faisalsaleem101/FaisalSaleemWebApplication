using DocumentGenerator.PowerPoint;
using FaisalLearningProjectMVC.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTestProject.DocumentGenerator.PowerPoint
{
    public class PowerPointTest
    {
        private readonly ContextDb _context;

        public PowerPointTest(ContextDb context)
        {
            _context = context;
        }

        [Fact]
        public void CreateTable()
        {
            var tableGenerator = new TableGeneratorPowerPoint();
            //tableGenerator.Run();
        }
    }
}

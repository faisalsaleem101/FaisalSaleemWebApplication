using DocumentGenerator.PowerPoint;
using FaisalLearningProjectMVC.Data;
using Xunit;

namespace UnitTestDocumentGenerator.DocumentGenerator.PowerPoint
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

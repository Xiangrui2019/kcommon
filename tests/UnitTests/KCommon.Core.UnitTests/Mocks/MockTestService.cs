namespace KCommon.Core.UnitTests.Mocks
{
    public class MockTestService
    {
        public int Number { get; set; } = 1;
        
        public bool Test()
        {
            return true;
        }

        public int TestNumber()
        {
            Number = Number + 1;
            return Number;
        }
    }
}
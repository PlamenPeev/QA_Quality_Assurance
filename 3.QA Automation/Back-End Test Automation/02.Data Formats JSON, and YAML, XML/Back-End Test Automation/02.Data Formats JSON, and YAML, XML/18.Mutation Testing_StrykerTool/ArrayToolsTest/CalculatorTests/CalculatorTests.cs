using Calculator;

namespace CalculatorTests
{
    public class CalculatorTests

    {

        public CalculatorEngine calculatorEngine = new CalculatorEngine();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(5, 5, 10)]
        public void TestSumFunction(int first, int second, int expected)
        {
            var result = calculatorEngine.Sum(first, second);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(5, 5, 0)]
        public void TestSubstractFunction(int first, int second, int expected)
        {
            var result = calculatorEngine.Substract(first, second);
            Assert.AreEqual(expected, result);
        }


        [Test]
        [TestCase(1, 1, 1)]
        [TestCase(5, 2, 10)]
        public void TestMultiplyFunction(int first, int second, int expected)
        {
            var result = calculatorEngine.Multiply(first, second);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(1, 1, 1)]
        [TestCase(4, 2, 2)]
        public void TestDivideFunction(int first, int second, int expected)
        {
            var result = calculatorEngine.Divide(first, second);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(1, 0)]
        public void Divide_ByZero_ShouldThrowDivideByZeroException(int first, int second)
        {
            Assert.Throws<DivideByZeroException>(() => calculatorEngine.Divide(first, second));
        }


        [Test]
        [TestCase(0, 1, 0)]
        public void Test_IfFirstIsZero(int first, int second, int expected)
        {
           var result =  calculatorEngine.Divide(first, second);
            Assert.AreEqual(expected, result);
        }

       


    }
}
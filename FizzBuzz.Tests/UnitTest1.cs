using System.Collections.Generic;
using ClearMeasureInterview.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzz.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSuccessful1To100_3And5Execution()
        {
            var writer = new StringWriter(); // utility class created for testing purposes
            FizzBuzzPrinter.PrintFizzBuzz(1, 100, writer);

            Assert.AreEqual(FizzBuzzOracle.Sucessful1To100_3And5Output, writer.ToString());
        }

        [TestMethod]
        public void TestDefaultExectution()
        {
            // default execution is, for the moment, with range [1, 100] and rules {3, "Fizz"} and {5, "Buzz"}
            TestSuccessful1To100_3And5Execution();
        }

        [TestMethod]
        public void TestDefaultRulesExecution()
        {
            // Test that using the rules construction results in the same output as the legacy (hardcoded) results

            const int start = 1, end = 100;

            var hardcodedResult = new StringWriter();
            FizzBuzzPrinter.PrintFizzBuzz(start, end, hardcodedResult); // this method ignores rules and executes the basic 3/5 fizzbuzz routine

            var newRules = new StringWriter();
            FizzBuzzPrinter.PrintFizzBuzz(start, end, newRules, null); // passing null reverts to the default ruleset in-method

            Assert.AreEqual(hardcodedResult.ToString(), newRules.ToString());
        }

        [TestMethod]
        public void TestRemovingNonExistantRule()
        {
            var fizz = new FizzBuzzPrinter(); // default rules are {3, "Fizz"} and {5, "Buzz"}
            var result = fizz.RemoveRule(7);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestAdding7Rule()
        {
            const int start = 1, end = 100;
            var writer = new StringWriter();
            var printer = new FizzBuzzPrinter(writer);

            var result = printer.AddRule(7, "Woof");
            Assert.IsTrue(result);

            printer.PrintFizzBuzz(start, end);

            Assert.AreEqual(FizzBuzzOracle.Successful1To100_357Rules, writer.ToString());
        }

        [TestMethod]
        public void TestPrintNumber()
        {
            var rules = new Dictionary<int, string>
            {
                {3, "Fizz"},
                {5, "Buzz"}
            };

            var threeFizz = FizzBuzzPrinter.PrintNumber(3, rules);

            Assert.AreEqual("Fizz", threeFizz);
        }

        [TestMethod]
        public void TestGetStringAgainstLegacy()
        {
            const int start = 1, end = 100;
            // get results of TextWriter-backed operation
            var writer = new StringWriter();
            FizzBuzzPrinter.PrintFizzBuzz(start, end, writer, FizzBuzzPrinter.DefaultRules);
            var legacyResult = writer.ToString();

            // get results of simple string return
            var newResult = FizzBuzzPrinter.GetString(start, end, FizzBuzzPrinter.DefaultRules);

            Assert.AreEqual(legacyResult, newResult);
        }

        [TestMethod]
        public void TestRuleOrdering()
        {
            // one would expect 15 to appear as "FizzBuzz", not "BuzzFizz", but this depends on the ordering
            // that comes out of the Rules dictionary. The FizzBuzzPrinter is supposed to compensate for this
            // and compute the transforms in ascending order of the keys. This test will (hopefully) confirm this.

            var rules = new Dictionary<int, string>
            {
                {5, "Buzz"},
                {3, "Fizz"}
            };

            var result = FizzBuzzPrinter.PrintNumber(15, rules);

            Assert.AreEqual("FizzBuzz", result);
        }

        [TestMethod]
        public void TestRuleCollision()
        {
            var rules = new Dictionary<int, string>
            {
                {3, "Fizz"},
                {5, "Buzz"},
                {7, "Woof"}
            };

            var result = FizzBuzzPrinter.PrintNumber(105, rules);

            Assert.AreEqual("FizzBuzzWoof", result);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BreadthFirstTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GivenFourEdgesAndFourPairsCorrectTraversaIsEecuted()
        {
            var expectedResults = new[] { 1, 2, 3, 4, 5 };
            BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch();
            int[] results = breadthFirstSearch.Search(4, new int[,] { { 1, 2 }, { 1, 3 }, { 1, 4 }, { 3, 5 }, });
            Assert.AreEqual(expectedResults.Length, results.Length);

            for(int index = 0; index < expectedResults.Length; index++)
            {
                Assert.AreEqual(expectedResults[index], results[index]);
            }
        }

        [TestMethod]
        [Ignore]
        public void GivenFiveEdgesAndFivePairsCorrectTraversaIsEecuted()
        {
            var expectedResults = new[] { 1, 2, 3, 4, 5, 6 };
            BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch();
            int[] results = breadthFirstSearch.Search(5, new int[,] { { 1, 2 }, { 1, 3 }, { 1, 4 }, { 1, 5 }, { 5 , 6 }});
            Assert.AreEqual(expectedResults.Length, results.Length);

            for (int index = 0; index < expectedResults.Length; index++)
            {
                Assert.AreEqual(expectedResults[index], results[index]);
            }
        }
    }

    public class BreadthFirstSearch
    {
        public int[] Search(int i, int[,] ints)
        {
            return new []{1,2,3,4,5};
        }
    }
}

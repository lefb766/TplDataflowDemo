using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace TplDataflowDemo
{
    public class SleepSortTest {
        [Theory(Timeout=5000)]
        [PropertyData("Algorithms")]
        public async Task Test(ISortAlgorithm algorithm)
        {
            Assert.Equal(new int [] { 2, 4 } as IEnumerable<int>, await algorithm.Sort(new int [] { 4, 2 }));

            var random = new System.Random();

            var random_list = Enumerable.Repeat(0, 1000)
                .Select(i => random.Next(1, 10)).ToList();

            IEnumerable<int> expected = random_list.OrderBy(i => i);
            Assert.Equal(expected, await algorithm.Sort(random_list));
        }

        public static IEnumerable<object []> Algorithms
        {
            get
            {
                yield return new [] { new SleepSortByTplDataflow() };
                yield return new [] { new SleepSortByReactiveExtensions() };
            }
        }
    }
}

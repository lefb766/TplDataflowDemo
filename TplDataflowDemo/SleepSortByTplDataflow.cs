using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TplDataflowDemo
{
    public class SleepSortByTplDataflow : ISortAlgorithm
    {
        public async Task<IList<int>> Sort(IList<int> source)
        {
            var sortedBuffer = new BufferBlock<int>();

            source.AsParallel().ForAll(async value =>
                {
                    await Task.Delay(value * 100);

                    // BufferBlock.Post seems to be thread safe
                    sortedBuffer.Post(value);
                });

            var sortedList = new List<int>();

            foreach (var i in Enumerable.Range(0, source.Count))
            {
                sortedList.Add(await sortedBuffer.ReceiveAsync());
            }

            return sortedList;
        }

        public override string ToString()
        {
            return "ByTplDataflow";
        }
    }
}

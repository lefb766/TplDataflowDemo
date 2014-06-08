using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace TplDataflowDemo
{
    public class SleepSortByReactiveExtensions : ISortAlgorithm
    {
        public async Task<IList<int>> Sort(IList<int> source)
        {
            var sortedObservable = Observable.Empty<int>();

            foreach (var value in source)
            {
                var subject = new Subject<int>();

                sortedObservable = sortedObservable.Merge(subject);

                Task.Run(async () =>
                    {
                        await Task.Delay(value * 100);

                        subject.OnNext(value);
                        subject.OnCompleted();
                    });
            }

            return sortedObservable.ToList().Wait();
        }

        public override string ToString()
        {
            return "ByReactiveExtensions";
        }
    }
}

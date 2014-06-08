using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TplDataflowDemo
{
    public interface ISortAlgorithm
    {
        Task<IList<int>> Sort(IList<int> source);
    }
}

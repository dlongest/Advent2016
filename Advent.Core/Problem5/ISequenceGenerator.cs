using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem5
{
    public interface ISequenceGenerator<T>
    {
        IEnumerable<T> Generate();
    }
}

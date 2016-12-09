using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem8
{
    public interface IPixelViewerCommand
    {
        void Update(IPixelViewer viewer);
    }
}

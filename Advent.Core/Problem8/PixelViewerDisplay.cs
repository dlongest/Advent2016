using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem8
{
    public class PixelViewerDisplay
    {
        private readonly IPixelViewer viewer;
        private readonly Action<string> output;

        public PixelViewerDisplay(IPixelViewer viewer, Action<string> output)
        {
            this.viewer = viewer;
            this.output = output;
        }

        public void Refresh()
        {
            foreach (var row in this.viewer.Rows)
            {
                row.Select(a => a.ToString()).ToList().ForEach(a => output(a));
            }
        }

    }
}

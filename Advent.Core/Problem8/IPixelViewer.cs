using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem8
{
    public interface IPixelViewer
    {
        void Update(Pixel pixel);

        void Update(Pixel[] pixels);

        IEnumerable<Pixel[]> Rows { get; }

        IEnumerable<Pixel[]> Columns { get; }

        Pixel At(int x, int y);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem8
{
    public class Screen : IPixelViewer
    {
        private int columns;
        private int rows;
        
        private HashSet<Pixel> screen = new HashSet<Pixel>(); 

        public Screen()
            :this(50, 6)
        {
        }

        public Screen(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;          

            foreach (var row in Enumerable.Range(0, this.rows))
            {
                foreach (var column in Enumerable.Range(0, this.columns))
                {
                    this.screen.Add(new Pixel(column, row, false));
                }
            }
        }

        public void Initialize()
        {
            foreach (var pixel in screen)
            {
                pixel.TurnOff();
            }
        }

        public IEnumerable<Pixel> Row(int rowIndex)
        {
            return this.screen.Where(a => a.Coordinate.Y == rowIndex);
        }

        public IEnumerable<Pixel> Column(int columnIndex)
        {
            return this.screen.Where(a => a.Coordinate.X == columnIndex);
        }

        public int RowLength { get { return this.rows; } }

        public int ColumnLength { get { return this.columns; } }

        public void Update(Pixel updatedPixel)
        {
            if (IsOutsideViewer(updatedPixel))
                return;

            var currentPixel = this.screen.Single(a => a.CoordinateEquals(updatedPixel));
            this.screen.Remove(currentPixel);
            this.screen.Add(updatedPixel);
        }

        public Pixel At(int x, int y)
        {
            return this.screen.Single(a => a.Coordinate.X == x && a.Coordinate.Y == y);
        }

        public void Update(Pixel[] updatedPixels)
        {
            foreach (var updatedPixel in updatedPixels)
            {
                Update(updatedPixel);
            }
        }

        private bool IsOutsideViewer(Pixel pixel)
        {
            return pixel.Coordinate.X >= this.ColumnLength || pixel.Coordinate.Y >= this.RowLength
                     || pixel.Coordinate.X < 0 || pixel.Coordinate.Y < 0;
        }
        
        public IEnumerable<Pixel[]> Rows
        {
            get
            {
                return ByDimension(p => p.Coordinate.Y, p => p.Coordinate.X);
            }
        }

        public IEnumerable<Pixel[]> Columns
        {
            get
            {             
                return ByDimension(p => p.Coordinate.X, p => p.Coordinate.Y);
            }
        }

        private IEnumerable<Pixel[]> ByDimension(Func<Pixel, int> primaryGrouping, Func<Pixel, int> secondaryOrdering)
        {
            var grouped = this.screen.GroupBy(g => primaryGrouping(g));

            var inPrimaryOrder = grouped.OrderBy(g => g.Key);

            var orderBySecondary = inPrimaryOrder.Select(g => g.OrderBy(sp => secondaryOrdering(sp)).ToArray());

            return orderBySecondary;
        }

    }    
}

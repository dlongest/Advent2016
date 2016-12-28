using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem11
{
    public class RadiationFacility
    {
        private IDictionary<int, HashSet<FacilityElement>> floors;
        private int elevatorFloor;

        public RadiationFacility()
        {
            this.floors = new Dictionary<int, HashSet<FacilityElement>>()
            {
                { 1, new HashSet<FacilityElement>() }, { 2, new HashSet<FacilityElement>() },
                { 3, new HashSet<FacilityElement>() }, { 4, new HashSet<FacilityElement>() }
            };

            this.elevatorFloor = 1;
        }

        public void Add(IEnumerable<FacilityElement> elements, int floor)
        {
            foreach (var element in elements)
            {
                this.floors[floor].Add(element);
            }
        }

        public int FindShortestPath()
        {
            int moves = 0;
            return 0;
            while (!IsComplete())
            {
                

            }   
        }


        private IEnumerable<PossibleMove> GetPossibleMoves()
        {
            var elementsOnFloor = this.floors[this.elevatorFloor];

            return null;

        }



        private bool IsComplete()
        {
            return !this.floors[1].Any() && !this.floors[2].Any() && !this.floors[3].Any();
        }
    }

    public class PossibleMove
    {
        public int ToFloor { get; set; }

        public FacilityElement Element { get; set; }
    }

    public class Isotope
    {
        public static Isotope Hydrogen = new Isotope("H", "Hydrogen");
        public static Isotope Strontium = new Isotope("S", "Strontium");
        public static Isotope Lithium = new Isotope("L", "Lithium");
        public static Isotope Thulium = new Isotope("T", "Thulim");
        public static Isotope Ruthenium = new Isotope("R", "Ruthenium");
        public static Isotope Curium = new Isotope("C", "Curium");
        public static Isotope Plutonium = new Isotope("P", "Plutonium");

        private Isotope(string abbreviation, string name)
        {
            this.Abbreviation = abbreviation;
            this.Name = name;
        }

        public string Abbreviation { get; private set; }

        public string Name { get; private set; }
    }

    public enum ElementType
    {
        Generator, Chip
    };

    public class FacilityElement
    {
        public ElementType Type { get; private set; }

        public Isotope Isotope { get; private set; }

        public bool IsSafeNear(FacilityElement element)
        {
            if (element.Type == this.Type)
                return true;

            return element.Isotope.Equals(this.Isotope);
        }

        public bool IsSafeNear(IEnumerable<FacilityElement> elements)
        {
            return elements.All(e => this.IsSafeNear(e));
        }
    } 
}

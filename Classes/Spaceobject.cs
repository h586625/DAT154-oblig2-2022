using System;
namespace SpaceSim
{
    public class SpaceObject
    {
        public String Name { get; set; }
        public double OrbitalRadius { get; set; }
        public double OrbitalPeriod { get; set; }
        public double ObjectRadius { get; set; }
        public double RotationalPeriod { get; set; }
        public string ObjectColor { get; set; }
        public SpaceObject? Parent;
        public List<SpaceObject> Children = new();
        protected bool Orbits = true;

        public SpaceObject(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor)
        {
            this.Name = name;
            this.OrbitalRadius = orbitalRadius;
            this.OrbitalPeriod = orbitalPeriod;
            this.ObjectRadius = objectRadius;
            this.RotationalPeriod = rotationalPeriod;
            this.ObjectColor = objectColor;
        }

        public void SetParent(SpaceObject parent)
        {
            this.Parent = parent;
        }

        public void SetChild(SpaceObject child)
        {
            this.Children.Add(child);
        }

        public virtual void Draw()
        {
            Console.WriteLine(Name);

            if (Parent != null)
            {
                Console.Write("Orbital radius: \t\t\t" + OrbitalRadius + "*10^6 km ");
                Console.WriteLine("around the " + Parent.Name);
                Console.WriteLine("Orbital period: \t\t\t" + OrbitalPeriod + " days");
                Console.WriteLine("Rotation period: \t\t\t" + RotationalPeriod + " days");
            }

            Console.WriteLine("Object radius: \t\t\t\t" + ObjectRadius + " km");
        }

        public virtual void WriteRelativePosition(SpaceObject obj, int time)
        {
            var temp = obj.CalculatePosition(time);
            double x = temp.Item1;
            double y = temp.Item2;

            Console.WriteLine(obj.Name + "'s position relative to the sun after " + time + " days in x and y direction: \n" + x + " km*10^6" + " and " + y + " km*10^6");
            Console.WriteLine();
        }

        public virtual (double, double) CalculatePosition(int time)
        {
            double x, y;

            if (Orbits && OrbitalRadius != 0)
            {
                double rad = 2 * Math.PI * (time / OrbitalPeriod);
                x = OrbitalRadius * Math.Cos(rad);
                y = OrbitalRadius * Math.Sin(rad);

                if (Parent != null)
                {
                    (double, double) parentPos = Parent.CalculatePosition(time);
                    x += parentPos.Item1;
                    y += parentPos.Item2;
                }
            }
            else
            {
                x = 0;
                y = 0;
            }

            return (x, y);
        }
    }
    public class Star : SpaceObject
    {
        public Star(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor)
        {
            Orbits = false;
        }
        public override void Draw()
        {
            Console.Write("Star: \t\t\t\t\t");
            base.Draw();
        }
    }
    public class Planet : SpaceObject
    {
        public Planet(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw()
        {
            Console.Write("Planet: \t\t\t\t");
            base.Draw();
        }
    }
    public class Moon : SpaceObject
    {
        public Moon(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw()
        {
            Console.Write("Moon: \t\t\t\t\t");
            base.Draw();
        }
    }
    // Task 2
    public class Comet : SpaceObject
    {
        public Comet(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw()
        {
            Console.Write("Comet: \t\t\t\t\t");
            base.Draw();
        }
    }
    public class Asteroid : SpaceObject
    {
        public Asteroid(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw()
        {
            Console.Write("Asteroid: \t\t\t\t\t");
            base.Draw();
        }
    }
    // asteroids, asteroid belts, and dwarf
    public class AsteroidBelt : Asteroid
    {
        public AsteroidBelt(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw()
        {
            Console.Write("AsteroidBelt: \t\t\t\t");
            base.Draw();
        }
    }
    public class DwarfStar : Star
    {
        public DwarfStar(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw()
        {
            Console.Write("Dwarf star: \t\t\t\t");
            base.Draw();
        }
    }
}
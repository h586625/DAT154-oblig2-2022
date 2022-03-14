using System;
namespace SpaceSim
{
    public class SpaceObject
    {
        protected String name;
        protected double orbitalRadius;
        protected double orbitalPeriod;
        protected double objectRadius;
        protected double rotationalPeriod;
        protected string objectColor;
        public SpaceObject(
            String name, 
            double orbitalRadius, 
            double orbitalPeriod,
            double objectRadius,
            double rotationalPeriod,
            string objectColor
        ) {
            this.name = name;
            this.orbitalRadius = orbitalRadius;
            this.orbitalPeriod = orbitalPeriod;
            this.objectRadius = objectRadius;
            this.rotationalPeriod = rotationalPeriod;
            this.objectColor = objectColor;
        }
        public virtual void Draw() {
            Console.WriteLine(name);
        }
    }
    public class Star : SpaceObject
    {
        public Star(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw() {
            Console.Write("Star: ");
            base.Draw();
        }
    }
    public class Planet : SpaceObject {
        public Planet(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw() {
            Console.Write("Planet: ");
            base.Draw();
        }
    }
    public class Moon : SpaceObject
    {
        public Moon(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw() {
            Console.Write("Moon: ");
            base.Draw();
        }
    }
    // Task 2
    public class Comet : SpaceObject
    {
        public Comet(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw()
        {
            Console.Write("Comet: ");
            base.Draw();
        }
    }
    public class Asteroid : SpaceObject
    {
        public Asteroid(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw()
        {
            Console.Write("Asteroid: ");
            base.Draw();
        }
    }
    // asteroids, asteroid belts, and dwarf
    public class AsteroidBelt : Asteroid
    {
        public AsteroidBelt(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw()
        {
            Console.Write("AsteroidBelt: ");
            base.Draw();
        }
    }
    public class DwarfStar : Star
    {
        public DwarfStar(String name, double orbitalRadius, double orbitalPeriod, double objectRadius, double rotationalPeriod, string objectColor) : base(name, orbitalRadius, orbitalPeriod, objectRadius, rotationalPeriod, objectColor) { }
        public override void Draw()
        {
            Console.Write("Dwarf star: ");
            base.Draw();
        }
    }
}
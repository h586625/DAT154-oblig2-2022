using System;
using System.Collections.Generic;
using SpaceSim;
class Astronomy
{
    public static void Main() {
        List<SpaceObject> solarSystem = new();

        Star Sun = new("Sun", 0, 0, 69635, 0, "yellow");

        Planet Earth = new("Earth", 149.6, 365.26, 6371, 1, "blue");
        Planet Mars = new("Mars", 227.9, 686.98, 3402, 1.025, "red");
        Planet Mercury = new("Mercury", 2439.7, 57.9, 87.96, 58.7, "dark_grey");
        Planet Venus = new("Venus", 6051.8, 108.2, 224.68, 243, "yellow");

        Moon TheMoon = new("The Moon", .384, 27.322, 1737.1, 27, "grey");
        Moon Phobos = new("Phobos", .000009, .3189, 11.25, .3, "grey");


        foreach (SpaceObject obj in solarSystem) {
            obj.Draw();
        }

        Mercury.SetParent(Sun);
        Venus.SetParent(Sun);
        Earth.SetParent(Sun);
        Mars.SetParent(Sun);

        TheMoon.SetParent(Earth);
        Phobos.SetParent(Mars);

        Earth.SetChild(TheMoon);
        Mars.SetChild(Phobos);

        solarSystem.Add(Sun);
        solarSystem.Add(Mercury);
        solarSystem.Add(Venus);
        solarSystem.Add(Earth);
        solarSystem.Add(Mars);

        solarSystem.Add(TheMoon);
        solarSystem.Add(Phobos);

        Console.WriteLine("Pick a number of days.");
        String strTime = Console.ReadLine();
        int time;
        if (!string.IsNullOrEmpty(strTime))
        {
            time = Convert.ToInt32(strTime);
        } else {
            time = 10;
            Console.WriteLine("You didn't pick a number so we defaulted to 10 days.");
        }

        bool foundPlanet = false;

        Console.WriteLine("Write the name of one of these planets: Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptun.");
        String answer = Console.ReadLine();

        foreach (SpaceObject obj in solarSystem) {
            if (obj.Name.Equals(answer)) {
                foundPlanet = true;
                obj.Draw();

                if (obj.Parent != null) {
                    obj.WriteRelativePosition(obj, time);
                }

                if (obj.Children.Count > 0) {
                    Console.WriteLine("Moons:");
                    Console.WriteLine();
                    
                    foreach (SpaceObject child in obj.Children) {
                        child.Draw();

                        child.WriteRelativePosition(child, time);
                    }
                }
            }

        }
        if (!foundPlanet) {
            Console.WriteLine("You didn't choose a valid planet.");
            Console.WriteLine();
            solarSystem[0].Draw();
            Console.WriteLine();

            foreach (SpaceObject planet in solarSystem) {
                if (planet is Planet) {
                    planet.Draw();
                    Console.WriteLine();
                }
            }
        }

        Console.ReadLine();
    }
}
using System;
using System.Collections.Generic;
using SpaceSim;
class Astronomy
{
    public static void Main() {
        List<SpaceObject> solarSystem = new List<SpaceObject>
        {
            //new Star("Sun"),
            new Planet("Mercury", 2439.7, 57.9, 87.96, 58.7, "dark grey"),
            new Planet("Venus", 6051.8, 108.2, 224.68, 243, "yellow"),
            new Planet("Terra", 6371, 149.6, 365.26, 1, "blue")
            //new Moon("The Moon"),
            //new Comet("The Comet"),
            //new Asteroid("The Asteroid"),
            //new AsteroidBelt("The Asteroid belt"),
            //new DwarfStar("The Dwarf star")
        };
        
        foreach (SpaceObject obj in solarSystem) {
            obj.Draw();
        }
        
        Console.ReadLine();
    }
}
using System;
using System.Collections.Generic;

public static class StartingPatternHelper
{
    private static List<StartingPattern> oscillatorStartingPatterns = new List<StartingPattern>(){
        /* Oscillators */

        // Blinker Horizontal
        new StartingPattern(new List<(int x, int y)>()
        {
            (x: 0, y: 0),
            (x: 1, y: 0),
            (x: 2, y: 0)
        }),
        // Blinker Vertical
        new StartingPattern(new List<(int x, int y)>()
        {
            (x: 0, y: 0),
            (x: 0, y: 1),
            (x: 0, y: 2)
        }),
        // Toad
        new StartingPattern(new List<(int x, int y)>()
        {
            (x: 0, y: 0),
            (x: 1, y: 0),
            (x: 2, y: 0),
            (x: 1, y: 1),
            (x: 2, y: 1),
            (x: 3, y: 1)
        }),
        // Beacon
        new StartingPattern(new List<(int x, int y)>()
        {
            (x: 2, y: 0),
            (x: 3, y: 0),
            (x: 3, y: 1),
            (x: 0, y: 2),
            (x: 0, y: 3),
            (x: 1, y: 3)
        }),
    };

    public static StartingPattern GetStartingPattern()
    {
        Random random = new Random();
        return oscillatorStartingPatterns[random.Next(0, oscillatorStartingPatterns.Count)];
    }
}
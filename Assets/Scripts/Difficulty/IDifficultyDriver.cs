using System.Collections.Generic;

// Represent the difficulty as a list of weighted Unity objects
// generated as a function of the level
public interface IDifficultyDriver
{
    List<WeightedObject> Difficulty(int level);
}

using UnityEngine;

[CreateAssetMenu(fileName = "MapLayout", menuName = "MapLayout")]
public class MapLayout : ScriptableObject
{
    [Tooltip("S - Start, E - End, # - Obstacle, . - Empty")]
    public string[] layout =
    {
        "S...#",
        "#....",
        "...#.",
        ".....",
        ".#..E"
    };
}

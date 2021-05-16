using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldParameters", menuName = "ScriptableObjects/WorldParameters", order = 1)]
public class WorldParameters : ScriptableObject
{
    // Amount of objects per tile
    public int AmountOfThings = 10;
    // Amount of tiles simulthaniously
    public int AmmTilesOnScreen = 2;
    // Initial zone without object's
    public float SafeZone = 10f;
}

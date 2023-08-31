using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapLocation
{
    public Vector2 coordinates = new Vector2(0,0);
    public GameObject buildingPref;

    public MapLocation(Vector2 c, GameObject b)
    {
        coordinates = c;
        buildingPref = b;
    }
}

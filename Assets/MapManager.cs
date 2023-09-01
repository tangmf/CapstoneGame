using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Data")]
    public List<MapLocation> mapLocations;

    public List<MapLocation> addedMapLocations;
    public List<MapLocation> defaultMapLocations;

    public Transform defaultLocationsContainer;
    public Transform addedLocationsContainer;

    [Header("Shortcut Keys")]
    public KeyCode update;
    public KeyCode addObject;

    // Debugging purposes
    public GameObject testPref;
    // Start is called before the first frame update
    void Start()
    {
        UpdateMapLocations();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(update))
        {
            UpdateMapLocations();
        }

        if (Input.GetKeyDown(addObject))
        {
            addedMapLocations.Add(new MapLocation(new Vector2(10, 10), testPref));
        }
    }

    public void UpdateMapLocations()
    {
        // Remove logic
        mapLocations.Clear();
        defaultMapLocations.Clear();
        // Set up defaults (only add logically)
        foreach (Transform t in defaultLocationsContainer)
        {
            MapLocation newMapLocation = new MapLocation(t.position, t.gameObject);
            defaultMapLocations.Add(newMapLocation);
            mapLocations.Add(newMapLocation);
        }

        // Remove Display + Logic
        foreach (Transform t in addedLocationsContainer)
        {
            Destroy(t.gameObject);
        }
        // Add added map locations (add logically to general list + display)
        foreach (MapLocation ml in addedMapLocations)
        {
            mapLocations.Add(ml);
            GameObject newLocation = Instantiate(ml.buildingPref, new Vector3(ml.coordinates.x, ml.coordinates.y, 0), Quaternion.identity);
            newLocation.transform.parent = addedLocationsContainer;
        }
    }


}

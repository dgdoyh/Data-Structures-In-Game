using System;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    private Graph mapGraph;
    private string currLocation;

    public event Action<string> OnCurrLocationUpdated;

    public Map()
    {
        mapGraph = new Graph();
    }

    private void Start()
    {
        AddLocation("A");
        AddLocation("B");
        AddLocation("C");
        AddLocation("D");
        AddLocation("E");

        ConnectLocations("A", "B");
        ConnectLocations("A", "C");
        ConnectLocations("B", "D");
        ConnectLocations("C", "D");
        ConnectLocations("C", "E");

        SetCurrLocation("A");
    }

    public void AddLocation(string location)
    {
        mapGraph.AddVertex(location);
    }

    public void ConnectLocations(string location1, string location2)
    {
        mapGraph.AddEdge(location1, location2);
    }

    public List<string> GetConnectedLocations(string location)
    {
        return mapGraph.GetNeighbors(location);
    }

    public void SetCurrLocation(string location)
    {
        currLocation = location;
        OnCurrLocationUpdated?.Invoke(currLocation);
    }

    public void Travel(string locationToTravel)
    {
        if (locationToTravel == currLocation)
        {
            Debug.Log("It's the current location.");
            return;
        }

        List<string> possibleTravelLocationList = GetConnectedLocations(currLocation);

        if (possibleTravelLocationList.Contains(locationToTravel))
        {
            Debug.Log("Travel to " + locationToTravel);
            SetCurrLocation(locationToTravel);
        }
        else
        {
            Debug.Log("It's not connected. Cannot travel.");
        }
    }
}

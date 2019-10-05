using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{ 
// Create array that holds all TurningPoints
public Transform[,] gBPoints = new Transform[27, 30];

// References the empty that contains all the points
private GameObject turningPoints;

// Use this for initialization
void Start()
{

    // Get the Empty named TurningPoints
    turningPoints = GameObject.Find("TurningPoints");

    // Cycle through each point in that empty
    foreach (Transform point in turningPoints.transform)
    {

        // Get vector position of point
        Vector2 pos = point.position;

        // Put point in the array
        gBPoints[(int)pos.x, (int)pos.y] = point;

    }
}
	
 
}

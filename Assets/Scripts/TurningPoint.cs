using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningPoint : MonoBehaviour
{
    public TurningPoint[] nextPoints;
    public Vector2[] vectToNextPoint;

    // Use this for initialization
    void Start()
    {

        // Initialize the array that will hold the vector to
        // each nearby TurningPoint
        vectToNextPoint = new Vector2[nextPoints.Length];

        for (int i = 0; i < nextPoints.Length; i++)
        {

            // Get each point so we can find its position
            // in relation to the current TurningPoint
            TurningPoint nextPoint = nextPoints[i];

            // Get the Vector to the next TurningPoint
            // Returns (1, 0) for right, (0, -1) for down, etc.
            Vector2 pointVect = nextPoint.transform.localPosition - transform.localPosition;

            // Store vector to Vector2 array
            // Without normalized the values wouldn't be 0, 1, or -1
            vectToNextPoint[i] = pointVect.normalized;
        }

    }
}

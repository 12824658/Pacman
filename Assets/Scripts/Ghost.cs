using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ghost : MonoBehaviour
{

	public float speed = 4f;

	// Used to move the Ghost
	private Rigidbody2D rb;

	// Animation sprites
	public Sprite lookLeftSprite;
	public Sprite lookRightSprite;
	public Sprite lookUpSprite;
	public Sprite lookDownSprite;

	// An array of destinations the Ghosts will move toward
	// while on patrol
	Vector2[] destinations = new Vector2[]
    {
		new Vector2( 1, 29 ),
		new Vector2( 26, 29 ),
		new Vector2( 26, 1 ),
		new Vector2( 1, 1 ),
		new Vector2( 6, 16 )
	};

	// The index to the first destination the Ghost aims at
	// Each Ghost aims at a different one
	public int destinationIndex;
	// Direction Ghost will move when it hits a Point
	Vector2 moveVect;

	// Used to change the sprite
	public SpriteRenderer sr;

	// Make this public so Ms. Pac-Man can get its value
	public bool isGhostBlue = false;

	// Stores blue version of ghost
	public Sprite blueGhost;
	// Seconds to wait before starting to chase at the beginning
	// of the game
	// Red: 2, Pink: 5, Blue: 10, Orange: 15 
	public float startWaitTime = 0;

	// Seconds to wait after Ghost is eaten
	public float waitTimeAfterEaten = 4.0f;
	// All Ys 15.5 RedX: 11.5, 12.5, 13.5, 14.5
	public float cellXPos = 0;
	public float cellYPos = 0;


	// Add Rigidbody to Ghosts
	void Awake()
    {
		// Get Ghost Rigidbody
		rb = GetComponent<Rigidbody2D> ();

		// Get the SpriteRenderer
		sr = gameObject.GetComponent<SpriteRenderer>();

	}

	void Start()
    {
	
		Invoke ("StartMoving", startWaitTime);
	}

    //Called when it is time to move Ghost
    void StartMoving()
    {

        // Move Ghost from cell to starting position
        transform.position = new Vector2(13.5f, 18.5f);

        // X of the destination TurningPoint
        float xDest = destinations[destinationIndex].x;

        // If Ghost x pos > destination x
        if (transform.position.x > xDest)
        {
            // Move the Ghost left
            rb.velocity = new Vector2(-1, 0) * speed;
        }
        else
        {
            // Move the Ghost right
            rb.velocity = new Vector2(1, 0) * speed;
        }
    }

	GameObject pacmanGO = null;

	//  After Ghost is eaten pause and then reset
	public void ResetGhostAfterEaten(GameObject pacman){

		// Move Ghosts to their cell position
		transform.position = new Vector2(cellXPos,cellYPos);

		// Stop the Ghost
		rb.velocity = Vector2.zero;

		pacmanGO = pacman;

		// Starts moving Ghost after defined seconds
		Invoke ("StartMovingAfterEaten", waitTimeAfterEaten);
	}

	// Called to move the Ghost
	void StartMovingAfterEaten()
    {

		// Move Ghost from cell to starting position
		transform.position = new Vector2 (13.5f, 18.5f);

		// X of the destination TurningPoint
		float xDest = destinations[destinationIndex].x;

		// If Ghost x pos > destination x
		if(transform.position.x > xDest)
        {

			// Move the Ghost left
			rb.velocity = new Vector2 (-1, 0) * speed;
		} else
        {
			// Move the Ghost right
			rb.velocity = new Vector2 (1, 0) * speed;
		}

	}

	// End of 2

	void OnTriggerEnter2D(Collider2D col)
    {

		// If the Ghost hit a Point
		if (col.gameObject.tag == "Point")
        {

			// Get the Vector Ghost wants to move towards
			moveVect = GetNewDirection(col.transform.position);

			// Position the Ghost at point on screen
			transform.position = new Vector2 ((int)col.transform.position.x + .5f, 
				(int)col.transform.position.y + .5f);

			// Change the sprites when turning
			if (moveVect.x != 2)
            {

				if (moveVect == Vector2.right)
                {

					// Changes the direction of the Ghost
					rb.velocity = moveVect * speed;

					// Change the sprite
					// Change if Sprite isn't blue
					if (!isGhostBlue)
                    {
						sr.sprite = lookRightSprite;
					}

				} else if (moveVect == Vector2.left)
                {

					// Changes the direction of the Ghost
					rb.velocity = moveVect * speed;

					// Change the sprite
					// Change if Sprite isn't blue

					if (!isGhostBlue) {
						sr.sprite = lookLeftSprite;
					}

				} else if (moveVect == Vector2.up)
                {

					// Changes the direction of the Ghost
					rb.velocity = moveVect * speed;

					// Change the sprite
					// Change if Sprite isn't blue

					if (!isGhostBlue) {
						sr.sprite = lookUpSprite;
					}

				} else if (moveVect == Vector2.down)
                {

					// Changes the direction of the Ghost
					rb.velocity = moveVect * speed;

					// Change the sprite
					// Change if Sprite isn't blue

					if (!isGhostBlue) {
						sr.sprite = lookDownSprite;
					}

				}
			}

		}

		// Simulates going through the portal
		Vector2 pmMoveVect = new Vector2(0,0);

		if (transform.position.x < 2 && transform.position.y == 15.5) {
			transform.position = new Vector2 (24.5f, 15.5f);
			pmMoveVect = new Vector2(-1,0);
			rb.velocity = pmMoveVect * speed;
		} else if (transform.position.x > 25 && transform.position.y == 15.5) {
			transform.position = new Vector2 (2f, 15.5f);
			pmMoveVect = new Vector2(1,0);
			rb.velocity = pmMoveVect * speed;
		}
			
	}

	Vector2 GetNewDirection(Vector2 pointVect){

		// Ghost position minus the additional .5 for X & Y
		float xPos = (float)Math.Floor(Convert.ToDouble(transform.position.x));
		float yPos = (float)Math.Floor(Convert.ToDouble(transform.position.y));

		// Pivot point position minus the additional .5 for X & Y
		pointVect.x = (float)Math.Floor(Convert.ToDouble(pointVect.x));
		pointVect.y = (float)Math.Floor(Convert.ToDouble(pointVect.y));

		// Get the destination
		Vector2 dest = destinations[destinationIndex];

		// 5. If I know where Pac-Man is go there
		if (pacmanGO != null) {
			dest = pacmanGO.transform.position;
		}

		// Checks to see if the Ghost hits the destination
		if(((pointVect.x + 1) == dest.x) && ((pointVect.y + 1) == dest.y)){
			destinationIndex = (destinationIndex == 4) ? 0 : 			
				destinationIndex + 1; 

		}

		dest = destinations[destinationIndex];

		// 5. If I know where Pac-Man is go there
		if (pacmanGO != null) {
			dest = pacmanGO.transform.position;
		}

		// Will hold the new direction Ms. Pac-Man will move to
		Vector2 newDir = new Vector2(2,0);

		// Holds previous direction traveled
		Vector2 prevDir = rb.velocity.normalized;

		// Holds opposite of previous direction traveled
		Vector2 oppPrevDir = prevDir * -1;

		// Vector2 directions
		Vector2 goRight = new Vector2(1,0);
		Vector2 goLeft = new Vector2(-1,0);
		Vector2 goUp = new Vector2(0,1);
		Vector2 goDown = new Vector2(0,-1);

		// Distance from destinations is used to decide if I
		// should move based off of which X or Y is closest
		float destXDist = dest.x - xPos;
		float destYDist = dest.y - yPos;

		// Upper Left

		// Keeps Ghost from going toward the portal
		if (destYDist > 0 && destXDist < 0)
        {

			if (pointVect.x == 5 && pointVect.y == 15)
            {

				if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir)
                {
					newDir = goUp;
				}

				// Pick Up or Left depending whether I'm closest to
				// the X or Y
			} else if (destYDist > destXDist)
            {

				if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir)
                {
					newDir = goLeft;
				} else if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir)
                {
					newDir = goUp;
				} else if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir)
                {
					newDir = goRight;
				} else if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir)
                {
					newDir = goDown;
				} else if (canIMoveInDirection (oppPrevDir, pointVect))
                {
					newDir = oppPrevDir;
				}


			}
            else if (destYDist < destXDist)
            {

				if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir)
                {
					newDir = goUp;
				} else if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir)
                {
					newDir = goLeft;
				} else if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir)
                {
					newDir = goRight;
				} else if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir)
                {
					newDir = goDown;
				} else if (canIMoveInDirection (oppPrevDir, pointVect))
                {
					newDir = oppPrevDir;
				}

			}

		}

		// Upper Right

		if (destYDist > 0 && destXDist > 0){

			if (destYDist > destXDist) {

				if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir) {
					newDir = goRight;
				} else if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir) {
					newDir = goUp;
				} else if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir) {
					newDir = goLeft;
				} else if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir) {
					newDir = goDown;
				} else if (canIMoveInDirection (oppPrevDir, pointVect)) {
					newDir = oppPrevDir;
				}

			} else if (destYDist < destXDist) {

				if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir) {
					newDir = goUp;
				} else if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir) {
					newDir = goRight;
				} else if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir) {
					newDir = goLeft;
				} else if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir) {
					newDir = goDown;
				} else if (canIMoveInDirection (oppPrevDir, pointVect)) {
					newDir = oppPrevDir;
				}

			}
				
		}

		// Lower Right

		if (destYDist < 0 && destXDist > 0){

			if (destYDist > destXDist) {

				if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir) {
					newDir = goRight;
				} else if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir) {
					newDir = goDown;
				} else if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir) {
					newDir = goLeft;
				} else if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir) {
					newDir = goUp;
				} else if (canIMoveInDirection (oppPrevDir, pointVect)) {
					newDir = oppPrevDir;
				}

			} else if (destYDist < destXDist) {

				if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir) {
					newDir = goDown;
				} else if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir) {
					newDir = goRight;
				} else if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir) {
					newDir = goLeft;
				} else if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir) {
					newDir = goUp;
				} else if (canIMoveInDirection (oppPrevDir, pointVect)) {
					newDir = oppPrevDir;
				} else if (canIMoveInDirection (oppPrevDir, pointVect)) {
					newDir = oppPrevDir;
				}

			}

		}

		// Lower Left

		if (destYDist < 0 && destXDist < 0)
        {

			if (destYDist > destXDist) {

				if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir)
                {
					newDir = goLeft;
				} else if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir)
                {
					newDir = goDown;
				} else if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir)
                {
					newDir = goRight;
				} else if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir)
                {
					newDir = goUp;
				} else if (canIMoveInDirection (oppPrevDir, pointVect))
                {
					newDir = oppPrevDir;
				}

			} else if (destYDist < destXDist) {

				if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir)
                {
					newDir = goDown;
				} else if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir)
                {
					newDir = goLeft;
				} else if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir)
                {
					newDir = goRight;
				} else if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir)
                {
					newDir = goUp;
				} else if (canIMoveInDirection (oppPrevDir, pointVect))
                {
					newDir = oppPrevDir;
				}

			}

		}
			
		// Ys Equal and Want to go Right
		// Done because the above don't test for if Xs & Ys are equal

		if ((int)(dest.y) == (int)(yPos)
			&& destXDist > 0){

			Debug.Log ("5");

			if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir)
            {
				newDir = goRight;
			} else if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir)
            {
				newDir = goUp;
			} else if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir)
            {
				newDir = goDown;
			} else if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir)
            {
				newDir = goLeft;
			}

		}

		// Ys Equal and Want to go Left

		if ((int)(dest.y) == (int)(yPos)
			&& destXDist < 0){

			Debug.Log ("6");

			if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir)
            {
				newDir = goLeft;
			} else if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir)
            {
				newDir = goUp;
			} else if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir)
            {
				newDir = goDown;
			} else if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir)
            {
				newDir = goRight;
			}

		}

		// Xs Equal and Want to go Up

		if ((int)(dest.x) == (int)(xPos)
			&& destYDist > 0) {


			Debug.Log ("7");

			if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir)
            {
				newDir = goUp;
			} else if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir)
            {
				newDir = goRight;
			} else if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir)
            {
				newDir = goLeft;
			} else if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir)
            {
				newDir = goDown;
			}
		}
			

		// Xs Equal and Want to go Down

		if ((int)(dest.x) == (int)(xPos)
			&& destYDist < 0) {

			Debug.Log ("8");

			if (canIMoveInDirection (goDown, pointVect) && goDown != oppPrevDir) {
				newDir = goDown;
			} else if (canIMoveInDirection (goRight, pointVect) && goRight != oppPrevDir) {
				newDir = goRight;
			} else if (canIMoveInDirection (goLeft, pointVect) && goLeft != oppPrevDir) {
				newDir = goLeft;
			} else if (canIMoveInDirection (goUp, pointVect) && goUp != oppPrevDir) {
				newDir = goUp;
			}

		}
		return newDir;
	}

	// Gets a chosen direction and searches for it in the array
	// that holds references to all the pivot points
	bool canIMoveInDirection(Vector2 dir, Vector2 pointVect)
    {

		// Ghost position
		Vector2 pos = transform.position;

		// Used to find if there a Point in the array or null
		Transform point = GameObject.Find ("GBGrid").GetComponent<Gameboard> ().gBPoints [(int)pointVect.x, (int)pointVect.y];

		// Did I find a Point here?
		if (point != null)
        {

			// Get Points associated GameObject
			GameObject pointGO = point.gameObject;

			// Get vectToNextPoint array attached to the Point
			Vector2[] vectToNextPoint = pointGO.GetComponent<TurningPoint> ().vectToNextPoint;

			Debug.Log ("Checking Vects " + dir);

			// Cycle through the attached vectToNextPoint array
			foreach (Vector2 vect in vectToNextPoint)
            {

				Debug.Log ("Check vector: " + vect);

				if (vect == dir)
                {
					return true;
				} 
			}
		} 
		return false;
	}

	// Calls for the Ghost to be turned blue
	public void TurnGhostBlue()
    {

		StartCoroutine (TurnGhostBlueAndBack ());

	}

	IEnumerator TurnGhostBlueAndBack()
    {

		// Set so that the Ghost isn't animated while blue
		isGhostBlue = true;

		// Change to the blueGhost (SET IN INSPECTOR)
		sr.sprite = blueGhost;

		// Wait 6 seconds before changing back
		yield return new WaitForSeconds( 6.0f );

		// Allow for the Ghost to be animated again
		isGhostBlue = false;

	}

}

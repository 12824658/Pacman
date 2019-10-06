using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MsPacman : MonoBehaviour
{

    public float speed = 0.4f;

    // Used to move Ms. Pacman
    private Rigidbody2D rb;

    // 5. Sprite used when Pac-Man is paused
    public Sprite pausedSprite;

    // Makes sure components have been created when the 
    // game starts
    void Awake()
    {
        // Get Pac-man Rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = new Vector2(-1, 0) * speed;
    }

    /* 1. 
	void FixedUpdate(){
 
		float horzMove = Input.GetAxisRaw ("Horizontal");
		float vertMove = Input.GetAxisRaw ("Vertical");
 
		// Move Left
		if (Input.GetKeyDown ("a")) {
 
			// Changes the direction to left
			rb.velocity = new Vector2 (horzMove, 0) * speed;
 
			// Faces Pacman left
			transform.localScale = new Vector2 (1, 1);
 
			// Sets rotation to default
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		} else if (Input.GetKeyDown ("d")) {
 
			// Changes the direction to right
			rb.velocity = new Vector2 (horzMove, 0) * speed;
 
			// Faces Pacman right
			transform.localScale = new Vector2 (-1, 1);
 
			// Sets rotation to default
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		} else if (Input.GetKeyDown ("w")){
 
			// Move up
			rb.velocity = new Vector2 (0, vertMove) * speed;
 
			// Sets facing direction to default
			transform.localScale = new Vector2 (1, 1);
 
			// Rotate facing up
			transform.localRotation = Quaternion.Euler (0, 0, 270);
		} else if (Input.GetKeyDown ("s")){
 
			// Move Down
			rb.velocity = new Vector2 (0, vertMove) * speed;
 
			// Sets facing direction to default
			transform.localScale = new Vector2 (1, 1);
 
			// Rotate facing down
			transform.localRotation = Quaternion.Euler (0, 0, 90);
		}
	}
	*/

    /*
	// 2. All direction changes using Turning Points
	void FixedUpdate(){
 
		float horzMove = Input.GetAxisRaw ("Horizontal");
		float vertMove = Input.GetAxisRaw ("Vertical");
 
		Vector2 moveVect;
 
		// Move Left
		if (Input.GetKeyDown ("a")) {
 
			// Direction I want to move
			moveVect = new Vector2 (horzMove, 0);
 
			if(canIMoveInDirection(moveVect)){
 
				// 3. Position Pac-Man at middle of the lane
				transform.position = new Vector2 ((int)transform.position.x + .5f, 
					(int)transform.position.y + .5f);
 
				// Changes the direction to left
				rb.velocity = moveVect * speed;
 
				// Faces Pacman left
				transform.localScale = new Vector2 (1, 1);
 
				// Sets rotation to default
				transform.localRotation = Quaternion.Euler (0, 0, 0);
			}
 
 
		} else if (Input.GetKeyDown ("d")) {
 
			// Direction I want to move
			moveVect = new Vector2 (horzMove, 0);
 
			if (canIMoveInDirection (moveVect)) {
 
				// 3. Position Pac-Man at middle of the lane
				transform.position = new Vector2 ((int)transform.position.x + .5f, 
					(int)transform.position.y + .5f);
 
				// Changes the direction to right
				rb.velocity = moveVect * speed;
 
				// Faces Pacman right
				transform.localScale = new Vector2 (-1, 1);
 
				// Sets rotation to default
				transform.localRotation = Quaternion.Euler (0, 0, 0);
 
			}
 
 
		} else if (Input.GetKeyDown ("w")){
 
			// Direction I want to move
			moveVect = new Vector2 (0, vertMove);
 
			if (canIMoveInDirection (moveVect)) {
 
				// 3. Position Pac-Man at middle of the lane
				transform.position = new Vector2 ((int)transform.position.x + .5f, 
					(int)transform.position.y + .5f);
 
				// Move up
				rb.velocity = moveVect * speed;
 
				// Sets facing direction to default
				transform.localScale = new Vector2 (1, 1);
 
				// Rotate facing up
				transform.localRotation = Quaternion.Euler (0, 0, 270);
 
			}
 
 
		} else if (Input.GetKeyDown ("s")){
 
			// Direction I want to move
			moveVect = new Vector2 (0, vertMove);
 
			if (canIMoveInDirection (moveVect)) {
 
				// 3. Position Pac-Man at middle of the lane
				transform.position = new Vector2 ((int)transform.position.x + .5f, 
					(int)transform.position.y + .5f);
 
				// Move Down
				rb.velocity = moveVect * speed;
 
				// Sets facing direction to default
				transform.localScale = new Vector2 (1, 1);
 
				// Rotate facing down
				transform.localRotation = Quaternion.Euler (0, 0, 90);
 
			}
		}
 
		// 5. Will pause and unpause Pac-Man animation 
		// depending on if Pac-Man is at a wall or not
		UpdateEatingAnimation();
 
	}
	*/

    // 6. Add the ability to go in reverse without a pivot point
    void FixedUpdate()
    {

        float horzMove = Input.GetAxisRaw("Horizontal");
        float vertMove = Input.GetAxisRaw("Vertical");

        Vector2 moveVect;

        // 6. Used to get direction Pac-Man is moving on
        // the X & Y access
        var localVelocity = transform.InverseTransformDirection(rb.velocity);

        // Move Left
        if (Input.GetKeyDown("a"))
        {

            // 6. Allows me to reverse direction without the need
            // to hit a point

            if (localVelocity.x > 0)
            {

                // Direction I want to move
                moveVect = new Vector2(horzMove, 0);

                // Position Pac-Man at middle of the lane
                transform.position = new Vector2((int)transform.position.x + .5f,
                    (int)transform.position.y + .5f);

                // Changes the direction to left
                rb.velocity = moveVect * speed;

                // Faces Pacman left
                transform.localScale = new Vector2(1, 1);

                // Sets rotation to default
                transform.localRotation = Quaternion.Euler(0, 0, 0);

            }
            else
            {

                // Direction I want to move
                moveVect = new Vector2(horzMove, 0);

                if (canIMoveInDirection(moveVect))
                {

                    // 3. Position Pac-Man at middle of the lane
                    transform.position = new Vector2((int)transform.position.x + .5f,
                        (int)transform.position.y + .5f);

                    // Changes the direction to left
                    rb.velocity = moveVect * speed;

                    // Faces Pacman left
                    transform.localScale = new Vector2(1, 1);

                    // Sets rotation to default
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }


        }
        else if (Input.GetKeyDown("d"))
        {

            if (localVelocity.x < 0)
            {

                // Direction I want to move
                moveVect = new Vector2(horzMove, 0);

                // Position Pac-Man at middle of the lane
                transform.position = new Vector2((int)transform.position.x + .5f,
                    (int)transform.position.y + .5f);

                // Changes the direction to left
                rb.velocity = moveVect * speed;

                // Faces Pacman left
                transform.localScale = new Vector2(-1, 1);

                // Sets rotation to default
                transform.localRotation = Quaternion.Euler(0, 0, 0);

            }
            else
            {

                // Direction I want to move
                moveVect = new Vector2(horzMove, 0);

                if (canIMoveInDirection(moveVect))
                {

                    // 3. Position Pac-Man at middle of the lane
                    transform.position = new Vector2((int)transform.position.x + .5f,
                        (int)transform.position.y + .5f);

                    // Changes the direction to right
                    rb.velocity = moveVect * speed;

                    // Faces Pacman right
                    transform.localScale = new Vector2(-1, 1);

                    // Sets rotation to default
                    transform.localRotation = Quaternion.Euler(0, 0, 0);

                }
            }

        }
        else if (Input.GetKeyDown("w"))
        {

            if (localVelocity.y > 0)
            {

                // Direction I want to move
                moveVect = new Vector2(0, vertMove);

                // Position Pac-Man at middle of the lane
                transform.position = new Vector2((int)transform.position.x + .5f,
                    (int)transform.position.y + .5f);

                // Changes the direction to left
                rb.velocity = moveVect * speed;

                // Faces Pacman left
                transform.localScale = new Vector2(1, 1);

                // Sets rotation to default
                transform.localRotation = Quaternion.Euler(0, 0, 270);

            }
            else
            {

                // Direction I want to move
                moveVect = new Vector2(0, vertMove);

                if (canIMoveInDirection(moveVect))
                {

                    // 3. Position Pac-Man at middle of the lane
                    transform.position = new Vector2((int)transform.position.x + .5f,
                        (int)transform.position.y + .5f);

                    // Move up
                    rb.velocity = moveVect * speed;

                    // Sets facing direction to default
                    transform.localScale = new Vector2(1, 1);

                    // Rotate facing up
                    transform.localRotation = Quaternion.Euler(0, 0, 270);

                }
            }


        }
        else if (Input.GetKeyDown("s"))
        {

            if (localVelocity.y < 0)
            {

                // Direction I want to move
                moveVect = new Vector2(0, vertMove);

                // Position Pac-Man at middle of the lane
                transform.position = new Vector2((int)transform.position.x + .5f,
                    (int)transform.position.y + .5f);

                // Changes the direction to left
                rb.velocity = moveVect * speed;

                // Faces Pacman left
                transform.localScale = new Vector2(1, 1);

                // Sets rotation to default
                transform.localRotation = Quaternion.Euler(0, 0, 90);

            }
            else
            {

                // Direction I want to move
                moveVect = new Vector2(0, vertMove);

                if (canIMoveInDirection(moveVect))
                {

                    // 3. Position Pac-Man at middle of the lane
                    transform.position = new Vector2((int)transform.position.x + .5f,
                        (int)transform.position.y + .5f);

                    // Move Down
                    rb.velocity = moveVect * speed;

                    // Sets facing direction to default
                    transform.localScale = new Vector2(1, 1);

                    // Rotate facing down
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                }
            }
        }

        // 5. Will pause and unpause Pac-Man animation 
        // depending on if Pac-Man is at a wall or not
        UpdateEatingAnimation();

    }


    // 2. Find out if Pac-man is on a TurningPoint
    bool canIMoveInDirection(Vector2 dir)
    {

        // Pac-Man position
        Vector2 pos = transform.position;

        // Used to find if there a Point in the array or null
        Transform point = GameObject.Find("GBGrid").GetComponent<Gameboard>().gBPoints[(int)pos.x, (int)pos.y];

        // Did I find a Point here?
        if (point != null)
        {

            // Get Points associated GameObject
            GameObject pointGO = point.gameObject;

            // Get vectToNextPoint array attached to the Point
            Vector2[] vectToNextPoint = pointGO.GetComponent<TurningPoint>().vectToNextPoint;

            // Cycle through the attached vectToNextPoint array
            foreach (Vector2 vect in vectToNextPoint)
            {
                if (vect == dir)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // 4. Check if Pac-Man hit a wall
    // Check Is trigger for Pac-Man Circle Collider
    // Change all Dots to Dot Tag and Pills to Pill Tag
    // Add Is Trigger to Points with Circle Collider Radius .1
    // Add Tag Point to all Points
    void OnTriggerEnter2D(Collider2D col)
    {

        bool hitAWall = false;

        // If Pac-Man hit a Point
        if (col.gameObject.tag == "TurnPoint")
        {

            // Get vectToNextPoint array attached to the Point
            Vector2[] vectToNextPoint = col.GetComponent<TurningPoint>().vectToNextPoint;

            // Cycle through the attached vectToNextPoint array
            // to see if there is a Vector2 == Pac-Mans velocity 
            // or the direction Pac-Man wants to travel
            if (Array.Exists(vectToNextPoint, element => element == rb.velocity.normalized))
            {
                hitAWall = false;
            }
            else
            {
                hitAWall = true;
            }

            // 5. Needed to make Pac-Man stop on the Point when it 
            // hits a wall
            transform.position = new Vector2((int)col.transform.position.x + .5f,
                (int)col.transform.position.y + .5f);

            // Stops Pac-Man when it hits a wall
            if (hitAWall)
                rb.velocity = Vector2.zero;

        }

    }

    // 5. When Pac-Man hits a wall the animation will stop
    void UpdateEatingAnimation()
    {
        if (rb.velocity == Vector2.zero)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = pausedSprite;
        }
        else
        {
            GetComponent<Animator>().enabled = true;
        }
    }
}
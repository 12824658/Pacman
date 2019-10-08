﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to find out if Pac-Man hits a wall
using System;

// Used to manipulate the UI
using UnityEngine.UI;

public class MsPacman : MonoBehaviour {

	public float speed = 0.4f;

	// Used to move Ms. Pacman
	private Rigidbody2D rb;

	// Sprite used when Pac-Man is paused
	public Sprite pausedSprite;

	// Get reference to the SoundManager for functions
	SoundManager soundManager;

	// Audio clips
	public AudioClip eatingGhost;
	public AudioClip pacmanDies;
	public AudioClip powerupEating;

	// Setup Canvas
	// Create UI -> Text -> Name it Score
	// Canvas -> PosX 12.5, PosY 14.5, Width 40, Height 36
	// Render Mode World Space
	// Score -> PosX .56, PosY 16, Width 400, Height 30, 
	// Scale .07, Bold, White, 22, Align Center

	// Add Circle Colliders to all Dots with .25 Radius
	// Check Is Trigger

	// Used to access the function IsValidSpace in Gameboard.cs
	Gameboard gameBoard;

	// Get the RedGhost script so I can call functions
	Ghost redGhostScript;

	// 4. Add other Ghost Scripts
	Ghost pinkGhostScript;
	Ghost blueGhostScript;
	Ghost orangeGhostScript;

	// Makes sure components have been created when the 
	// game starts
	void Awake(){
		// Get Pac-man Rigidbody
		rb = GetComponent<Rigidbody2D> ();

		// Setup Gameboard so I can access functions
		gameBoard = FindObjectOfType(typeof(Gameboard)) as Gameboard; 

		// Get the RedGhost GameObject
		GameObject redGhostGO = GameObject.Find ("RedGhost");

		// 4. Get other Ghost GameObjects
		GameObject pinkGhostGO = GameObject.Find ("PinkGhost");
		GameObject blueGhostGO = GameObject.Find ("BlueGhost");
		GameObject orangeGhostGO = GameObject.Find ("OrangeGhost");

		// Get the Script attached to the RedGhost
		redGhostScript = (Ghost) redGhostGO.GetComponent(typeof(Ghost));

		// 4. Get other Ghost Scripts
		pinkGhostScript = (Ghost) pinkGhostGO.GetComponent(typeof(Ghost));
		blueGhostScript = (Ghost) blueGhostGO.GetComponent(typeof(Ghost));
		orangeGhostScript = (Ghost) orangeGhostGO.GetComponent(typeof(Ghost));
	}

	void Start(){
		rb.velocity = new Vector2 (-1, 0) * speed;

		// Get SoundManager reference
		soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
	}

	// Add the ability to go in reverse without a pivot point
	void FixedUpdate(){

		float horzMove = Input.GetAxisRaw ("Horizontal");
		float vertMove = Input.GetAxisRaw ("Vertical");

		Vector2 moveVect;

		// Used to get direction Pac-Man is moving on
		// the X & Y access
		var localVelocity = transform.InverseTransformDirection(rb.velocity);

		// Move Left
		if (Input.GetKeyDown ("a")) {

			// Allows me to reverse direction without the need
			// to hit a point

			// Change to check if this is a valid space
			// Subtracting 1 from x because I'm trying to move left
			if (localVelocity.x > 0 && gameBoard.IsValidSpace(transform.position.x - 1,transform.position.y)) {

				// Direction I want to move
				moveVect = new Vector2 (horzMove, 0);

				// Position Pac-Man at middle of the lane
				transform.position = new Vector2 ((int)transform.position.x + .5f, 
					(int)transform.position.y + .5f);

				// Changes the direction to left
				rb.velocity = moveVect * speed;

				// Faces Pacman left
				transform.localScale = new Vector2 (1, 1);

				// Sets rotation to default
				transform.localRotation = Quaternion.Euler (0, 0, 0);

			} else {

				// Direction I want to move
				moveVect = new Vector2 (horzMove, 0);

				if (canIMoveInDirection (moveVect)) {

					// Position Pac-Man at middle of the lane
					transform.position = new Vector2 ((int)transform.position.x + .5f, 
						(int)transform.position.y + .5f);

					// Changes the direction to left
					rb.velocity = moveVect * speed;

					// Faces Pacman left
					transform.localScale = new Vector2 (1, 1);

					// Sets rotation to default
					transform.localRotation = Quaternion.Euler (0, 0, 0);
				}
			}


		} else if (Input.GetKeyDown ("d")) {

			// Change to check if this is a valid space
			// Adding 1 to x because I'm trying to move right
			if (localVelocity.x < 0 && gameBoard.IsValidSpace(transform.position.x + 1,transform.position.y)) {

				// Direction I want to move
				moveVect = new Vector2 (horzMove, 0);

				// Position Pac-Man at middle of the lane
				transform.position = new Vector2 ((int)transform.position.x + .5f, 
					(int)transform.position.y + .5f);

				// Changes the direction to left
				rb.velocity = moveVect * speed;

				// Faces Pacman left
				transform.localScale = new Vector2 (-1, 1);

				// Sets rotation to default
				transform.localRotation = Quaternion.Euler (0, 0, 0);

			} else {

				// Direction I want to move
				moveVect = new Vector2 (horzMove, 0);

				if (canIMoveInDirection (moveVect)) {

					// Position Pac-Man at middle of the lane
					transform.position = new Vector2 ((int)transform.position.x + .5f, 
						(int)transform.position.y + .5f);

					// Changes the direction to right
					rb.velocity = moveVect * speed;

					// Faces Pacman right
					transform.localScale = new Vector2 (-1, 1);

					// Sets rotation to default
					transform.localRotation = Quaternion.Euler (0, 0, 0);

				}
			}
				
		} else if (Input.GetKeyDown ("w")){

			// Change to check if this is a valid space
			// Adding 1 to y because I'm trying to move up
			if (localVelocity.y > 0 && gameBoard.IsValidSpace(transform.position.x,transform.position.y + 1)) {

				// Direction I want to move
				moveVect = new Vector2 (0, vertMove);

				// Position Pac-Man at middle of the lane
				transform.position = new Vector2 ((int)transform.position.x + .5f, 
					(int)transform.position.y + .5f);

				// Changes the direction to left
				rb.velocity = moveVect * speed;

				// Faces Pacman left
				transform.localScale = new Vector2 (1, 1);

				// Sets rotation to default
				transform.localRotation = Quaternion.Euler (0, 0, 270);

			} else {

				// Direction I want to move
				moveVect = new Vector2 (0, vertMove);

				if (canIMoveInDirection (moveVect)) {

					// Position Pac-Man at middle of the lane
					transform.position = new Vector2 ((int)transform.position.x + .5f, 
						(int)transform.position.y + .5f);

					// Move up
					rb.velocity = moveVect * speed;

					// Sets facing direction to default
					transform.localScale = new Vector2 (1, 1);

					// Rotate facing up
					transform.localRotation = Quaternion.Euler (0, 0, 270);

				}
			}

		} else if (Input.GetKeyDown ("s")){

			// Change to check if this is a valid space
			// Subtracting 1 from y because I'm trying to move down
			if (localVelocity.y < 0 && gameBoard.IsValidSpace(transform.position.x,transform.position.y - 1)) {

				// Direction I want to move
				moveVect = new Vector2 (0, vertMove);

				// Position Pac-Man at middle of the lane
				transform.position = new Vector2 ((int)transform.position.x + .5f, 
					(int)transform.position.y + .5f);

				// Changes the direction to left
				rb.velocity = moveVect * speed;

				// Faces Pacman left
				transform.localScale = new Vector2 (1, 1);

				// Sets rotation to default
				transform.localRotation = Quaternion.Euler (0, 0, 90);

			} else {

			// Direction I want to move
			moveVect = new Vector2 (0, vertMove);

				if (canIMoveInDirection (moveVect)) {

					// Position Pac-Man at middle of the lane
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
		}

		// Will pause and unpause Pac-Man animation 
		// depending on if Pac-Man is at a wall or not
		UpdateEatingAnimation();

		if (transform.position.y == 2.5) {
			transform.position = new Vector2 (transform.position.x, 3.5f);
		}

	}

	// Find out if Pac-man is on a TurningPoint
	bool canIMoveInDirection(Vector2 dir){

		// Pac-Man position
		Vector2 pos = transform.position;

		// Used to find if there a Point in the array or null
		Transform point = GameObject.Find ("GBGrid").GetComponent<Gameboard> ().gBPoints [(int)pos.x, (int)pos.y];

		// Did I find a Point here?
		if (point != null) {

			// Get Points associated GameObject
			GameObject pointGO = point.gameObject;

			// Get vectToNextPoint array attached to the Point
			Vector2[] vectToNextPoint = pointGO.GetComponent<TurningPoint> ().vectToNextPoint;

			// Cycle through the attached vectToNextPoint array
			foreach (Vector2 vect in vectToNextPoint) {
				if (vect == dir) {
					return true;
				} 
			}
		} 
		return false;
	}

	// Check if Pac-Man hit a wall
	// Check Is trigger for Pac-Man Circle Collider
	// Change all Dots to Dot Tag and Pills to Pill Tag
	// Add Is Trigger to Points with Circle Collider Radius .1
	// Add Tag Point to all Points
	void OnTriggerEnter2D(Collider2D col){

		bool hitAWall = false;

		// If Pac-Man hit a Point
		if (col.gameObject.tag == "Point") {

			// Get vectToNextPoint array attached to the Point
			Vector2[] vectToNextPoint = col.GetComponent<TurningPoint> ().vectToNextPoint;

			// Cycle through the attached vectToNextPoint array
			// to see if there is a Vector2 == Pac-Mans velocity 
			// or the direction Pac-Man wants to travel
			if (Array.Exists (vectToNextPoint, element => element == rb.velocity.normalized)) {
				hitAWall = false;
			} else {
				hitAWall = true;
			}

			// Needed to make Pac-Man stop on the Point when it 
			// hits a wall
			transform.position = new Vector2 ((int)col.transform.position.x + .5f, 
				(int)col.transform.position.y + .5f);

			// Stops Pac-Man when it hits a wall
			if (hitAWall) 
				rb.velocity = Vector2.zero;		

		}

		// Handle eating pills
		// Add Circle Colliders + Is Trigger to all Pills
		if (col.gameObject.tag == "Pill") {

			// Play pill eating sound
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.powerupEating);

			// Calls for the Ghost to be turned blue in the Ghost script
			redGhostScript.TurnGhostBlue ();

			// 4. Turn other Ghosts blue
			pinkGhostScript.TurnGhostBlue ();
			blueGhostScript.TurnGhostBlue ();
			orangeGhostScript.TurnGhostBlue ();

			// 3. Add points earned
			IncreaseTextUIScore (50);

			// 3. Destory the pill
			Destroy(col.gameObject);

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
			
		// If Pac-Man hit a Dot
		if (col.gameObject.tag == "Dot") {
			ADotWasEaten (col);
		}

		// 4. Handle hitting a Ghost
		if (col.gameObject.tag == "Ghost") {

			// Get name for Ghost I collided with
			String ghostName = col.GetComponent<Collider2D>().gameObject.name;

			// Get the AudioSource
			AudioSource audioSource = soundManager.GetComponent<AudioSource>();

			// If the Ghosts name matches
			if (ghostName == "RedGhost") {
				if (redGhostScript.isGhostBlue) {

					// Call for the Ghost to be reset and for its destination 
					// to now be Ms. Pac-Man
					redGhostScript.ResetGhostAfterEaten (gameObject);

					// Play eating ghost sound
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.eatingGhost);

					// Increase the score
					IncreaseTextUIScore (400);
				} else {

					// Play Ms. Pac-Man dies sound
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.pacmanDies);

					// Turn off the dot eating sound
					audioSource.Stop ();

					// Destroy Ms. Pac-Man
					Destroy (gameObject);
				}
			} else if (ghostName == "PinkGhost") {
				if (pinkGhostScript.isGhostBlue) {
					pinkGhostScript.ResetGhostAfterEaten (gameObject);
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.eatingGhost);
					IncreaseTextUIScore (400);
				} else {
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.pacmanDies);
					audioSource.Stop ();
					Destroy (gameObject);
				}
			} else if (ghostName == "BlueGhost") {
				if (blueGhostScript.isGhostBlue) {
					blueGhostScript.ResetGhostAfterEaten (gameObject);
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.eatingGhost);
					IncreaseTextUIScore (400);
				} else {
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.pacmanDies);
					audioSource.Stop ();
					Destroy (gameObject);
				}
			} else if (ghostName == "OrangeGhost") {
				if (orangeGhostScript.isGhostBlue) {
					orangeGhostScript.ResetGhostAfterEaten (gameObject);
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.eatingGhost);
					IncreaseTextUIScore (400);
				} else {
					SoundManager.Instance.PlayOneShot (SoundManager.Instance.pacmanDies);
					audioSource.Stop ();
					Destroy (gameObject);
				}
			}

		}
		// END OF 4

	}

	// When Pac-Man hits a wall the animation will stop
	void UpdateEatingAnimation(){
		if (rb.velocity == Vector2.zero) {
			GetComponent<Animator> ().enabled = false;
			GetComponent<SpriteRenderer> ().sprite = pausedSprite;

			// Pause Pac-Man eating sound
			soundManager.PausePacman();

		} else {
			GetComponent<Animator> ().enabled = true;
			// Unpause Pac-Man eating sound
			soundManager.UnPausePacman();
		}
	}

	// Increase the score and delete a dot that Pac-Man eats
	void ADotWasEaten(Collider2D col){

		// Increase the score on the screen
		// 3. Add points earned
		IncreaseTextUIScore (10);

		// Destroy the Dot Pac-Man collided with
		Destroy (col.gameObject);
	}

	// Increases the score the the text UI name passed
	// 3. Add points
	void IncreaseTextUIScore(int points){

		// Find the Score UI component
		Text textUIComp = GameObject.Find("Score").GetComponent<Text>();

		// Get the string stored in it and convert to an int
		int score = int.Parse(textUIComp.text);

		// Increment the score
		// 3. Add points here
		score += points;

		// Convert the score to a string and update the UI
		textUIComp.text = score.ToString();
	}

}
	
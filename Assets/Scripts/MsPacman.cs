using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void FixedUpdate()
    {

        float horzMove = Input.GetAxisRaw("Horizontal");
        float vertMove = Input.GetAxisRaw("Vertical");

        // Move Left
        if (Input.GetKeyDown("a"))
        {

            // Changes the direction to left
            rb.velocity = new Vector2(horzMove, 0) * speed;

            // Faces Pacman left
            transform.localScale = new Vector2(1, 1);

            // Sets rotation to default
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown("d"))
        {

            // Changes the direction to right
            rb.velocity = new Vector2(horzMove, 0) * speed;

            // Faces Pacman right
            transform.localScale = new Vector2(-1, 1);

            // Sets rotation to default
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown("w"))
        {

            // Move up
            rb.velocity = new Vector2(0, vertMove) * speed;

            // Sets facing direction to default
            transform.localScale = new Vector2(1, 1);

            // Rotate facing up
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
        else if (Input.GetKeyDown("s"))
        {

            // Move Down
            rb.velocity = new Vector2(0, vertMove) * speed;

            // Sets facing direction to default
            transform.localScale = new Vector2(1, 1);

            // Rotate facing down
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
    }
}

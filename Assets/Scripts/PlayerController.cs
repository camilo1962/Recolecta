using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    bool isMoveLeft;
    bool isMoveRight;
    float horizontalMove;
    [SerializeField]
    float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        isMoveLeft = false;
        isMoveRight = false;
    }

    void Update()
    {
        MovementPlayer();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }

    // Pressing Left Arrow Button
    public void DownArrowLeft()
    {
        isMoveLeft = true;
    }

    // Not Pressing Left Arrow Button
    public void UpAroowLeft()
    {
        isMoveLeft = false;
    }

    // Same thing with the Right Arrow Button
    public void DownArrowRight()
    {
        isMoveRight = true;
    }
    public void UpAroowRight()
    {
        isMoveRight = false;
    }

    // Moving player with Arrow Buttons
    private void MovementPlayer()
    {
        if (isMoveLeft)
        {
            horizontalMove = -speed;
        }
        else if (isMoveRight)
        {
            horizontalMove = speed;
        }
        else
        {
            horizontalMove = 0;
        }

        // If both arrows are pressed player stops 
        if (isMoveRight && isMoveLeft)
        {
            horizontalMove = 0;
        }
    }

    //  Moving player with finger
    void OnMouseDrag()
    {
        // Checks if game is in pause
        if (HudMenu.instance.isPause != true)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, transform.position.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            objPosition.y = -4.08f;
            transform.position = objPosition;
        }
    }
}


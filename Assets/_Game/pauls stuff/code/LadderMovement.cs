using UnityEngine;

public class LadderMovement : MonoBehaviour
{

    private float Vertical;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D RB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        Vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(Vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            RB.gravityScale = 0f;
            RB.linearVelocity = new Vector2(RB.linearVelocity.x, Vertical * speed);
        }
        else
        {
            RB.gravityScale = 4f;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    
    {
if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;   
        }
    }
}

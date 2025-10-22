using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 0.5f;
    private float destroyDelay = 2f;

    bool isFalling;
    Rigidbody2D rb; 

   void start ()
   {
       rb = GetComponent<Rigidbody2D>();
   }
    private onCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            startCoroutine(fall()); 
        }
    }
    private IEnumerator fall()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);
        rb.bodytype = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}

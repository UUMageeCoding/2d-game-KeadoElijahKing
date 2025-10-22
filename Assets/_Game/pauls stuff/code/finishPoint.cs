using UnityEngine;

public class finishPoint : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // go to next level
        }
    }
    
}

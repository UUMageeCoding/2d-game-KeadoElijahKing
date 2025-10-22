using UnityEngine;

/*public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 0.5f;
    private float destroyDelay = 2f;

    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   private IEnumerator Fall() 
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        destroyDelay(gameObject, destroyDelay);
    }
}
*/
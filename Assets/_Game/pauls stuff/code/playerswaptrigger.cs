using UnityEngine;

public class playerswaptrigger : MonoBehaviour
{
    public GameObject newPlayerPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwapPlayer(collision.gameObject);
        }
    }
    private void SwapPlayer(GameObject currentPlayer)
    {
        if (newPlayerPrefab == null)
        {
            return;
        }
    }
}

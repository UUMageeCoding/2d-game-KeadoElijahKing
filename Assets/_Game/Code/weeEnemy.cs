using UnityEngine;

public class weeEnemy : MonoBehaviour
{
    public Transform playerPosition;
    private Vector2 directonToPlayer;
    int enemySpeed = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPosition != null)
        {
            directonToPlayer = (playerPosition.position - transform.position).normalized;
            Debug.Log(directonToPlayer);
        }
        if (playerPosition.position - transform.position != Vector3.zero) MoveToPlayer(enemySpeed);
    }
    public void MoveToPlayer(float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
    }
}

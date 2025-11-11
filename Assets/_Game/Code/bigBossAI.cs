using UnityEngine;
public enum BigBossState
{
    Idle,
Attacking 
}
public class bigBossAI : MonoBehaviour
{
    
    public Transform playerPosition;
    int hystereis = 2;
    SpriteRenderer spriteRenderer;
    public int attackDistance;
    public BigBossState currentState = BigBossState.Idle;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition.position);
        switch (currentState)
        {
            case BigBossState.Idle:
                if (distanceToPlayer < attackDistance)
                {
                    currentState = BigBossState.Attacking;
                    spriteRenderer.color = Color.red;
                }
                break;

            case BigBossState.Attacking:
                if (distanceToPlayer > attackDistance + hystereis)
                {
                    currentState = BigBossState.Idle;
                    spriteRenderer.color = Color.white;
                }
                break; 
        }
    }
}

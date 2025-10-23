using UnityEngine;

public class moveObject : MonoBehaviour
{
    public GameObject NPC;
    public GameObject endposition;

    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NPC.transform.position = Vector2.MoveTowards(NPC.transform.position, endposition.transform.position, speed);
    }
}

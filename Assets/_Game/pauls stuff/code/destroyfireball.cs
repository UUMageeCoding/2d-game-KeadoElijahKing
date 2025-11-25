using UnityEngine;

public class destroyfireball : MonoBehaviour
{
    public GameObject prefabfireBall;
    public float speed;
    public Transform startPosition; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //startPosition.position = prefabfireBall.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "fallingFire")
        {
             Debug.Log("isrespawning" + collision.name);
             collision.gameObject.transform.position = startPosition.transform.position;
             Debug.Log(collision.gameObject.transform.position); 
        }
        //Instantiate(prefabfireBall,startPosition);
    }
}

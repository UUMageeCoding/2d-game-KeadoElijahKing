using Unity.VisualScripting;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    private RespawnScript rs;

     void Awake()
    {
        rs = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnScript>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rs.respawnPoint = this.gameObject;
        }
    }
}

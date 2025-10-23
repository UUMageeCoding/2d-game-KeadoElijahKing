using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ObjectToMove : MonoBehaviour
{
    public GameObject NPC;
    public GameObject startPosition;
    public GameObject endPosition;

    public float speed = 10f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(NPC.transform.position, endPosition.transform.position, Time.deltaTime);
    }
}

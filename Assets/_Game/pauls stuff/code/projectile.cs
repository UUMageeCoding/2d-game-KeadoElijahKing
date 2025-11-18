using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;

    private bool hit;
    public float lifeTime;
    private Animator anim;

    private CapsuleCollider2D boxCollider;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if (lifeTime > 5) gameObject.SetActive(false);

          // Get horizontal input
        direction = Input.GetAxisRaw("Horizontal");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //hit = true;
        //boxCollider.enabled = false;
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            anim.SetTrigger("explode");
            Deactivate(); 
        }
    }
    public void SetDirection(float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
            localScaleX = localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false); 
    }
}

using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    public float lifeTime;
    private Animator anim;

    private CapsuleCollider2D boxCollider;
    public PlatformerController pc; 
    private bool fireBallDirection; 
    SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        hit = false;
        direction = 1;

        fireBallDirection = pc.isMovingRight;
        SetDirection(fireBallDirection);
    }
    private void Update()
    {
        // if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0) gameObject.SetActive(false);



        Debug.Log(pc.isMovingRight);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if(collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            anim.SetTrigger("explode");
            Deactivate();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("explode");
            Deactivate();
        }
    }

    public void SetFireballActive()
    {
        gameObject.SetActive(true);
    } 
    public void SetDirection(bool _direction)
    {
        
        hit = false;
        boxCollider.enabled = true;

        if (_direction)
        {
            sr.flipX = false;
            direction = 1;
        } 
        else
        {
            sr.flipX = true;   
            direction = -1; 
        }
        
    }
    private void Deactivate()
    {
        gameObject.SetActive(false); 
    }
}
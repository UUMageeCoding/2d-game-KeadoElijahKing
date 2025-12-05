using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [Header("Health")]
[SerializeField] private float startingHealth;
public float currentHealth { get; private set;}
private Animator anim;
private bool dead;

[Header("iFrames")]
[SerializeField] private float iFramesDuration; 

[SerializeField] private float numberOfFlashes; 
private SpriteRenderer spriteRend;

    [Header("Components")]
   [SerializeField] private Behaviour[] components; 

    public GameManager gameManager;

    private bool isDead;
    private bool isEnemyDead;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>(); 
        spriteRend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(currentHealth <= 0 && !isDead)
        {
            isDead = true;
            gameObject.SetActive(false);
            gameManager.gameOver();
            Debug.Log("dead");
        }
        if (currentHealth <= 0 && !isEnemyDead)
        {
            isEnemyDead = true;
            gameObject.SetActive(false);
            Debug.Log("dead");
        }
    }
    public void TakeDamage(float _damage)
    {
        currentHealth =Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
                anim.SetTrigger("hurt");
                StartCoroutine(Invunerability()); 
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                foreach (Behaviour component in components)
                    component.enabled = false;
                

                dead = true;
            }
            
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth =Mathf.Clamp(currentHealth + _value, 0, startingHealth);

    }
    public void Respawn()
    {
        AddHealth(startingHealth);
        anim.ResetTrigger("die"); 
        anim.Play("idle");
        StartCoroutine(Invunerability());
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(6,10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1,0,0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white; 
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
         Physics2D.IgnoreLayerCollision(6,10, false);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

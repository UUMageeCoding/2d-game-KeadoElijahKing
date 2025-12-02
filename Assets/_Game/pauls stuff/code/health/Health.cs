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


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>(); 
        spriteRend = GetComponent<SpriteRenderer>();
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
                //Player
                if(GetComponent<PlatformerController>() != null)
                GetComponent<PlatformerController>().enabled = false; 

                //Enemy
                if(GetComponent<enemyPatrol>() != null)
                GetComponentInParent<enemyPatrol>().enabled = false;

                if(GetComponent<meleeEnemy>() != null)
                GetComponent<meleeEnemy>().enabled = false;

                dead = true;
            }
            
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth =Mathf.Clamp(currentHealth + _value, 0, startingHealth);

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
}

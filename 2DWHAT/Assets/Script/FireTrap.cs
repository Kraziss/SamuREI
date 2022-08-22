using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private AudioClip FIRE; 

   [Header ("Firetrap Timer")]
   [SerializeField]private float activationDelay;
   [SerializeField]private float activationTime;
   private Animator anim;
   private SpriteRenderer spriteRend;




    private bool triggered;
    private bool active;

    private Health playerHealth;




   private void Awake()
   {
    anim = GetComponent<Animator>();
    spriteRend = GetComponent<SpriteRenderer>();
   }

private void Update() {
    if (playerHealth != null && active)
    {
        playerHealth.TakeDamage(damage);
    }
}


   private void OnTriggerEnter2D (Collider2D collision)
   {
    if (collision.tag == "Player")
    {
        playerHealth = collision.GetComponent<Health>();
        
        if(!triggered) 
        {
                StartCoroutine(ActivateFiretrap());
        }
        if(active)
            collision.GetComponent<Health>().TakeDamage(damage);
    }
   }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player")
        {
            playerHealth = null;
        }
    }



   private IEnumerator ActivateFiretrap()
   {
    triggered = true;
    spriteRend.color = Color.red;
    yield return new WaitForSeconds(activationDelay);
    SoundManager.instance.PlaySound(FIRE);
    spriteRend.color = Color.white;
    active = true;

    anim.SetBool("activated", true);




     yield return new WaitForSeconds(activationTime);
     active = false;
     triggered = false;

     anim.SetBool("activated", false);

   }
}

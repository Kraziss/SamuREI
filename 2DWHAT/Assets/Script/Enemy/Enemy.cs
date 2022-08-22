using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private AudioClip SworldHit;
    
    public Animator animator;


    public int maxHealth = 1;
    int currentHealth;



    void Start()
    {
        currentHealth = maxHealth;
    }

   public void TakeDagame(int damage)
   {
    currentHealth =- damage;

    animator.SetTrigger("hurt");



    if(currentHealth <=0)
    {
        Die();
    }
   }

  void Die() 
  {
    
    Debug.Log("Enemy died");

    animator.SetBool("die",true);
     SoundManager.instance.PlaySound(SworldHit);

    GetComponent<Collider2D>().enabled = false;
    this.enabled = false;
    
    


   }
}

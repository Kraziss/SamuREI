using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private AudioClip SworldHit;

    public Transform attackPoint;
    public LayerMask EnemyLayer;
   
    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public float attackRate =2f;
    float nextAttackTime = 0f;
    
    void Update()
    {
            if (Time.time > nextAttackTime)
           { 
                if (Input.GetKeyDown(KeyCode.E))     
                {
                    Attack();
                    nextAttackTime = Time.time +1f / attackRate;
                }
           }
    }


    void Attack()
    {
        SoundManager.instance.PlaySound(SworldHit);
        animator.SetTrigger("attack");


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,EnemyLayer);


        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDagame(attackDamage);
        }
    }


     void OnDrawGizmosSelected() {
        {
            if (attackPoint == null)
            return;



            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}

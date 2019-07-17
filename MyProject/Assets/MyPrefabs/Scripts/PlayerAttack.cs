using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{


    Animator animator;

    //Stats
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;
    public int Poisondamage;
    public int PoisonDamagePeriod;
    PlayerController player;

    //Change weapon sprite

    public Slider enemyHpBar;
    Vectors backstabVector;


    //Debug
    bool TakeNormalDamage = false;
    bool TakePoisonDamage = false;
    bool TakebackstabDamage = false;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        backstabVector = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Vectors>();

        Physics2D.queriesStartInColliders = false;

    }



    void AttackNormalOrPoison()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemyHpBar.gameObject.SetActive(true);
                if (player.poisonWeaponStatus && !enemiesToDamage[i].GetComponent<EnemyController>().poisonEnemyStatus)
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().TakePoisonDamage(Poisondamage, PoisonDamagePeriod);
                    TakePoisonDamage = true;
                    Debug.Log($"TakePoisonDamage = {TakePoisonDamage}");
                }
                else if (backstabVector.Backstapble())
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().TakeBackstabDamage(damage);
                    TakebackstabDamage = true;
                    Debug.Log($"TakebackstabDamage = {TakebackstabDamage}");
                }
                else
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().TakeNormalDamage(damage);
                    TakeNormalDamage = true;
                    Debug.Log($"TakeNormalDamage = {TakeNormalDamage}");
                }
            }
        }

    }



    private void Update()
    {
        AttackNormalOrPoison();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
#region timeBtwAttack


//if (timeBtwAttack<0)
//{
//    if (Input.GetKeyDown(KeyCode.Mouse0))
//    {
//        animator.SetBool("isAttacking", true);
//        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsAnamy);
//        for (int i = 0; i < enemiesToDamage.Length; i++)
//        {
//            enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(damage);
//        }
//    }
//    timeBtwAttack = startTimeBtwAttack;
//}
//else
//{
//    animator.SetBool("isAttacking", false);
//    timeBtwAttack -= Time.deltaTime;
//}
#endregion

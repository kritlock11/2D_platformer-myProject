using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public int ProjectileDamage;
    public Slider enemyHpBar;
    public GameObject destroyEffect;
    public float time;

    private void Start()
    {
        //Invoke("DestroyProjectile", time);
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other != null)
            {
                other.gameObject.GetComponent<PlayerController>().TakeNormalDamage(ProjectileDamage);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            if (other != null)
            {
                enemyHpBar.gameObject.SetActive(true);
                enemyHpBar.value = other.gameObject.GetComponent<EnemyController>().currentHealth;
                other.gameObject.GetComponent<EnemyController>().TakeNormalDamage(ProjectileDamage);
                Debug.Log($"{other}");
                Instantiate(destroyEffect, transform.position, Quaternion.identity);

            }
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Map")|| other.gameObject.CompareTag("projectileKnife"))
        {
            if (other != null)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject,0.2f);
            }
        }
    }
}




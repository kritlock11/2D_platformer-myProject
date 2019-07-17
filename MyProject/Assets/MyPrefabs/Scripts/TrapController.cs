using UnityEngine;
using UnityEngine.UI;

public class TrapController : MonoBehaviour
{
    public int trapDamage;
    public Slider enemyHpBar;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other != null)
            {
                other.gameObject.GetComponent<PlayerController>().TakeNormalDamage(trapDamage);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            if (other != null)
            {
                if (other.gameObject.GetComponent<Vectors>().distansePE < 9*9)
                {
                    enemyHpBar.gameObject.SetActive(true);
                    enemyHpBar.value = other.gameObject.GetComponent<EnemyController>().currentHealth;
                }
                other.gameObject.GetComponent<EnemyController>().TakeNormalDamage(trapDamage);
            }
            Destroy(gameObject);
        }
    }
}

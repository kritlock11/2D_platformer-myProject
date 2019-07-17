using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurseCounterController : MonoBehaviour
{

    public ParticleSystem Shine;
    public ParticleSystem PickUpVFX;
    private SpriteRenderer Sprite;
    private void Start()
    {
        //hpPotionPickUpVFX.Stop();
        Sprite = GetComponent<SpriteRenderer>();
    }

    //heal logic
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))

        {

            if (gameObject.CompareTag("hpBottle"))
            {
                other.gameObject.GetComponent<PlayerController>().hpPotionCounter++;
                Shine.Stop();
                PickUpVFX.Play();
                Sprite.enabled = false;
            }

            if (gameObject.CompareTag("poisonBottle"))
            {
                other.gameObject.GetComponent<PlayerController>().poisonPotionCounter++;
                Shine.Stop();
                PickUpVFX.Play();
                Sprite.enabled = false;
            }
            if (gameObject.CompareTag("SmallKey"))
            {
                other.gameObject.GetComponent<PlayerController>().KeyCounter++;
                Shine.Stop();
                PickUpVFX.Play();
                Sprite.enabled = false;
            }
            if (gameObject.CompareTag("coins"))
            {
                other.gameObject.GetComponent<PlayerController>().coinCounter++;
                Shine.Stop();
                PickUpVFX.Play();
                Sprite.enabled = false;
            }
            Destroy(gameObject, 0.5f);

        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().currentHealth += other.GetComponent<EnemyController>().bottleHeal;
            Shine.Stop();
            PickUpVFX.Play();
            Sprite.enabled = false;
            Destroy(gameObject, 0.5f);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyForDoorOpen : MonoBehaviour
{
    
    bool _isPickUped;
    public ParticleSystem KeyVFXpickUp;
    private SpriteRenderer _keySprite;



    private void Start()
    {
        _keySprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPickUped = true;
            other.gameObject.GetComponent<PlayerController>().KeyCounter++;
            _keySprite.enabled = false;

            DestroyKey();
            KeyVFXpickUp.Play();
        }
    }

    public void DestroyKey()
    {
        StartCoroutine(DestroyKeyObject());
    }
    IEnumerator DestroyKeyObject()
    {
        if (_isPickUped == true)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}

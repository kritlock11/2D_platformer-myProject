using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiEnemy : MonoBehaviour
{
    public float lifeTime;
    float startLifeTime;
    public Transform position;
    //public Animation animator;
    private void Start()
    {
        startLifeTime = lifeTime;
        position = GameObject.FindGameObjectWithTag("Enemy").transform;
    }
    void Update()
    {

        //animator. .Play();

        transform.position = position.gameObject.GetComponent<EnemyController>().emojiPos.transform.position;
        startLifeTime -= Time.deltaTime;
        if (startLifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}

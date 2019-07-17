using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDash : MonoBehaviour
{
    GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"{other} / {target.gameObject.GetComponent<PlayerController>().isDushing}");
        if (target.gameObject.GetComponent<PlayerController>().isDushing == true && other.gameObject.CompareTag("Enemy") == true)
        {
            target.GetComponent<Rigidbody2D>().gravityScale = 0;
            target.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"{other} / {target.gameObject.GetComponent<PlayerController>().isDushing}");
        if (other.gameObject.CompareTag("Enemy") == true)
        {
            target.GetComponent<Rigidbody2D>().gravityScale = 5;
            target.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }

}

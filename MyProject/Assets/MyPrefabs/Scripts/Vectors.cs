using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vectors : MonoBehaviour
{
    EnemyController enemy;
    public Slider enemyHpBar;


    public Vector3 pP;
    public Vector3 pE;
    public Vector3 directionEP;
    public Vector3 directionPE;


    private float vLeft;
    private float vRight;
    public float distansePE;



    public Transform P;
    public Transform E;

    private void Start()
    {
        enemy = GetComponent<EnemyController>();
    }

    void Update()
    {
        DrawVectorLines();
    }

    void DrawVectorLines()
    {
        pP = P.position;
        pE = E.position;

        directionEP = pE - pP;
        directionEP.Normalize();
        directionPE = pP - pE;
        directionPE.Normalize();


        vLeft = Vector2.Dot(directionEP, new Vector2(1, 0));
        vRight = Vector2.Dot(directionPE, new Vector2(-1, 0));
        //distansePE = Vector2.Distance(P.transform.position, E.transform.position);
        distansePE = (pP-pE).sqrMagnitude;

        if (distansePE > 100)
        {
            enemyHpBar.gameObject.SetActive(false);
        }

        //Debug.Log(distansePE);
        //Debug.DrawLine(pP, pE, Color.red);

        //Debug.Log($"vLeft : {vLeft}, vRight {vRight}, player.lookDirection.x {enemy.moovingLeft}, Backstapble :{Backstapble()}"); 
    }

    public bool Backstapble()
    {
        if ((vLeft <= 0 && !enemy.moovingLeft) || (vRight >= 0 && enemy.moovingLeft))
        {
            return true;
        }
        return false;
    }


}

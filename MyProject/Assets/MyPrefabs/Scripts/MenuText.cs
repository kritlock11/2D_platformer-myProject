using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuText : MonoBehaviour
{
    public Transform textRotationChange;

    private void Start()
    {
        textRotationChange = GetComponent<Transform>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            textRotationChange.gameObject.SetActive(false);
        }
    }
}

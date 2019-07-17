using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class TextController : MonoBehaviour
{
    public TextMeshPro TextMeshPro;
    public Transform textRotationChange;


    private void Start()
    {
        textRotationChange = GetComponent<RectTransform>();
    }

    public void Update()
    {
        textRotationChange.rotation = Quaternion.identity;
    }

    private void Awake()
    {
        TextMeshPro = GetComponent<TextMeshPro>();
    }

    public void SetHitCounter(int hitCount)
    {
        TextMeshPro.SetText(hitCount.ToString());
    }









}

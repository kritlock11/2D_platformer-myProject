using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dedHpSlider : MonoBehaviour
{
    public Transform textRotationChange;
    public void Update()
    {
        textRotationChange.rotation = Quaternion.identity;
    }

}

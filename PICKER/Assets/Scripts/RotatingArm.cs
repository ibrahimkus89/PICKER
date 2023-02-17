using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingArm : MonoBehaviour
{
    private bool rotate;

    [SerializeField] private float RotateValue;
    public void StartRotate()
    {
        rotate=true;
    }

    
    void Update()
    {
        if (rotate) 
        transform.Rotate(0,0,RotateValue,Space.Self);
    }
}

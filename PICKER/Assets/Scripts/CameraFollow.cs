using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    

   
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,target.position+offset,.125f);
    }
}

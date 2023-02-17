using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
   
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("CountBall"))
        {
            _gameManager.BallCount();
        }
    }
}

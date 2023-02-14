using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Messenger : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickerBorderObj"))
        {
            _gameManager.BorderReach();
        }
    }
}

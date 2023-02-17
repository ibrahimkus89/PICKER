using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ballitem : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private string ItemType;
    [SerializeField] private int BonusBallIndex;



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickerBorderObj"))
        {
            if (ItemType=="Palet")
            {
                _gameManager.RevealThePalettes();
                gameObject.SetActive(false);
            }
           else 
            {
                _gameManager.BonusBallAdd(BonusBallIndex);
                gameObject.SetActive(false);
            }


        }
    }
}

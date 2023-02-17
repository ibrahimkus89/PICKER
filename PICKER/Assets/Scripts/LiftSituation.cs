using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftSituation : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Animator BarrierField;


    public void BarrierRemove()
    {
        BarrierField.Play("BarrierRemove");
    }

    public void Finish()
    {
        _gameManager.PickerMoveSituation = true;
    }
}

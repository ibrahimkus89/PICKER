using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PickerObj;
    [SerializeField] private bool PickerMoveSituation;


    void Start()
    {
        PickerMoveSituation = true;
    }

    
    void Update()
    {
        if (PickerMoveSituation)
        {
            PickerObj.transform.position += 5f * Time.deltaTime * PickerObj.transform.forward;

            if (Time.timeScale!=0)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    PickerObj.transform.position = Vector3.Lerp(PickerObj.transform.position,
                        new Vector3(PickerObj.transform.position.x -.1f,PickerObj.transform.position.y,PickerObj.transform.position.z),.05f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    PickerObj.transform.position = Vector3.Lerp(PickerObj.transform.position,
                        new Vector3(PickerObj.transform.position.x  +.1f, PickerObj.transform.position.y, PickerObj.transform.position.z),.05f);
                }
            }
        }
    }


    public void BorderReach()
    {
        PickerMoveSituation=false;
        Debug.Log("here");
    }
}

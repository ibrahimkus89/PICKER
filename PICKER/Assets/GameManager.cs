using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;

[Serializable]

public class BallFieldProcess
{
    public Animator BallFieldAnimator;
    public TextMeshProUGUI numberText;
    public int AgBall;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PickerObj;
    [SerializeField] private GameObject BallControlObj;
    [SerializeField] private bool PickerMoveSituation;

    private int AtlBallNumber;
    [SerializeField] private List<BallFieldProcess> _BallFieldProcess =new List<BallFieldProcess>();
    void Start()
    {
        PickerMoveSituation = true;
        _BallFieldProcess[0].numberText.text = AtlBallNumber + "/" + _BallFieldProcess[0].AgBall;
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

        Collider[] hitCol = Physics.OverlapBox(BallControlObj.transform.position,BallControlObj.transform.localScale / 2,Quaternion.identity);

        int i = 0;
        while (i<hitCol.Length)
        {

            hitCol[i].GetComponent<Rigidbody>().AddForce(new Vector3(0,0,.8f),ForceMode.Impulse);

            i++;
        }

        Debug.Log(i);
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(BallControlObj.transform.position,BallControlObj.transform.localScale);
    //}
}

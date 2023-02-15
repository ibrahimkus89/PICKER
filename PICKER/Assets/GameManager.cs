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
    public GameObject[] Balls;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PickerObj;
    [SerializeField] private GameObject BallControlObj;
     public  bool PickerMoveSituation;

    private int AtlBallNumber;
    private int totalNumberOfCheckpoint;
    private int mevCheckpointIndex;
    [SerializeField] private List<BallFieldProcess> _BallFieldProcess =new List<BallFieldProcess>();
    void Start()
    {
        PickerMoveSituation = true;
        for (int i = 0; i <_BallFieldProcess.Count; i++)
        {
            _BallFieldProcess[i].numberText.text = AtlBallNumber + "/" + _BallFieldProcess[i].AgBall;
        }
        
          totalNumberOfCheckpoint = _BallFieldProcess.Count-1;
       
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
        Invoke("StageControl",2f);
        Collider[] hitCol = Physics.OverlapBox(BallControlObj.transform.position,BallControlObj.transform.localScale / 2,Quaternion.identity);

        int i = 0;
        while (i<hitCol.Length)
        {

            hitCol[i].GetComponent<Rigidbody>().AddForce(new Vector3(0,0,.8f),ForceMode.Impulse);

            i++;
        }

        Debug.Log(i);
    }


    public void BallCount()
    {
        AtlBallNumber++;
        _BallFieldProcess[mevCheckpointIndex].numberText.text = AtlBallNumber + "/" + _BallFieldProcess[mevCheckpointIndex].AgBall;

    }

    void StageControl()
    {
        if (AtlBallNumber >= _BallFieldProcess[mevCheckpointIndex].AgBall)
        {
            _BallFieldProcess[mevCheckpointIndex].BallFieldAnimator.Play("Lift");
            foreach (var item in _BallFieldProcess[mevCheckpointIndex].Balls)
            {
                item.SetActive(false);
            }

            if (mevCheckpointIndex==totalNumberOfCheckpoint)
            {
                Debug.Log("Game Over");// win panel
                Time.timeScale = 0;
            }
            else
            {
                mevCheckpointIndex++;
                AtlBallNumber = 0;

            }

        }
        else
        {
            Debug.Log("Lost");// lost panel
        }
    }

    
}

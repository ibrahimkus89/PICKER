using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.SceneManagement;

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
    [Header("----BALL MANAGEMENT")]
    [SerializeField] private GameObject PickerObj;
    [SerializeField] private GameObject[] PickerPalets;
    [SerializeField] private GameObject[] BonusBalls;
    private bool AreTherePallets;
    [SerializeField] private GameObject BallControlObj;
     public  bool PickerMoveSituation;

     [Header("-----------SOUNDS")] [SerializeField]
     private AudioSource[] Sounds;

     
     [Header("--------PANELS")]
     [SerializeField] private GameObject[] Panels;

    private int AtlBallNumber;
    private int totalNumberOfCheckpoint;
    private int mevCheckpointIndex;
    [SerializeField] private List<BallFieldProcess> _BallFieldProcess =new List<BallFieldProcess>();
    private float fingerPosX;
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
                if (Input.touchCount>0)
                {
                    Touch touch=Input.GetTouch(0);
                    Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,10f));
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            fingerPosX = TouchPosition.x - PickerObj.transform.position.x;
                            break;
                        case TouchPhase.Moved:

                            if (TouchPosition.x - fingerPosX > -1.15 && TouchPosition.x - fingerPosX < 1.15)
                            {
                                PickerObj.transform.position=Vector3.Lerp(PickerObj.transform.position,
                                    new Vector3(TouchPosition.x - fingerPosX,PickerObj.transform.position.y,PickerObj.transform.position.z),3f);
                            }
                            break;
                    }
                }

                //if (Input.GetKey(KeyCode.LeftArrow))
                //{
                //    PickerObj.transform.position = Vector3.Lerp(PickerObj.transform.position,
                //        new Vector3(PickerObj.transform.position.x -.1f,PickerObj.transform.position.y,PickerObj.transform.position.z),.05f);
                //}
                //if (Input.GetKey(KeyCode.RightArrow))
                //{
                //    PickerObj.transform.position = Vector3.Lerp(PickerObj.transform.position,
                //        new Vector3(PickerObj.transform.position.x  +.1f, PickerObj.transform.position.y, PickerObj.transform.position.z),.05f);
                //}
            }
        }
    }


    public void BorderReach()
    {
        if (AreTherePallets)
        {
            PickerPalets[0].SetActive(false);
            PickerPalets[1].SetActive(false);
        }

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
            Sounds[0].Play();

            if (mevCheckpointIndex==totalNumberOfCheckpoint)
            {
                Sounds[3].Play();
                Panels[1].SetActive(true);
                Time.timeScale = 0;
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            }
            else
            {
                mevCheckpointIndex++;
                AtlBallNumber = 0;
                if (AreTherePallets)
                {
                    PickerPalets[0].SetActive(true);
                    PickerPalets[1].SetActive(true);
                }
            }

        }
        else
        {
            Sounds[2].Play();
            Panels[2].SetActive(true);
        }
    }


    public void RevealThePalettes()
    {
        Sounds[1].Play();
        AreTherePallets =true;
        PickerPalets[0].SetActive(true);
        PickerPalets[1].SetActive(true);
    }

    public void BonusBallAdd(int BonusBallIndex)
    {
        Sounds[1].Play();
        BonusBalls[BonusBallIndex].SetActive(true);
    }

    public void ButtonProcess(string process)
    {
        switch (process)
        {
            case "Pause": 
                Panels[0].SetActive(true);
                Time.timeScale = 0;
                break;
            case "Resume":
                Panels[0].SetActive(false);
                Time.timeScale = 1;
                break;
            case "Retry":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
            case "Next":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
            case "Settings":
                // you can make a settings panel
            break;
            case "Quit":
                Application.Quit();
                break;
        }
    }
}

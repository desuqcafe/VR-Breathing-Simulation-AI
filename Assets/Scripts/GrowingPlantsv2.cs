using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class GrowingPlantsv2 : MonoBehaviour
{


    bool phaseOneDone = false;
    bool phaseTwoDone = false;
    bool phaseThreeDone = false;
    bool phaseFourDone = false;
    bool phaseFiveDone = false;

    public TMP_FontAsset KoreanFont;
    public TMP_FontAsset EnglishFont;

    public BreathingExercise breath_scrpt;

    public GameObject[] phaseOne;
    public GameObject[] phaseTwo;
    public GameObject[] phaseThree;
    public GameObject[] phaseFour;
    public GameObject visualEffect;
    public GameObject[] phaseFive;
    // public GameObject[] phaseThree;


    Vector3 TargetVector = new Vector3(1.0f, 1.1f, 1.0f);
    Vector3 ResetVector = new Vector3(1.0f, 0.1f, 1.0f);   // reset all


    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)      //  s key starts    c key completes
        {
            breath_scrpt.coroutineRunning = false;
            ResetPhasesScaleAndRendering();
        } else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ActivateAllPhaseandRender();
        }

        if (breath_scrpt.phaseOneComplete && !phaseOneDone) {
            treeScale();
        }

        if (breath_scrpt.phaseTwoComplete && !phaseTwoDone) {
            flowerScale();
            phaseOneDone = true;
        }

        if (breath_scrpt.phaseTwoComplete && !phaseThreeDone) {
            flowerPhaseThreeScale();
            phaseTwoDone = true;
        }

        if (breath_scrpt.phaseThreeComplete && !phaseFourDone) {
            treePhaseFourScale();
            AuroraPhaseFive();
            phaseThreeDone = true;
        }

        if (breath_scrpt.phaseFiveComplete && !phaseFiveDone) {
            phaseFourDone = true;
        }
    }

void treeScale()
{
     for(int i=0; i<phaseOne.Length; i++)
     {
         MeshRenderer m = phaseOne[i].GetComponent<MeshRenderer>();
         m.enabled = true;

            if (phaseOne[i].transform.localScale.y <= TargetVector.y)
             {
                   Vector3 v3ScaleOne = phaseOne[i].transform.localScale;
                   phaseOne[i].transform.localScale = new Vector3(v3ScaleOne.x, v3ScaleOne.y + Random.Range(0.0007f, 0.0015f), v3ScaleOne.z);
             }
     }
}

    void flowerScale()
    {
     for(int i=0; i<phaseTwo.Length; i++)
        {

        MeshRenderer m = phaseTwo[i].GetComponent<MeshRenderer>();
        m.enabled = true;

          if (phaseTwo[i].transform.localScale.y <= TargetVector.y)
           {
                 Vector3 v3ScaleOne = phaseTwo[i].transform.localScale;
                 phaseTwo[i].transform.localScale = new Vector3(v3ScaleOne.x, v3ScaleOne.y + Random.Range(0.005f, 0.009f), v3ScaleOne.z);
           }
        }
    }

    void flowerPhaseThreeScale()
    {
     for(int i=0; i<phaseThree.Length; i++)
        {

        MeshRenderer m = phaseThree[i].GetComponent<MeshRenderer>();
        m.enabled = true;

          if (phaseThree[i].transform.localScale.y <= TargetVector.y)
           {
                 Vector3 v3ScaleOne = phaseThree[i].transform.localScale;
                 phaseThree[i].transform.localScale = new Vector3(v3ScaleOne.x, v3ScaleOne.y + Random.Range(0.0007f, 0.0015f), v3ScaleOne.z);
           }
        }
                 
                 
              visualEffect.SetActive(true);   // Floating particles effect
    }

    void treePhaseFourScale()
    {
     for(int i=0; i<phaseFour.Length; i++)
        {

        MeshRenderer m = phaseFour[i].GetComponent<MeshRenderer>();
        m.enabled = true;

          if (phaseFour[i].transform.localScale.y <= TargetVector.y)
           {
                 Vector3 v3ScaleOne = phaseFour[i].transform.localScale;
                 phaseFour[i].transform.localScale = new Vector3(v3ScaleOne.x, v3ScaleOne.y + Random.Range(0.0007f, 0.0015f), v3ScaleOne.z);

           }
        }
    }

    void AuroraPhaseFive()
    {
     for(int i=0; i<phaseFive.Length; i++)
        {
            MeshRenderer m = phaseFive[i].GetComponent<MeshRenderer>();
            m.enabled = true;
        }
      phaseFiveDone = true;
    }

public void ResetPhasesScaleAndRendering()
{

    phaseOneDone = false;
    phaseTwoDone = false;
    phaseThreeDone = false;
    phaseFourDone = false;
    phaseFiveDone = false;

    breath_scrpt.phaseOneComplete = false;
    breath_scrpt.phaseTwoComplete = false;
    breath_scrpt.phaseThreeComplete = false;
    breath_scrpt.phaseFourComplete = false;
    breath_scrpt.phaseFiveComplete = false;

    breath_scrpt.EnglishDefault = false;
    breath_scrpt.language.text = "KR";

    breath_scrpt.inhaleExhale.font = EnglishFont;
    breath_scrpt.inhaleExhale.fontSize = 180;
            

    breath_scrpt.inhaleExhale.text = "VR Breathe";
    breath_scrpt.stage_number.text = "0 / 4";


    // Set the scale and mesh rendering of each element in the phaseOne array
    // to their original values.
    for (int i = 0; i < phaseOne.Length; i++)
    {
        phaseOne[i].transform.localScale = ResetVector;
        phaseOne[i].GetComponent<MeshRenderer>().enabled = false;
    }

    // Set the scale and mesh rendering of each element in the phaseTwo array
    // to their original values.
    for (int i = 0; i < phaseTwo.Length; i++)
    {
        phaseTwo[i].transform.localScale = ResetVector;
        phaseTwo[i].GetComponent<MeshRenderer>().enabled = false;
    }

    // Set the scale and mesh rendering of each element in the phaseThree array
    // to their original values.
    for (int i = 0; i < phaseThree.Length; i++)
    {
        phaseThree[i].transform.localScale = ResetVector;
        phaseThree[i].GetComponent<MeshRenderer>().enabled = false;
    }

    // Set the scale and mesh rendering of each element in the phaseFour array
    // to their original values.
    for (int i = 0; i < phaseFour.Length; i++)
    {
        phaseFour[i].transform.localScale = ResetVector;
        phaseFour[i].GetComponent<MeshRenderer>().enabled = false;
    }

    for (int i = 0; i < phaseFive.Length; i++)
    {
        phaseFive[i].GetComponent<MeshRenderer>().enabled = false;
    }

    // Reset the visual effect to its original state.
    visualEffect.SetActive(false);
}


public void ActivateAllPhaseandRender()
{

    phaseOneDone = true;
    phaseTwoDone = true;
    phaseThreeDone = true;
    phaseFourDone = true;
    phaseFiveDone = true;

    breath_scrpt.phaseOneComplete = true;
    breath_scrpt.phaseTwoComplete = true;
    breath_scrpt.phaseThreeComplete = true;
    breath_scrpt.phaseFourComplete = true;
    breath_scrpt.phaseFiveComplete = true;

    breath_scrpt.EnglishDefault = false;
    breath_scrpt.language.text = "KR";

    breath_scrpt.inhaleExhale.font = EnglishFont;
    breath_scrpt.inhaleExhale.fontSize = 180;


    breath_scrpt.inhaleExhale.text = "VR - Breathe";
    breath_scrpt.stage_number.text = "4 / 4";


    treeScale();
    flowerScale();
    flowerPhaseThreeScale();
    treePhaseFourScale();
    AuroraPhaseFive();
}

}

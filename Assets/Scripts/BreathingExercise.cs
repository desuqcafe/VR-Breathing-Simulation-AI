using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public enum BreathPhase
{
    INHALE,
    HOLD,
    EXHALE,
    REST
}

public class BreathingExercise : MonoBehaviour
{
    //timer and instructions UI
    public TMP_Text inhaleExhale;
    public TMP_Text language;
    public TextMeshProUGUI timer;

    public TextMeshProUGUI stage_number;

    //time remaining for current phase
    public float timeRemaining = 5;
    public bool timerIsRunning = false;

    //flags for tracking completed phases
    public bool phaseOneComplete = false;
    public bool phaseTwoComplete = false;
    public bool phaseThreeComplete = false;
    public bool phaseFourComplete = false;
    public bool phaseFiveComplete = false;

    public bool EnglishDefault = false;

    public TMP_FontAsset KoreanFont;
    public TMP_FontAsset EnglishFont;


    public bool coroutineRunning = false;

    //reference to upandDown script
    public upandDown upDown_scrpt;

    //current phase of the exercise
    public BreathPhase currentPhase = BreathPhase.REST;
    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //update timer if running
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timer.text = timeRemaining.ToString("N0");
            }
            else
            {
                //reset timer and stop running
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }

        //start deep breath coroutine when L key is pressed
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            deepBreathCoRoutine();
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && !EnglishDefault)
        {
            EnglishDefault = true;
            language.text = "EN";
        } else if (Keyboard.current.eKey.wasPressedThisFrame && EnglishDefault) {
            EnglishDefault = false;
            language.text = "KR";
        }
    }

    public void deepBreathCoRoutine() 
    {
        if (!coroutineRunning) 
        {
        coroutineRunning = true;
        StartCoroutine(deepBreath());
        }
    }

    IEnumerator deepBreath()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)      //  s key starts    c key completes
        {
            yield break;
            
        } else {

            if (!EnglishDefault) {
                    inhaleExhale.font = KoreanFont;
                    inhaleExhale.fontSize = 153;
            } else {
                    inhaleExhale.font = EnglishFont;
                    inhaleExhale.fontSize = 180;
            }

            bool phaseStart = false;



        for (int i = 0; i < 4; i++) {
            if (!phaseStart) {
            currentPhase = BreathPhase.REST;
//            movementSelector(BreathPhase.REST);

            if (!EnglishDefault) {
               inhaleExhale.text = "준비";
            } else {
               inhaleExhale.text = "Get Ready";
            }

            timeRemaining = 5;
            timerIsRunning = true;
            phaseStart = true;

            if(!coroutineRunning) {
                yield break;
            }

            yield return new WaitForSeconds(5.0f);
            }

        currentPhase = BreathPhase.INHALE;
//        movementSelector(BreathPhase.INHALE);

            if (!EnglishDefault) {
               inhaleExhale.text = "들이마시세요";
            } else {
               inhaleExhale.text = "Inhale ~";
            }

        timeRemaining = 4;
        timerIsRunning = true;

            if(!coroutineRunning) {
                yield break;
            }

        yield return new WaitForSeconds(4.0f);  // Inhale 


        currentPhase = BreathPhase.HOLD;
  //      movementSelector(BreathPhase.HOLD);

            if (!EnglishDefault) {
               inhaleExhale.text = "잠깐 멈추세요";
            } else {
               inhaleExhale.text = "Hold...";
            }

        timeRemaining = 4;
        timerIsRunning = true;

            if(!coroutineRunning) {
                yield break;
            }

        yield return new WaitForSeconds(4.0f);  // Hold 


        currentPhase = BreathPhase.EXHALE;
  //      movementSelector(BreathPhase.EXHALE);
        inhaleExhale.text = "Exhale ~";

            if (!EnglishDefault) {
               inhaleExhale.text = "내쉬세요";
            } else {
               inhaleExhale.text = "Exhale ~";
            }

        timeRemaining = 4;
        timerIsRunning = true;


            if(!coroutineRunning) {
                yield break;
            }

        yield return new WaitForSeconds(4.0f);  // Exhale 

        //check for completed phases and update flags
        phaseChecker();

        }

            if(!coroutineRunning) {
                yield break;
            }

        //set movement for current phase
        currentPhase = BreathPhase.REST;
 //       movementSelector(BreathPhase.REST);
            if (!EnglishDefault) {
               inhaleExhale.text = "좋아요!";
            } else {
               inhaleExhale.text = "Finished!";
            }

        timer.text = "";

        coroutineRunning = false;
        }

    }

    void movementSelector(BreathPhase phase)
    {
        //set movement based on current phase
        switch (phase)
        {
            case BreathPhase.INHALE:
                upDown_scrpt.speed = 1.55f;
                upDown_scrpt.height = 0.5f;
                break;
            case BreathPhase.HOLD:
                upDown_scrpt.speed = 0f;
                upDown_scrpt.height = 0.05f;
                break;
            case BreathPhase.EXHALE:
                upDown_scrpt.speed = 1.55f;
                upDown_scrpt.height = 0.5f;
                break;
            case BreathPhase.REST:
                upDown_scrpt.speed = 6.0f;
                upDown_scrpt.height = 0.05f;
                break;
        }
    }

    // void phaseChecker()
    // {
    //     //check for completed phases and update flags
    //     if (phaseOneComplete && phaseTwoComplete && phaseThreeComplete)
    //     {
    //         phaseFourComplete = true;
    //     }
    //     if (phaseOneComplete && phaseTwoComplete)
    //     {
    //         phaseThreeComplete = true;
    //     }
    //     if (phaseOneComplete)
    //     {
    //         phaseTwoComplete = true;
    //     }
    //     if (!phaseOneComplete)
    //     {
    //         phaseOneComplete = true;
    //     }
    // }

    void phaseChecker()
{
    int phase = 0;

    if (phaseOneComplete) { phase++; }
    if (phaseTwoComplete) { phase++; }
    if (phaseThreeComplete) { phase++; }
    if (phaseFourComplete) { phase++; }

    switch (phase)
    {
        case 0:
            phaseOneComplete = true;
            stage_number.text = "1 / 4";
            break;
        case 1:
            phaseTwoComplete = true;
            stage_number.text = "2 / 4";
            break;
        case 2:
            phaseThreeComplete = true;
            stage_number.text = "3 / 4";
            break;
        case 3:
            phaseFourComplete = true;
            phaseFiveComplete = true;
            stage_number.text = "4 / 4";
            break;
        case 4:
   //         phaseFiveComplete = true;
   //         stage_number.text = "5 / 5";
            break;
    }
}
}
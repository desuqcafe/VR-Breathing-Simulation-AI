// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using TMPro;

// public class BasicTreeAnimation : MonoBehaviour
// {
//     // public TextMeshProUGUI inhaleExhale;
//     // public TextMeshProUGUI timer;

//     public SineWave sineScript;


//     private Animator mAnimator;

//     public AudioSource audioSource;
    
//     public AudioClip audioClip;


//     public float timeRemaining = 5;
//     public bool timerIsRunning = false;
//     bool secondTimer = false;

//     bool normal = true;
//     bool breath = false;
//     bool exhale = false;

//     bool reset = false;
//     bool goodToGo = false;



//     // Start is called before the first frame update
//     void Start()
//     {
//         mAnimator = GetComponent<Animator>();
//         StartCoroutine(breathingExercise());
//     }

//     // Update is called once per frame
//     void Update()
//     {
// //         if (mAnimator != null)
// //         {
// //  //           if(Input.GetKeyDown(KeyCode.L))
// //             if(Keyboard.current.lKey.wasPressedThisFrame)
// //             {
// //     //            mAnimator.SetTrigger("TrLeft");
// //      //           audioSource.PlayOneShot(audioClip);
// //                 inhaleExhale.text = "Inhale";
// //                 sineScript.amplitude = 0.45f;
// //             }

// //        //     if(Input.GetKeyDown(KeyCode.R))
// //            if(Keyboard.current.rKey.wasPressedThisFrame)
// //             {
// //       //          mAnimator.SetTrigger("TrRight");
// //       //          audioSource.PlayOneShot(audioClip);
// //                 inhaleExhale.text = "Exhale";
// //                 sineScript.amplitude = 0.45f;
// //             }
// //         }


//         if (timerIsRunning)
//         {
//             if (timeRemaining > 0)
//             {

//                     if(secondTimer) 
//                     {
//                         timeRemaining -= Time.deltaTime;
//                         timer.text = timeRemaining.ToString("N0");
//                     }
//                     else if (!secondTimer)
//                     {
//                         timeRemaining -= Time.deltaTime;
//                         inhaleExhale.text = timeRemaining.ToString("N0");
//                     }
//             }
//             else
//             {
//                 timeRemaining = 0;
//                 timerIsRunning = false;
//             }
//         }


//     if (normal) 
//     {
//         Vector3 newRotation = new Vector3((sineScript.ampCounter * 10), -108, 0);
//         transform.eulerAngles = newRotation;
//     } else if (breath)
//     {



//         if (reset) 
//         {
//             if (sineScript.ampCounter < 0.1 && sineScript.ampCounter > -0.1) 
//             {
//                 reset = false;
//                 goodToGo = true;

//             } else if (sineScript.ampCounter > 0.1) {
//                 sineScript.ampCounter -= 0.025f;
//             } else if (sineScript.ampCounter < -0.1) {
//                 sineScript.ampCounter += 0.025f;
//             }
//         }

//         if (goodToGo)
//         {
//         Vector3 newRotation = new Vector3(Mathf.Abs(sineScript.ampCounter * 40), -108, 0);
//         transform.eulerAngles = newRotation;
//         } else {
//             Vector3 newRotation = new Vector3((sineScript.ampCounter * 40), -108, 0);
//             transform.eulerAngles = newRotation;  
//         }



//     } else if (exhale) 
//     {




//         if (reset) 
//         {
//             if (sineScript.ampCounter < 0.1 && sineScript.ampCounter > -0.1) 
//             {
//                 reset = false;
//                 goodToGo = true;

//             } else if (sineScript.ampCounter > 0.1) {
//                 sineScript.ampCounter -= 0.025f;
//             } else if (sineScript.ampCounter < -0.1) {
//                 sineScript.ampCounter += 0.025f;
//             }
//         }

//         if (goodToGo)
//         {
//         Vector3 newRotation = new Vector3(-Mathf.Abs(sineScript.ampCounter * 40), -108, 0);
//         transform.eulerAngles = newRotation;
//         } else {
//             Vector3 newRotation = new Vector3((sineScript.ampCounter * 40), -108, 0);
//             transform.eulerAngles = newRotation;  
//         }
//     }


//     }

//     IEnumerator breathingExercise () {
//      while(true){ // This creates a never-ending loop
//              timerIsRunning = true;
//              yield return new WaitForSeconds(5.0f);

//              inhaleExhale.text = "Breathe ~";
//              secondTimer = true;

//              timeRemaining = 3;
//              timerIsRunning = true;

             
//     reset = true;
//     normal = false;
//     breath = true;
//  //   exhale = false;


//         yield return new WaitForSeconds(4.0f);

//             inhaleExhale.text = "Now...";
//             timer.text = "";

//         yield return new WaitForSeconds(1.0f);


//             inhaleExhale.text = "Exhale ~";

//              timeRemaining = 4;
//              timerIsRunning = true;

//     reset = true;         
//    // normal = false;
//     breath = false;
//     exhale = true;

//         yield return new WaitForSeconds(4.0f);

//              inhaleExhale.text = "Get ready!";

//              timeRemaining = 4;
//              timerIsRunning = true;


//     normal = true;
//    // breath = false;
//     exhale = false;

//         yield return new WaitForSeconds(4.0f);    

//                     timer.text = "";   



//      }
// }
// }

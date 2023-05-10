using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.VFX;

public class OrbExpandContract : MonoBehaviour
{

    public VisualEffect vfx;
    public BreathingExercise breath_script;
    public GazeWatcher gazeWatcher;

    int countMe = 1;    
    private string phaseDataLogPath = "PhaseDataLog.txt";




    public class PhaseData
    {
        public int phase;
        public float timeSpentLookingAtPlane;
        public float averageDurationOfContinuousFocus;

        public PhaseData(int phase, float timeSpentLookingAtPlane, float averageDurationOfContinuousFocus)
        {
            this.phase = phase;
            this.timeSpentLookingAtPlane = timeSpentLookingAtPlane;
            this.averageDurationOfContinuousFocus = averageDurationOfContinuousFocus;
        }

        public override string ToString()
        {
            string phaseName = "";
            switch (phase)
            {
                case 0:
                    phaseName = "INHALE";
                    break;
                case 1:
                    phaseName = "HOLD";
                    break;
                case 2:
                    phaseName = "EXHALE";
                    break;
                case 3:
                    phaseName = "REST";
                    break;
            }
            return $"Phase: {phaseName}, Time Spent Looking At Plane: {timeSpentLookingAtPlane}, Average Duration Of Continuous Focus: {averageDurationOfContinuousFocus}";
        }
    }

    
    // List to store each phase's data
    public List<PhaseData> phaseDataList = new List<PhaseData>();

    // Start is called before the first frame update
    void Start()
    {
        //get a reference to the Visual Effect Asset attached to the GameObject
        vfx = GetComponent<VisualEffect>();
        gazeWatcher = FindObjectOfType<GazeWatcher>();
        // Initialize the PhaseDataLog file
        File.WriteAllText(phaseDataLogPath, "Phase,TimeSpentLookingAtPlane,AverageDurationOfContinuousFocus\n");
    }

    // Update is called once per frame
    void Update()
    {

     switch (breath_script.currentPhase)
        {
            case BreathPhase.INHALE:
             if (countMe == 1) {
             SetSize(3.8f);
             countMe++;
            }
             break;
            case BreathPhase.HOLD:
                if (countMe == 2)
                {
                    SetNewColorHold();

                    // Create and add PhaseData for INHALE phase
                    PhaseData phaseDataInhale = new PhaseData((int)BreathPhase.INHALE,
                        GazeWatcher.timeSpentLookingAtPlane,
                        GazeWatcher.averageDurationOfContinuousFocus);
                    phaseDataList.Add(phaseDataInhale);

                    // Write PhaseData to the PhaseDataLog file
                    WritePhaseDataToLog(phaseDataInhale);

                    // Reset gaze data in GazeWatcher
                    gazeWatcher.ResetGazeData();
                    countMe++;
                }
                break;
            case BreathPhase.EXHALE:
                if (countMe == 3)
                {
                    SetBaseColorHold();
                    SetSize(3.8f);

                    // Create and add PhaseData for HOLD phase
                    PhaseData phaseDataHold = new PhaseData((int)BreathPhase.HOLD,
                        GazeWatcher.timeSpentLookingAtPlane,
                        GazeWatcher.averageDurationOfContinuousFocus);
                    phaseDataList.Add(phaseDataHold);

                    // Write PhaseData to the PhaseDataLog file
                    WritePhaseDataToLog(phaseDataHold);

                    // Reset gaze data in GazeWatcher
                    gazeWatcher.ResetGazeData();

                    countMe = (countMe - 2);
                }
                break;
            case BreathPhase.REST:
                SetSize(0.3f);

                // Create and add PhaseData for EXHALE phase
                PhaseData phaseDataExhale = new PhaseData((int)BreathPhase.EXHALE,
                    GazeWatcher.timeSpentLookingAtPlane,
                    GazeWatcher.averageDurationOfContinuousFocus);
                phaseDataList.Add(phaseDataExhale);

                // Write PhaseData to the PhaseDataLog file
                WritePhaseDataToLog(phaseDataExhale);

                // Reset gaze data in GazeWatcher
                gazeWatcher.ResetGazeData();
                break;
        }
    }

    void WritePhaseDataToLog(PhaseData phaseData)
    {
        using (StreamWriter writer = new StreamWriter(phaseDataLogPath, true))
        {
            writer.WriteLine($"{phaseData.phase},{phaseData.timeSpentLookingAtPlane},{phaseData.averageDurationOfContinuousFocus}");
        }
    }

    public void SetNewColorHold()
    {
        //   vfx.SetFloat("size", 0.2f * Mathf.PingPong(Time.time * 0.5f, 0.5f) + 0.3f);
        // Create a new Color object with the specified red, green, blue, and alpha values
        Color customColor = new Color(0.3275424f, 1.467875f, 0.9219711f, 0.0f);

    // Set the "color" parameter to the custom color
    vfx.SetVector4("color", customColor);

    }

    public void SetBaseColorHold()
    {
     //   vfx.SetFloat("size", 0.2f * Mathf.PingPong(Time.time * 0.5f, 0.5f) + 0.3f);
         // Create a new Color object with the specified red, green, blue, and alpha values
    Color customColor = new Color(0.6065599f, 1.273776f, 2.317059f, 0.0f);

    // Set the "color" parameter to the custom color
    vfx.SetVector4("color", customColor);

    }

    public void SetSize(float duration)
    {
    // Define an animation curve that interpolates the size value over time
    AnimationCurve sizeCurve = new AnimationCurve();
    sizeCurve.AddKey(0, vfx.GetFloat("size")); // start at the current size

        switch (breath_script.currentPhase)
        {
            case BreathPhase.INHALE:
             sizeCurve.AddKey(duration, 2.8f);
             break;
            case BreathPhase.EXHALE:
             sizeCurve.AddKey(duration, 0.7f);
             break;
            case BreathPhase.REST:
             sizeCurve.AddKey(duration, 1.5f); // end at 0.8f after the given duration
             break;
        }


    // Use a coroutine to update the size value over time
    StartCoroutine(UpdateSizeOverTime(duration, sizeCurve));   
    }

    private IEnumerator UpdateSizeOverTime(float duration, AnimationCurve sizeCurve)
{
    float elapsedTime = 0;
    while (elapsedTime < duration)
    {
        // Interpolate the size value using the animation curve
        float size = sizeCurve.Evaluate(elapsedTime);

        // Set the size value on the vfx object
        vfx.SetFloat("size", size);

        // Wait for the next frame before continuing the loop
        yield return null;

        // Increase the elapsed time
        elapsedTime += Time.deltaTime;
    }

    // Set the final size value to ensure that it is exactly 0.8f
    // vfx.SetFloat("size", 0.8f);
}
}

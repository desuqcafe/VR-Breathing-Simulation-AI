using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR;

public class GazeWatcher : MonoBehaviour
{

    public ChatGPTSubscriber chatGPTSubscriber;

    public GameObject planeObject;
    public Color lookingAtColor;
    public Color notLookingAtColor;
    public float gazeDistanceThreshold = 3f;
    public LineRenderer gazeLineRenderer;
    public Camera mainCamera;
    public bool showVisualAid = false;
    public string logFilePath = "GazeLog.txt";

    private MeshRenderer planeMeshRenderer;
    private bool isLookingAtPlane = false;

    private int gazeOnCount = 0;
    private int gazeOffCount = 0;
    private int totalGazeOnTime = 0;
    private int longestGazeOnStreak = 0;
    private int currentGazeOnStreak = 0;


    // IEnumerator CheckGazeOffCount()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(5f);

    //         if (gazeOffCount > 0)
    //         {
    //             chatGPTSubscriber.SendGazeOffMessage();
    //         }
    //     }
    // }

    public void ResetGazeData()
    {
        timeSpentLookingAtPlane = 0;
        averageDurationOfContinuousFocus = 0;
        gazeOnCount = 0;
        gazeOffCount = 0;
        totalGazeOnTime = 0;
        longestGazeOnStreak = 0;
        currentGazeOnStreak = 0;
    }

    void Start()
    {
        planeMeshRenderer = planeObject.GetComponent<MeshRenderer>();
        planeMeshRenderer.material.color = new Color(0, 0, 0, 0); // Set initial color to transparent

        if (!gazeLineRenderer)
        {
            Debug.LogError("No Line Renderer assigned for gaze visualization.");
        }

        if (!mainCamera)
        {
            Debug.LogError("No main camera assigned.");
        }

        StartCoroutine(CheckGazeAndLog());
        //StartCoroutine(CheckGazeOffCount());

    }

    void Update()
    {
        List<XRNodeState> nodeStates = new List<XRNodeState>();
        InputTracking.GetNodeStates(nodeStates);

        isLookingAtPlane = false;

        foreach (XRNodeState nodeState in nodeStates)
        {
            if (nodeState.nodeType == XRNode.Head)
            {
                Vector3 localHeadPosition;
                Quaternion localHeadRotation;
                if (nodeState.TryGetPosition(out localHeadPosition) && nodeState.TryGetRotation(out localHeadRotation))
                {
                    Vector3 worldHeadPosition = mainCamera.transform.TransformPoint(localHeadPosition);
                    Vector3 worldGazeDirection = mainCamera.transform.TransformDirection(localHeadRotation * Vector3.forward);
                    RaycastHit hit;

                    if (Physics.Raycast(worldHeadPosition, worldGazeDirection, out hit, gazeDistanceThreshold))
                    {
                        if (hit.collider.gameObject == planeObject)
                        {
                            planeMeshRenderer.material.color = lookingAtColor;
                            isLookingAtPlane = true;
                        }
                        else
                        {
                            planeMeshRenderer.material.color = notLookingAtColor;
                        }

                        if (showVisualAid)
                        {
                            DrawGazeLine(worldHeadPosition, hit.point);
                        }
                    }
                    else
                    {
                        planeMeshRenderer.material.color = notLookingAtColor;

                        if (showVisualAid)
                        {
                            DrawGazeLine(worldHeadPosition, worldHeadPosition + worldGazeDirection * gazeDistanceThreshold);
                        }
                    }
                }
            }
        }

        if (!showVisualAid)
        {
            gazeLineRenderer.SetPosition(0, Vector3.zero);
            gazeLineRenderer.SetPosition(1, Vector3.zero);
        }
    }

    void DrawGazeLine(Vector3 startPoint, Vector3 endPoint)
    {
        if (gazeLineRenderer)
        {
            gazeLineRenderer.SetPosition(0, startPoint);
            gazeLineRenderer.SetPosition(1, endPoint);
        }
    }

    public static float timeSpentLookingAtPlane = 0;
    public static float averageDurationOfContinuousFocus = 0;
    IEnumerator CheckGazeAndLog()
    {
        int frameCount = 0;

        while (true)
        {
            yield return new WaitForSeconds(1f);

            frameCount += (int)(1f / Time.deltaTime);

            if (isLookingAtPlane)
            {
                gazeOnCount++;
                currentGazeOnStreak++;
                longestGazeOnStreak = Mathf.Max(longestGazeOnStreak, currentGazeOnStreak);
            }
            else
            {
                gazeOffCount++;
                currentGazeOnStreak = 0;
            }

            totalGazeOnTime = gazeOnCount;

            timeSpentLookingAtPlane = (float)totalGazeOnTime / frameCount * 100;
            averageDurationOfContinuousFocus = (float)longestGazeOnStreak;

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"Frame {frameCount}: {(isLookingAtPlane ? "1" : "0")}");
                writer.WriteLine($"Time Spent Looking at Plane: {timeSpentLookingAtPlane}");
                writer.WriteLine($"Average Duration of Continuous Focus: {averageDurationOfContinuousFocus}");
                writer.WriteLine($"Gaze On Count: {gazeOnCount}");
                writer.WriteLine($"Gaze Off Count: {gazeOffCount}");
                writer.WriteLine($"Total Gaze On Time: {totalGazeOnTime}");
                writer.WriteLine($"Longest Gaze On Streak: {longestGazeOnStreak}");
                writer.WriteLine($"Current Gaze On Streak: {currentGazeOnStreak}");
                writer.WriteLine("---------------------------------");
            }
        }
    }


}

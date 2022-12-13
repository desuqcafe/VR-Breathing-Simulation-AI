using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SineWave : MonoBehaviour
{

    public TextMeshProUGUI ampText;

    public float ampCounter;


    // If you want 2D Space, in options uncheck World Space

    public LineRenderer myLineRenderer;
    public int points;
    public float amplitude = 1;
    public float frequency = 1;
    public Vector2 xLimits = new Vector2(0,1);
    public float movementSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        myLineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(DataOutput());

    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    void Draw()
    {
        float xStart = xLimits.x;
        float Tau = 2 * Mathf.PI;
        float xFinish = xLimits.y;

        myLineRenderer.positionCount = points;

        for(int currentPoint = 0; currentPoint<points;currentPoint++)
        {
            float progress = (float)currentPoint/(points-1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin((Tau * frequency * x) + (Time.timeSinceLevelLoad * movementSpeed));
            myLineRenderer.SetPosition(currentPoint, new Vector3(x,y,0));
           // print("amplitude:" + y);
            ampCounter = y;
        }
    }

     IEnumerator DataOutput () 
     {
     while(true)
        { // This creates a never-ending loop
           yield return new WaitForSeconds(0.1f);
                ampText.text = ampCounter.ToString();
        }  
     }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingPlants : MonoBehaviour
{

    public SineWave sineScript;

    Vector3 TargetVector = new Vector3(25.0f, 25.0f, 25.0f);
    Vector3 TargetTomatoVector = new Vector3(0.245f, 0.245f, 0.245f);

    Vector3 TargetFlowerVector = new Vector3(0.425f, 0.425f, 0.425f);
    

    public GameObject wave;
    public GameObject[] scaleBushPhaseOne;
    public GameObject[] tomatoTree;
    public GameObject[] flowers;

    public GameObject[] secondPhaseFlowers;


    // bool sineP1 = true;
    // bool sineP2 = true;
    // bool sineP3 = true;


    void Start()
    {
        sineScript = wave.GetComponent<SineWave>();
    }

    void Update()
    {
        if (sineScript.ampCounter > 0.40) 
          {
            bushScale();
            flowerScale();
            tomatoScale();
            flowerTreeScale();
          }
        // else if (sineScript.amplitude >= 0.4) 
        //   {
        //     flowerScale();
        //   }
        // else if (sineScript.amplitude >= 0.4)
        //   {
        //     tomatoScale();
        //   }
    }

    void bushScale()
    {
     for(int i=0; i<scaleBushPhaseOne.Length; i++)
        {
          if (scaleBushPhaseOne[i].transform.localScale.x < TargetVector.x)
           {
                 Vector3 v3ScaleOne = scaleBushPhaseOne[i].transform.localScale;
                 scaleBushPhaseOne[i].transform.localScale = new Vector3(v3ScaleOne.x + 0.010f, v3ScaleOne.y + 0.010f, v3ScaleOne.z + 0.010f);
           }
        } 
               //    sineP1 = false;
    }

    void tomatoScale()
    {
     for(int i=0; i<tomatoTree.Length; i++)
        {
          if (tomatoTree[i].transform.localScale.x < TargetTomatoVector.x)
           {
                 Vector3 v3ScaleOne = tomatoTree[i].transform.localScale;
                 tomatoTree[i].transform.localScale = new Vector3(v3ScaleOne.x + 0.005f, v3ScaleOne.y + 0.005f, v3ScaleOne.z + 0.005f);
           }
        } 
              //      sineP3 = false;

    }

    void flowerScale()
    {
     for(int i=0; i<flowers.Length; i++)
        {
          if (flowers[i].transform.localScale.y < TargetFlowerVector.y)
           {
                 Vector3 v3ScaleOne = flowers[i].transform.localScale;
                 flowers[i].transform.localScale = new Vector3(v3ScaleOne.x, v3ScaleOne.y + Random.Range(0.0004f, 0.0009f), v3ScaleOne.z);
                //  if (flowers[i].transform.localScale.y == TargetFlowerVector.y)
                //  {
                //     v3ScaleOne = secondPhaseFlowers[i].transform.localScale;
                //     secondPhaseFlowers[i].transform.localScale = new Vector3(v3ScaleOne.x, v3ScaleOne.y + Random.Range(0.008f, 0.0014f), v3ScaleOne.z);
                //  }
           }

        } 
    }

      void flowerTreeScale()
        {
       for(int i=0; i<secondPhaseFlowers.Length; i++)
         {
          if (secondPhaseFlowers[i].transform.localScale.y < TargetFlowerVector.y)
           {
                    Vector3 v3ScaleOne = secondPhaseFlowers[i].transform.localScale;
                    secondPhaseFlowers[i].transform.localScale = new Vector3(v3ScaleOne.x, v3ScaleOne.y + Random.Range(0.0008f, 0.0014f), v3ScaleOne.z);
                    secondPhaseFlowers[i].SetActive(true);
           }
         }
        }
}
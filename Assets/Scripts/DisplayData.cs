// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR;
// using TMPro;

// public class DisplayData : MonoBehaviour
// {
//     public ChangeColor stringVal;
    
//     public TextMeshProUGUI ampText;
//     float ampCounter;


//     // Start is called before the first frame update
//     void Start()
//     {
//         StartCoroutine(DataOutput());
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         ampText.text = ampCounter.ToString();
//     //  ampText.text = stringVal.content;
//     }
//         StartCoroutine(DataOutput());

//     IEnumerator DataOutput () {
//      while(true){ // This creates a never-ending loop
//          yield return new WaitForSeconds(1.0f);
//          ampCounter = Random.Range(-1.0f, 1.0f);
//      //       print($"{stringVal.content}");
//      }
//  }
// }

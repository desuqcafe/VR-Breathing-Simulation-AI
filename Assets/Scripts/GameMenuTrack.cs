using UnityEngine;
//using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{

    public Transform head;
    public float spawnDistance = 2;

    public GameObject menu;
//    public InputActionProperty showButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(showButton.action.WasPerformedThisFrame())
    //     {
    //         menu.SetActive(!menu.activeSelf);

    //         menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
    //     }

    //     menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
    //     menu.transform.forward *= -1;  // flip menu
    // }
}

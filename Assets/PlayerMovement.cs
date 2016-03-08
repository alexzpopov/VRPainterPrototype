using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public SixenseInput sixInput;
    public GameObject player;
    public GameObject Playercamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("0  +  X :"+SixenseInput.Controllers[0].JoystickX);
        Debug.Log("0  +  Y :" + SixenseInput.Controllers[0].JoystickY);
        Debug.Log("1  +  X :" + SixenseInput.Controllers[1].JoystickX);
        Debug.Log("1  +  Y :" + SixenseInput.Controllers[1].JoystickY);

        player.transform.position += (player.transform.forward*0.1f) * SixenseInput.Controllers[0].JoystickY;
        player.transform.position += (player.transform.right*0.1f) * SixenseInput.Controllers[0].JoystickX;
        Playercamera.transform.eulerAngles += player.transform.forward * SixenseInput.Controllers[1].JoystickY;
    }
}

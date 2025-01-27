using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject InteractText_Keyboard;
    public GameObject InteractText_Controller;

    GameObject DoorHinge;
    bool rotateDoor;
    bool canOpenDoor;
    bool isOpen;
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        InteractText_Keyboard.SetActive(false);
        InteractText_Controller.SetActive(false);

        DoorHinge = GameObject.Find("DoorHinge");
        rotateDoor = false;
        canOpenDoor = false;
        isOpen = false;
        rotationSpeed = 100.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if(canOpenDoor)
        {
            if(Input.GetButtonDown("Interact"))
            {
                rotateDoor = true;
                InteractText_Keyboard.SetActive(false);
                InteractText_Controller.SetActive(false);
            }
        }
        
        if(rotateDoor)
        {
            DoorHinge.transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);

            if(DoorHinge.transform.rotation.eulerAngles.y > 350.0f)
            {
                rotateDoor = false;
                canOpenDoor = false;
                isOpen = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isOpen)
        {
            Debug.Log(gameObject.name);
            canOpenDoor = true;

            bool isUsingController = Input.GetJoystickNames().Length > 0;

            if (isUsingController)
            {
                InteractText_Controller.SetActive(true);
            }
            else
            {
                InteractText_Keyboard.SetActive(true);
            }

        }
    }

}

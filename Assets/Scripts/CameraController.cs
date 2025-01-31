using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author: youtube.com/@Filmstorm

public class CameraController : MonoBehaviour
{
    public float CameraMoveSpeed = 120.0f;
    public Transform CameraFollowObject;
    Vector3 FollorPOS;
    public float clampAngle = 80.0f;
    public float inputSensivity = 150.0f;
    public GameObject CameraObj;
    public GameObject PlayerObj;
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    public float rotX;
    public float rotY;

    public bool playerFell = false;
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.fell)
        {
            float inputX = Input.GetAxis("RightStickHorizontal");
            float inputY = Input.GetAxis("RightStickVertical");
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            finalInputX = inputX + mouseX;
            finalInputZ = inputY + mouseY;

            rotY += finalInputX * inputSensivity * Time.deltaTime;
            rotX += finalInputZ * inputSensivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;

        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(player.gameObject.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2.0f * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        Transform target = CameraFollowObject;    

        float step = CameraMoveSpeed * Time.deltaTime;

        if ((target))
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}

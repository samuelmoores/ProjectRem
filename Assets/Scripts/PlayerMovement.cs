using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    CharacterController controller;
    Camera cam;
    Animator animator;
    Transform CameraDetachLocation;
    PlayerHealth PlayerHealth;
    float startTimer = 1.0f;
    bool detachCamera = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        PlayerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 walkDirection = new Vector3(horizontal, 0.0f, vertical);
        walkDirection.Normalize();

        animator.SetFloat("speed", walkDirection.magnitude);

        if (walkDirection != Vector3.zero && PlayerHealth.Alive())
        {
            walkDirection = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * walkDirection;
            walkDirection.Normalize();

            Quaternion toRotation = Quaternion.LookRotation(walkDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 720.0f);
        }

        walkDirection.y = Physics.gravity.y;

        startTimer -= Time.deltaTime;

        if(!controller.isGrounded && startTimer < 0.0f)
        {
            if(!detachCamera)
            {
                detachCamera = true;
                GameObject.Find("CameraBase").GetComponent<CameraController>().CameraFollowObject = null;
            }

            animator.SetBool("falling", true);
        }

        if(PlayerHealth.Alive())
        {
            controller.Move(walkDirection * movementSpeed * Time.deltaTime);
        }


    }
}

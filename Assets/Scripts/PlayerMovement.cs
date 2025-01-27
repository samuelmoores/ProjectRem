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

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
        direction.Normalize();

        animator.SetFloat("speed", direction.magnitude);

        if (direction != Vector3.zero)
        {
            direction = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * direction;
            direction.Normalize();

            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 720.0f);
        }

        controller.Move(direction * movementSpeed * Time.deltaTime);


    }
}

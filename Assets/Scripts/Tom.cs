using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Tom : MonoBehaviour
{
    public bool isFloating;

    NavMeshAgent agent;
    GameObject player;
    Animator animator;
    Vector3 StartingPosition;
    bool chase;
    float chaseCooldown;
    float cooldownDefault = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartingPosition = transform.position;
        chase = false;
        chaseCooldown = cooldownDefault;

        if (isFloating)
        {
            animator.SetBool("floating", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent)
        {
            animator.SetFloat("velocity", agent.velocity.magnitude);
        }

        transform.LookAt(player.transform);

        if(player.GetComponent<PlayerMovement>().fell)
        {
            transform.eulerAngles = new Vector3 (0.0f, transform.rotation.eulerAngles.y, 0.0f);
        }

        if(chase)
        {
            Vector3 direction = (transform.position - player.transform.position).normalized;
            Vector3 targetPosition = player.transform.position + direction * 7;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2);
        }
        else if(chaseCooldown <= 0.0f)
        {
            transform.position = Vector3.Lerp(transform.position, StartingPosition, Time.deltaTime/2);

            if (transform.position == StartingPosition)
            {
                chaseCooldown = cooldownDefault;
            }

        }
        else
        {
            chaseCooldown -= Time.deltaTime;
        }
  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            chase = true;
            chaseCooldown = cooldownDefault;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chase = false;
        }
    }
}

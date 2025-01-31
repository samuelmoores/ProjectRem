using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    int damages = 0;
    bool dead = false;
    bool euphoriaHasStarted = false;

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

        if(isFloating && !dead)
        {
            transform.LookAt(player.transform);

            if(player.GetComponent<PlayerMovement>().fell)
            {
                transform.eulerAngles = new Vector3 (0.0f, transform.rotation.eulerAngles.y, 0.0f);
            }

            if(chase)
            {
                Vector3 direction = (transform.position - player.transform.position).normalized;
                direction.y = 0.0f; 
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
        
        if(euphoriaHasStarted)
        {
            GameObject.Find("Directional Light").GetComponent<Light>().intensity += Time.deltaTime/20;
        }

  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && isFloating)
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

    public void Damage()
    {
        animator.SetTrigger("damage");
        damages++;

        if(damages == 10)
        {
            dead = true;
            animator.SetBool("dead", true);
        }
    }

    public void PureUtterEuphoria()
    {
        GameObject[] allKnives = FindObjectsOfType<GameObject>()
            .Where(obj => obj.name == "Knife(Clone)")
            .ToArray();

        foreach (GameObject knife in allKnives)
        {
            Rigidbody rb = knife.GetComponent<Rigidbody>();
            rb.freezeRotation = false;
            rb.useGravity = false;
            rb.AddForce(new Vector3(Random.Range(0.2f, 0.9f), 1.0f, Random.Range(0.2f, 0.9f)) * 100.0f, ForceMode.Impulse);
            rb.AddTorque(Vector3.forward * Random.Range(500.0f, 3000.0f), ForceMode.Impulse);
        }

        euphoriaHasStarted = true;
    }

    public bool Alive()
    {
        return !dead;
    }
}

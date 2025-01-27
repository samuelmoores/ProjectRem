using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tom : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Todd");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("velocity", agent.velocity.magnitude);
    }
}

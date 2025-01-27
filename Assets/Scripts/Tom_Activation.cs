using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tom_Activation : MonoBehaviour
{
    public GameObject Tom;
    NavMeshAgent agent;
    GameObject Todd;

    // Start is called before the first frame update
    void Start()
    {
        agent = Tom.GetComponent<NavMeshAgent>();
        Todd = GameObject.Find("Todd");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            agent.destination = GameObject.Find("Todd_Sleep").transform.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class Tom_Activation : MonoBehaviour
{
    public GameObject Tom;
    public GameObject BedRoomLight;
    
    NavMeshAgent agent;
    GameObject Todd;

    // Start is called before the first frame update
    void Start()
    {
        agent = Tom.GetComponent<NavMeshAgent>();
        Todd = GameObject.Find("Todd");
    }

    // Update is called once per frame
    void Update()
    {
        float distance_to_bed = Vector3.Distance(agent.pathEndPosition, Tom.transform.position);

        if(agent.hasPath && distance_to_bed < 2.0f)
        {
            BedRoomLight.GetComponent<Light>().intensity += Time.deltaTime * 3.0f;
            BedRoomLight.GetComponent<Light>().range += Time.deltaTime * 0.00f;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            agent.destination = GameObject.Find("Todd_Sleep").transform.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    Rigidbody rb;
    float dropTimer;
    bool stuckInTree;

    // Start is called before the first frame update
    void Start()
    {
        dropTimer = Random.Range(0.0f, 3.0f);
        rb = GetComponent<Rigidbody>();
        rb.AddForce((transform.up) * Random.Range(-5000, -7000), ForceMode.Impulse);
        stuckInTree = false;


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !stuckInTree)
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().Kill();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        stuckInTree = true;
        rb.excludeLayers = 0;

    }

    public void GetExploded()
    {
        rb.AddForce(GameObject.Find("Player").transform.forward * 6000, ForceMode.Impulse);
    }
}

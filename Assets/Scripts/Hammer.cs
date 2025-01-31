using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public bool pickedUp = false;
    PlayerMovement player;
    float spin = 0.0f;
    int nums;
    float cooldownTimer = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pickedUp)
        {
            spin += Time.deltaTime;
            transform.eulerAngles = new Vector3(0.0f, spin * 40.0f, 0.0f);
        }

        cooldownTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Knife(Clone)" && player.inflictingDamage && cooldownTimer < 0.0f)
        {
            Debug.Log("Hammer hit knife " + nums++ + " times");
            cooldownTimer = 1.0f;
        }
    }

    
}

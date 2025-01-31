using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public bool pickedUp = false;
    float spin = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!pickedUp)
        {
            spin += Time.deltaTime;

            transform.eulerAngles = new Vector3(0.0f, spin * 40.0f, 0.0f);


        }
    }
}

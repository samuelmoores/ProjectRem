using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Animator animator;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool Alive()
    {
        return !dead;
    }

    public void Kill()
    {
        dead = true;
        animator.SetBool("dead", true);
    }
}

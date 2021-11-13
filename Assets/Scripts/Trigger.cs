using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private Animator Animator;
    bool IsTouchingPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }
    private void Update()
    {
       // SwitchController();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsTouchingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            IsTouchingPlayer = false;
        }

    }
}

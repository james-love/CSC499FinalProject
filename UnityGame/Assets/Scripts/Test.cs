using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    private Transform player;
    private bool punch;
    private bool inRange;
    private float speed = 2f;
    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        punch = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (inRange)
        {
            transform.LookAt(player);
            transform.position += transform.forward * speed * Time.deltaTime;
            
            float distToPlayer = Vector3.Distance(transform.position, player.position);
            if (distToPlayer <= 2 && !punch)
            {
                punch = true;
                animator.SetTrigger("Punch");
            }
            else if (distToPlayer > 2 && punch)
            {
                punch = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger enter");
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger exit");
            inRange = false;
        }
    }

    private void Die()
    {
        animator.SetTrigger("Death");
    }
}
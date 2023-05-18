using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class NewTest : MonoBehaviour
{
    private CharacterController _controller;
    private Transform _player;
    [SerializeField]
    private float _speed = 2.5f;
    [SerializeField]
    private float _gravity = 2.0f;
    private Vector3 _velocity;
    private Animator animator;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (_controller.isGrounded == true)
        {
            Vector3 direction = _player.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            transform.localRotation = Quaternion.LookRotation(direction);
            _velocity = direction * _speed;
            animator.SetBool("Running", true);
            Debug.Log("walking");
        }
        _velocity.y -= _gravity;
        _controller.Move(_velocity * Time.deltaTime);
        //Debug.Log("walking");
        //animator.SetBool("Running", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _gravity = 2.0f;
    private CharacterController _controller;
    private Transform _player;
    private Vector3 _velocity;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
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
        }
        _velocity.y -= _gravity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("enemy attack activated");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("enemy attack deactivated");
        }
    }
}
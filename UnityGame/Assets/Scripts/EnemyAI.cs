using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyAI : Enemy
{
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _gravity = 2.0f;
    private CharacterController _controller;
    private Transform _player;
    private Vector3 _velocity;
    public float _maxDistance = 10;
    private float totalHealth = 1f;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, _player.position) < _maxDistance)
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

    public override void Hit(float damageValue)
    {
        totalHealth = Mathf.Clamp(totalHealth - damageValue, 0f, totalHealth);
        if (totalHealth == 0f)
            Destroy(gameObject);
    }
}

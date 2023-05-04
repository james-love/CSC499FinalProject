using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewClimb : MonoBehaviour
{
    public float open = 100f;
    public float range = 0.5f;
    public bool touchingWall = false;
    public float upwardSpeed = 3f;
    public Camera ladderCam;

    private void Update()
    {
        Shoot();

        if (Keyboard.current.cKey.wasPressedThisFrame && touchingWall == true)
        {
            StartCoroutine(StartClimbing());
        }

        if (Keyboard.current.cKey.wasReleasedThisFrame)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            touchingWall = false;
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(ladderCam.transform.position, ladderCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                touchingWall = true;
            }
        }
    }

    IEnumerator StartClimbing()
    {
        transform.position += Vector3.up * Time.deltaTime * upwardSpeed;
        GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(0.85f);
        touchingWall = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
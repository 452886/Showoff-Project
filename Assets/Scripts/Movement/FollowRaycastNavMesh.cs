﻿using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowRaycastNavMesh : MonoBehaviour
{
    [SerializeField]
    private float minRotateSpeed = 5;
    [SerializeField]
    private float maxRotateSpeed = 50;

    public Camera mainCamera;
    private NavMeshAgent agent;

    private int layerMask;
    private int groundMask;
    private bool isMoving = false;

    float distanceWalked = 0.0f;

    Vector3 autoMoveTarget;


    float timer = 0;
    float tapDelay = 1.00f;     // Default delay for registering a tap as a gesture is 1/10 of a second .

    public bool reverseDirection = false;
    private bool controls = true;
    public float DistanceWalked { get => distanceWalked; }
    float orinalSpeed;
    public bool IsMoving { get => isMoving; }

    void OnEnable() { LeanTouch.OnGesture += handleFingerGesture; }
    void OnDisable() { LeanTouch.OnGesture -= handleFingerGesture; }
    void Start()
    {
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        layerMask = LayerMask.GetMask("Obstacles");
        groundMask = LayerMask.GetMask("RaycastGround");

        orinalSpeed = agent.speed;
    }

    void FixedUpdate()
    {
        if (controls)
        {
            if ((LeanTouch.Fingers.Count == 0))
                stop();
        }
        else if (Vector3.Distance(autoMoveTarget, transform.position) < 1.0f)
            isMoving = false;
    }

    public void DisableInput()
    {
        OnDisable();
        controls = false;
    }
    public void SetDestination(Vector3 destination)
    {
        autoMoveTarget = destination;
        DisableInput();
        isMoving = true;
        agent.SetDestination(destination);
    }

    public void handleFingerGesture(List<LeanFinger> fingers)
    {
        if (timer < tapDelay)
            timer += Time.deltaTime * 10.0f;
        else
        {
            LeanFinger finger = fingers[0];
            if (finger != null)
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(finger.ScreenPosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
                {
                    if (hit.collider != null)
                        MoveTowardsTarget(hit.point);
                }
            }
        }
    }

    public float GetAgentSpeed()
    {
        if (isMoving)
            return agent.speed;
        else
            return 0;
    }

    private void LerpRotateTowardsTarget(Vector3 delta)
    {
        Quaternion rotation = Quaternion.LookRotation(delta);

        if (reverseDirection)
        {
            Vector3 rot = rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            rotation = Quaternion.Euler(rot);
        }

        float rotateSpeed = maxRotateSpeed - delta.magnitude;
        rotateSpeed = Mathf.Clamp(rotateSpeed, minRotateSpeed, maxRotateSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }

    public void RotateTowardsTarget(Vector3 delta)
    {
        Quaternion rotation = Quaternion.LookRotation(delta);
        transform.rotation = rotation;
    }

    public void MoveTowardsTarget(Vector3 target)
    {
        isMoving = true;

        Vector3 delta = target - transform.position;
        LerpRotateTowardsTarget(delta);

        if (delta.magnitude > 0.5f)
        {
            delta.Normalize();

            if (reverseDirection)
                agent.Move(-transform.forward * Time.deltaTime * agent.speed);
            else if (!reverseDirection)
                agent.Move(transform.forward * Time.deltaTime * agent.speed);

            distanceWalked += (transform.forward * Time.deltaTime * agent.speed).magnitude;
        }
    }

    public void ScaleSpeed(float scalar, float time)
    {

        agent.speed = orinalSpeed * scalar;
        StartCoroutine(scaleSpeedTimer(time, orinalSpeed));
    }

    IEnumerator scaleSpeedTimer(float time, float originalSpeed)
    {
        yield return new WaitForSeconds(time);
        agent.speed = originalSpeed;
    }

    private void stop()
    {
        isMoving = false;
        timer = 0;
    }

}

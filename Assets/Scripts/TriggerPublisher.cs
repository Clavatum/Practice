using System;
using Assets.Scripts.Movement;
using UnityEngine;

public class TriggerPublisher : MonoBehaviour
{
    private MovementMediator movementMediator;

    [SerializeField] private string areaName;
    [SerializeField] private AudioClip areaClip;

    void Awake()
    {
        movementMediator = FindAnyObjectByType<MovementMediator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovementController>() == null) { return; }
        movementMediator.TriggerEnteredEffectiveArea(areaName, areaClip);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovementController>() == null) { return; }
        movementMediator.TriggerExitedEffectiveArea(areaName, areaClip);
    }
}
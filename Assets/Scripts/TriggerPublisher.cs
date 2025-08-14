using System;
using Assets.Scripts.Movement;
using UnityEngine;

public class TriggerPublisher : MonoBehaviour
{
    private Mediator mediator;

    [SerializeField] private string areaName;
    [SerializeField] private AudioClip areaClip;
    [SerializeField] private Vector3 targetScale;
    [SerializeField] private CapsuleCollider targetCapsuleCollider;

    void Awake()
    {
        mediator = FindAnyObjectByType<Mediator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovementController>() == null) { return; }
        mediator.TriggerEnteredEffectiveArea(areaName, areaClip, targetScale, targetCapsuleCollider);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovementController>() == null) { return; }
        mediator.TriggerExitedEffectiveArea(areaName, areaClip, targetScale, targetCapsuleCollider);
    }
}
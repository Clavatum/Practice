using Assets.Scripts.Movement;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private Mediator mediator;

    [SerializeField] private AudioSource audioSource;
    private AudioClip audioClip;

    void Awake()
    {
        mediator = FindAnyObjectByType<Mediator>();
    }

    private void PlaySound(AudioClip audioClip)
    {
        this.audioClip = audioClip;
        audioSource.PlayOneShot(audioClip);
    }

    void OnEnable()
    {
        mediator.OnEnteredEffectiveArea += (sender, args) => { PlaySound(args.AudioClip); };
    }

    void OnDisable()
    {
        mediator.OnEnteredEffectiveArea -= (sender, args) => { PlaySound(args.AudioClip); };
    }
}
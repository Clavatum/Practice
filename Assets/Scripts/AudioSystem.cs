using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private Mediator mediator;

    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        mediator = FindAnyObjectByType<Mediator>();
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    void OnEnable()
    {
        mediator.OnEnteredEffectiveArea += (sender, args) => { PlaySound(args.AudioClip); };
        mediator.OnExitedEffectiveArea += (sender, args) => PlaySound(args.AudioClip);
    }

    void OnDisable()
    {
        mediator.OnEnteredEffectiveArea -= (sender, args) => { PlaySound(args.AudioClip); };
        mediator.OnExitedEffectiveArea -= (sender, args) => PlaySound(args.AudioClip);
    }
}
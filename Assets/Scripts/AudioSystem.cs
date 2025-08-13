using Assets.Scripts.Movement;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private MovementMediator movementMediator;

    [SerializeField] private AudioSource audioSource;
    private AudioClip audioClip;

    void Awake()
    {
        movementMediator = FindAnyObjectByType<MovementMediator>();
    }

    private void PlaySound(AudioClip audioClip)
    {
        this.audioClip = audioClip;
        audioSource.PlayOneShot(audioClip);
    }

    void OnEnable()
    {
        movementMediator.OnEnteredEffectiveArea += (sender, args) => { PlaySound(args.AudioClip); };
    }

    void OnDisable()
    {
        movementMediator.OnEnteredEffectiveArea -= (sender, args) => { PlaySound(args.AudioClip); };
    }
}
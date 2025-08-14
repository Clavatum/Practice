using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private Mediator mediator;

    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        mediator = FindAnyObjectByType<Mediator>();
    }

    private void PlaySound(object sender, EffectiveAreaEventArgs args)
    {
        audioSource.PlayOneShot(args.AudioClip);
    }

    void OnEnable()
    {
        mediator.OnEnteredEffectiveArea += PlaySound;
        mediator.OnExitedEffectiveArea += PlaySound;
    }

    void OnDisable()
    {
        mediator.OnEnteredEffectiveArea -= PlaySound;
        mediator.OnExitedEffectiveArea -= PlaySound;
    }
}
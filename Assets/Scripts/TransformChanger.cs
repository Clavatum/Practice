using UnityEngine;
using System.Collections;

public class TransformChanger : MonoBehaviour
{
    private Mediator mediator;
    private TransformMemento originalTransformMemento;
    private Coroutine scaleRoutine;

    [SerializeField] private float scaleDuration = 0.5f;

    void Awake()
    {
        mediator = FindAnyObjectByType<Mediator>();
    }

    public void ChangeScale(object sender, EffectiveAreaEventArgs args)
    {
        if (originalTransformMemento == null)
        {
            originalTransformMemento = new TransformMemento(transform.localScale, transform.localPosition);
        }

        StartScaleRoutine(args.TargetScale);
    }

    public void RestoreScale(object sender, EffectiveAreaEventArgs args)
    {
        if (originalTransformMemento != null)
        {
            StartScaleRoutine(originalTransformMemento.Scale);
            originalTransformMemento = null;
        }
    }

    private void StartScaleRoutine(Vector3 targetScale)
    {
        if (scaleRoutine != null) StopCoroutine(scaleRoutine);
        scaleRoutine = StartCoroutine(SmoothScale(targetScale));
    }

    private IEnumerator SmoothScale(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / scaleDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }

    void OnEnable()
    {
        mediator.OnEnteredEffectiveArea += ChangeScale;
        mediator.OnExitedEffectiveArea += RestoreScale;
    }

    void OnDisable()
    {
        mediator.OnEnteredEffectiveArea -= ChangeScale;
        mediator.OnExitedEffectiveArea -= RestoreScale;
    }
}
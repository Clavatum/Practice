using UnityEngine;
using System.Collections;

public class TransformChanger : MonoBehaviour
{
    private Mediator mediator;
    private TransformMemento originalTransformMemento;
    private Coroutine scaleRoutine;

    [SerializeField] private CapsuleCollider originalCapsuleCollider;
    [SerializeField] private float scaleDuration = 0.5f;

    void Awake()
    {
        mediator = FindAnyObjectByType<Mediator>();
    }

    public void ChangeScale(object sender, EffectiveAreaEventArgs args)
    {
        if (originalTransformMemento == null)
        {
            originalTransformMemento = new TransformMemento(transform.localScale, transform.localPosition, args.TargetCapsuleCollider);
        }

        StartScaleRoutine(args.TargetScale, args.TargetCapsuleCollider);
    }

    public void RestoreScale(object sender, EffectiveAreaEventArgs args)
    {
        if (originalTransformMemento != null)
        {
            StartScaleRoutine(originalTransformMemento.Scale, originalCapsuleCollider);
            originalTransformMemento = null;
        }
    }

    private void StartScaleRoutine(Vector3 targetScale, CapsuleCollider targetCapsuleCollider)
    {
        if (scaleRoutine != null) StopCoroutine(scaleRoutine);
        scaleRoutine = StartCoroutine(SmoothScale(targetScale, targetCapsuleCollider));
    }

    private IEnumerator SmoothScale(Vector3 targetScale, CapsuleCollider targetCapsuleCollider)
    {
        CapsuleCollider originalCapsuleCollider = this.originalCapsuleCollider;
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;
        float speed = 0f;

        while (elapsed < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / scaleDuration);
            originalCapsuleCollider.center = Vector3.Lerp(originalCapsuleCollider.center, targetCapsuleCollider.center, elapsed / scaleDuration);
            originalCapsuleCollider.radius = Mathf.SmoothDamp(originalCapsuleCollider.radius, targetCapsuleCollider.radius, ref speed, 10f);
            originalCapsuleCollider.height = Mathf.SmoothDamp(originalCapsuleCollider.height, targetCapsuleCollider.height, ref speed, 10f);
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
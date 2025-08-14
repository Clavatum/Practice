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

    public void ChangeScale(Vector3 targetScale, CapsuleCollider targetCapsuleCollider)
    {
        if (originalTransformMemento == null)
        {
            originalTransformMemento = new TransformMemento(transform.localScale, transform.localPosition, targetCapsuleCollider);
        }

        StartScaleRoutine(targetScale, targetCapsuleCollider);
    }

    public void RestoreScale()
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
        mediator.OnEnteredEffectiveArea += (sender, args) => { ChangeScale(args.TargetScale, args.TargetCapsuleCollider); };
        mediator.OnExitedEffectiveArea += (sender, args) => { RestoreScale(); };
    }

    void OnDisable()
    {
        mediator.OnEnteredEffectiveArea -= (sender, args) => { ChangeScale(args.TargetScale, args.TargetCapsuleCollider); };
        mediator.OnExitedEffectiveArea -= (sender, args) => { RestoreScale(); };
    }
}
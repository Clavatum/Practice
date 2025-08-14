using TMPro;
using UnityEngine;

public class FeedbackSystem : MonoBehaviour
{
    private Mediator mediator;

    [SerializeField] private TextMeshProUGUI feedbackText;

    void Awake()
    {
        mediator = FindAnyObjectByType<Mediator>();
    }

    public void SetFeedback(object sender, EffectiveAreaEventArgs args)
    {
        feedbackText.text = $"You have entered an area makes you {args.AreaName}!";
    }

    public void ResetFeedback(object sender, EffectiveAreaEventArgs args)
    {
        feedbackText.text = "";
    }

    void OnEnable()
    {
        mediator.OnEnteredEffectiveArea += SetFeedback;
        mediator.OnExitedEffectiveArea += ResetFeedback;
    }

    void OnDisable()
    {
        mediator.OnEnteredEffectiveArea -= SetFeedback;
        mediator.OnExitedEffectiveArea -= ResetFeedback;
    }
}
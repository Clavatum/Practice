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

    public void SetFeedback(string areaName)
    {
        feedbackText.text = $"You have entered an area makes you {areaName}!";
    }

    public void ResetFeedback()
    {
        feedbackText.text = "";
    }

    void OnEnable()
    {
        mediator.OnEnteredEffectiveArea += (sender, args) => { SetFeedback(args.AreaName); };
        mediator.OnExitedEffectiveArea += (sender, args) => { ResetFeedback(); };
    }

    void OnDisable()
    {
        mediator.OnEnteredEffectiveArea -= (sender, args) => { SetFeedback(args.AreaName); };
        mediator.OnExitedEffectiveArea -= (sender, args) => { ResetFeedback(); };
    }
}
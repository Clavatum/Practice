using TMPro;
using UnityEngine;

public class FeedbackSystem : MonoBehaviour
{
    private Mediator mediator;

    [SerializeField] private TextMeshProUGUI feedbackText;
    private string areaName;

    void Awake()
    {
        mediator = FindAnyObjectByType<Mediator>();
    }

    public void Feedback(string areaName)
    {
        this.areaName = areaName;
        feedbackText.text = $"You have entered an area makes you {areaName}!";
    }

    void OnEnable()
    {
        mediator.OnEnteredEffectiveArea += (sender, args) => { Feedback(args.AreaName); };
    }

    void OnDisable()
    {
        mediator.OnEnteredEffectiveArea -= (sender, args) => { Feedback(args.AreaName); };
    }
}
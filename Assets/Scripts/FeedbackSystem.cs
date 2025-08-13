using Assets.Scripts.Movement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackSystem : MonoBehaviour
{
    private MovementMediator movementMediator;

    [SerializeField] private TextMeshProUGUI feedbackText;
    private string areaName;

    void Awake()
    {
        movementMediator = FindAnyObjectByType<MovementMediator>();
    }

    public void Feedback(string areaName)
    {
        this.areaName = areaName;
        feedbackText.text = $"You have entered an area makes you {areaName}!";
    }

    void OnEnable()
    {
        movementMediator.OnEnteredEffectiveArea += (sender, args) => { Feedback(args.AreaName); };
    }

    void OnDisable()
    {
        movementMediator.OnEnteredEffectiveArea -= (sender, args) => { Feedback(args.AreaName); };
    }
}
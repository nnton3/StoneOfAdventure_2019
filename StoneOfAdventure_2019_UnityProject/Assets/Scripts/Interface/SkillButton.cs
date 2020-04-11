using UnityEngine;
using UnityEngine.UI;
using StoneOfAdventure.Combat;
using Zenject;

public class SkillButton : MonoBehaviour
{
    [Header("OBJECTS")]
    [SerializeField] private Transform loadingBar;
    [SerializeField] private SkillBase targetSkill;

    [Header("VARIABLES (IN-GAME)")]
    [Range(0, 100)] public float currentPercent;
    [Range(0, 100)] public int speed;

    [Inject] private SignalBus signalBus;

    private void Start()
    {
        speed = (int)(100 / targetSkill.CoolDown);
        targetSkill.SkillUsed.AddListener(ResetSkill);
        signalBus.Subscribe<BrokenClockTriggered>(ReduceCoolDown);
    }

    void FixedUpdate()
    {
        if (currentPercent >= 100)
        {
            loadingBar.GetComponent<Image>().fillAmount = 1f;
            return;
        }

        currentPercent += speed * Time.deltaTime;

        loadingBar.GetComponent<Image>().fillAmount = currentPercent / 100;
    }

    private void ResetSkill() { currentPercent = 0; }

    private void ReduceCoolDown(BrokenClockTriggered args)
    {
        currentPercent += (speed * args.reducedTime);
    }
}

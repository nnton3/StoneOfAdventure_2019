using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [Header("OBJECTS")]
    [SerializeField] private Transform loadingBar;
    [SerializeField] private SkillBase targetSkill;

    [Header("VARIABLES (IN-GAME)")]
    [Range(0, 100)] public float currentPercent;
    [Range(0, 100)] public int speed;


    private void Start()
    {
        speed = (int)(100 / targetSkill.CoolDown);
        targetSkill.SkillUsed.AddListener(ResetSkill);
    }

    void Update()
    {
        if (currentPercent >= 100) return;

        currentPercent += speed * Time.deltaTime;

        loadingBar.GetComponent<Image>().fillAmount = currentPercent / 100;
    }

    private void ResetSkill() { currentPercent = 0; }
}

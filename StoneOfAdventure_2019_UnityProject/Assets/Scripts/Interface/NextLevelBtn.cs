using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NextLevelBtn : MonoBehaviour
{
    private Button btn;
    private GameObject interactiveText;
    [Inject] readonly SignalBus signalBus;

    void Start()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(() =>
        {
            signalBus.Fire<PlayerStartNextLevel>();
            DisableBtn();
        });
        signalBus.Subscribe<LocationCompletedSignal>(EnableBtn);
        interactiveText = GetComponentInChildren<Text>().gameObject;
        DisableBtn();
    }

    private void EnableBtn()
    {
        interactiveText.SetActive(true);
        btn.interactable = true;
    }

    private void DisableBtn()
    {
        interactiveText.SetActive(false);
        btn.interactable = false;
    }
}

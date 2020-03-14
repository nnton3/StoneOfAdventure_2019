using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class NextLevelBtn : MonoBehaviour
{
    private Button btn;
    private GameObject child;
    private TransitionAnimStarter loadScreen;
    [Inject] readonly SignalBus signalBus;

    [HideInInspector] public UnityEvent PlayerStartNextLevel;

    void Start()
    {
        btn = GetComponent<Button>();
        loadScreen = FindObjectOfType<TransitionAnimStarter>();

        btn.onClick.AddListener(() =>
        {
            PlayerStartNextLevel?.Invoke();
            DisableBtn();
        });
        Debug.Log(signalBus == null);
        signalBus.Subscribe<LocationCompletedSignal>(EnableBtn);
        child = transform.GetChild(0).gameObject;
        DisableBtn();
    }

    private void EnableBtn()
    {
        child.SetActive(true);
        btn.interactable = true;
    }

    private void DisableBtn()
    {
        child.SetActive(false);
        btn.interactable = false;
    }

    private void OnDestroy()
    {
        PlayerStartNextLevel.RemoveAllListeners();
    }
}

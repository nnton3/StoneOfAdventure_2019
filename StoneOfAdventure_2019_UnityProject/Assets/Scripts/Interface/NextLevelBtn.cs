using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NextLevelBtn : MonoBehaviour
{
    private Button btn;
    private GameObject child;
    private TransitionAnimStarter loadScreen;
    private LocationPointsStorage storage;

    [HideInInspector] public UnityEvent PlayerStartNextLevel;

    void Start()
    {
        btn = GetComponent<Button>();
        loadScreen = FindObjectOfType<TransitionAnimStarter>();
        storage = FindObjectOfType<LocationPointsStorage>();

        btn.onClick.AddListener(() =>
        {
            PlayerStartNextLevel?.Invoke();
            DisableBtn();
        });
        storage.LocationCompleted.AddListener(EnableBtn);
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

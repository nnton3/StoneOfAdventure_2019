using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NextLevelBtn : MonoBehaviour
{
    private Button btn;
    private TransitionAnimStarter loadScreen;
    private LocationPointsStorage storage;

    [HideInInspector] public UnityEvent PlayerStartNextLevel;

    void Start()
    {
        btn = GetComponent<Button>();
        loadScreen = FindObjectOfType<TransitionAnimStarter>();
        storage = FindObjectOfType<LocationPointsStorage>();

        btn.onClick.AddListener(() => PlayerStartNextLevel?.Invoke());
        btn.enabled = false;
        storage.LocationCompleted.AddListener(EnableBtn);
    }

    private void EnableBtn()
    {
        btn.enabled = true;
    }

    private void DisableBtn()
    {
        btn.enabled = false;
    }

    private void OnDisable()
    {
        PlayerStartNextLevel.RemoveAllListeners();
    }
}

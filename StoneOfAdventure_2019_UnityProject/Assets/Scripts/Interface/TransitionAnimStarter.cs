using UnityEngine;
using Zenject;

public class TransitionAnimStarter : MonoBehaviour
{
    #region Variables
    private Animator anim;
    [Inject] private SignalBus signalBus;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();

        signalBus.Subscribe<PlayerStartNextLevel>(StartAnimation);
    }

    private void StartAnimation()
    {
        anim.SetTrigger("start");
    }
}

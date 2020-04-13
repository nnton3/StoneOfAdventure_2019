using UnityEngine;
using Zenject;

namespace StoneOfAdventure.UI
{
    public class UIController : MonoBehaviour
    {
        [Inject] private ArtifactSelector artifactSelector;

        internal void ShowArtifactSelector()
        {
            artifactSelector.OpenArtifactSelector();
        }
    }
}

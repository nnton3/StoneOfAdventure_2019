﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoneOfAdventure.Artifacts;
using UnityEngine.UI;

namespace StoneOfAdventure.UI
{
    public class ArtifactSelector : MonoBehaviour
    {
        [SerializeField] private GameObject[] artifacts;
        [SerializeField] private List<GameObject> selectedArtifacts;
        private Animator anim;
        [SerializeField] private GameObject bck;

        private void Start()
        {
            anim = bck.GetComponentInChildren<Animator>();
        }

        public void EnableArtifactSelector()
        {
            bck.SetActive(true);
            ShowArtifacts(SelectArtifacts());
        }

        private void ShowArtifacts(List<GameObject> prefs)
        {
            for (int i = 0; i < prefs.Count; i++)
            {
                var artifact = Instantiate(prefs[i], bck.transform);
                selectedArtifacts[i] = artifact;
            }
        }

        public void CloseArtifactSelector(GameObject selectedArtifact)
        {
            selectedArtifacts.Remove(selectedArtifact);
            StartCoroutine("Hide");
        }

        IEnumerator Hide()
        {
            for (int j = 0; j < selectedArtifacts.Count; j++)
            {
                selectedArtifacts[j].GetComponent<Artifact>().Hide();
            }
            yield return new WaitForSeconds(1f);
            anim.SetTrigger("action");
            yield return new WaitForSeconds(1f);
            foreach (var art in GetComponentsInChildren<Artifact>())
            {
                Destroy(art.gameObject);
            }
        }

        private List<GameObject> SelectArtifacts()
        {
            selectedArtifacts = new List<GameObject>()
            {
                artifacts[Random.Range(0, artifacts.Length - 1)],
                artifacts[Random.Range(0, artifacts.Length - 1)],
                artifacts[Random.Range(0, artifacts.Length - 1)]
            };

            return selectedArtifacts;
        }
    }
}

using UnityEngine;
using System.Collections;

namespace StoneOfAdventure.Artifacts
{
    public class ArtifactsController : MonoBehaviour
    {
        private Hashtable playerArts = new Hashtable();

        /// <summary>
        /// Add new art or increase params of old art
        /// </summary>
        /// <param name="addedArt"> Key for arts hashtable</param>
        /// <returns>current art lvl</returns>
        public void AddArt(object addedArt)
        {
            if (playerArts.ContainsKey(addedArt))
            {
                playerArts[addedArt] = (int) playerArts[addedArt] + 1;
            }
            else
                playerArts.Add(addedArt, 1);
        }

        public int GetArtLvl(object art)
        {
            return (int)playerArts[art];
        }
    }
}

using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using GameClient.Saving;

namespace GameClient.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        enum DestinationIdentifier
        {
            A, B, C, D, E
        }
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;


        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Player")
            {
                print("Portal Triggered");
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                Debug.LogError("Scene to load is not set");
                yield break;
            }
            DontDestroyOnLoad(gameObject); // Nonsense - was adding more objects to scene
            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

            yield return fader.FadeOut(fadeOutTime);

            savingWrapper.Save();



            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            savingWrapper.Load();

            
            print("Scene loaded");
            Portal otherPortal = GetOtherPortal();
            print("Portal other portal");
            UpdatePlayer(otherPortal);
            print("Player updated");   
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            yield return new WaitForSeconds(fadeWaitTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            // player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position; // if there is no mesh agent, can be used in future.
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
            
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{


    public class Portal : MonoBehaviour
    {
        public static int Keys;
        public Component Doorcollider;


        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;

        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Player")
            {
                StartCoroutine(Transition());


            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;

            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newPlayerController.enabled = false;

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            fader.FadeIn(fadeInTime);

            newPlayerController.enabled = true;

            Destroy(gameObject);

        }


        private void UpdatePlayer(Portal otherPortal)
        {
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = otherPortal.spawnPoint.position;
                player.transform.rotation = otherPortal.spawnPoint.rotation;
                player.GetComponent<NavMeshAgent>().enabled = true;
            }
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;

                return portal;
            }

            return null;
        }
    }
}

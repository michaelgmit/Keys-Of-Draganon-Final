﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{

    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentActiveFade = null;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            DontDestroyOnLoad(gameObject);
        }
        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            print("Faded Out");
            yield return FadeIn(3f);
            print("Faded In");
        }



        public Coroutine FadeOut(float time)
        {
            //alpha is not 1
            
                return Fade(1, time);
            }

        public Coroutine FadeIn(float time)
        {
                return Fade(0, time);
            }

        public Coroutine Fade(float target, float time)

        {
            if (currentActiveFade != null)
                {
                    StopCoroutine(currentActiveFade);
                }
                currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentActiveFade;
        }
        
        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            { //continue move towards raycast target after fade begins
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }

}
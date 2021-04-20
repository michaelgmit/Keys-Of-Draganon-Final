using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experiencePoints = 0;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;

            if(experiencePoints > 400)
            {
 
                SceneManager.LoadScene("WonGame");
            }
        }

        public float GetPoints()
        {
            return experiencePoints;
        }
    }
}

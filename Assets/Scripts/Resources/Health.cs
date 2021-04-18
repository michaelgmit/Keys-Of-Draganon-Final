using UnityEngine;
using RPG.Stats;
using RPG.Core;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace RPG.Resources
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        [SerializeField] UnityEvent takeDamage;
        [SerializeField] UnityEvent onDie;


        bool isDead = false;

        //stats relation
        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage) //keeps track of damage taken - current health
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                onDie.Invoke();
                Die(); //calls the die function when health = 0
                AwardExperience(instigator);
                SceneManager.LoadScene("gameover");
            }
            else
            {
                takeDamage.Invoke();
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public void Heal(float healthToRestore)
        {
            healthPoints = Mathf.Min(healthPoints + healthToRestore);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true; //checks bool Line 11 for death status 
            GetComponent<Animator>().SetTrigger("die"); // if dead animation is triggered
            GetComponent<ActionSchedular>().CancelCurrentAction(); // anything that was running should know to stop running
        }
    }
}
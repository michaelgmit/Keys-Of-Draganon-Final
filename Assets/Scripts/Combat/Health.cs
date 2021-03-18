using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage) //keeps track of damage taken - current health
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die(); //calls the die function when health = 0
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true; //checks bool Line 11 for death status 
            GetComponent<Animator>().SetTrigger("die"); // if dead animation is triggered
        }
    }
}
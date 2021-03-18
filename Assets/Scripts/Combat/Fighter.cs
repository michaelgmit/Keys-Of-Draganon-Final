﻿using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction //nned to implement IAction interface for code to obey its contract i.e. Cancel
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f; //adds a pause between animations
        [SerializeField] float weaponDamage = 5f;

        Health target;
        float timeSinceLastAttack = 0; 

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return; //if theres no target selected code dosent run
            if (target.IsDead()) return; //returns bool from health script

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform); //makes the direction of player face the targeted location or object
            if (timeSinceLastAttack > timeBetweenAttacks) //triggers attack animatoion every time the "timebetween attacks" has elapsed
            {
                GetComponent<Animator>().SetTrigger("attack");
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange; //check if distance is greater than attack range
        }
        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionSchedular>().StartAction(this); //puts action into the schedular
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel() //part of the cancel lock on enemys
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        // Hit is part of the animator
        void Hit() // calls attack animation-animator
        {
            if (target == null) { return; }
            target.TakeDamage(weaponDamage);
        }
    }
}
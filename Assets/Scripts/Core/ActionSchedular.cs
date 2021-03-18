using UnityEngine;

namespace RPG.Core
{
    public class ActionSchedular : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return; //sets a new click to cancel a current action
            if (currentAction != null) //checks to see if current action is null
            {

                currentAction.Cancel();
            }
            currentAction = action; //sets the current action to new action
        }
    }
}
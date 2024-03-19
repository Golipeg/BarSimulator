using UnityEngine;

namespace Player
{
    public class TakeMoneyState : IPlayerState
    {
        public void Enter(Player player)
        {
            Debug.Log("Entering TakeMoney state");
        }

        public void Update(Player player)
        {
            // Логика забора денег
        }

        public void Exit(Player player)
        {
            Debug.Log("Exiting TakeMoney state");
        }
    }
}
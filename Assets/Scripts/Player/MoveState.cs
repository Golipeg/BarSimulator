
using UnityEngine;

namespace Player
{
    public class MoveState : IPlayerState
    {
        private float _speed;

        public MoveState(float speed)
        {
            _speed = speed;
        }
        public void Enter(Player player)
        {
            Debug.Log("Entering Move state");
        }

        public void Update(Player player)
        {
            Vector3 movement = player.InputController.GetDirection;

            if (movement != Vector3.zero)
            {
                player.transform.Translate(movement * _speed * Time.deltaTime, Space.World);
                
                Quaternion lookRotation = Quaternion.LookRotation(movement, Vector3.up);
                player.transform.rotation = lookRotation;
            }
        }

        public void Exit(Player player)
        {
            Debug.Log("Exiting Move state");
        }
    }
}
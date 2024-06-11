using UnityEngine;

namespace Enemy.Sensors
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerSensor : MonoBehaviour
    {
        public CapsuleCollider2D Collider;
        public delegate void PlayerEnterEvent(Transform player);
        public delegate void PlayerExitEvent(Vector3 lastKnownPosition);

        public event PlayerEnterEvent OnPlayerEnter;
        public event PlayerExitEvent OnPlayerExit;

        private void Awake()
        {
            Collider = GetComponent<CapsuleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                OnPlayerEnter?.Invoke(player.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                OnPlayerExit?.Invoke(other.transform.position);
            }
        }  
    }
}
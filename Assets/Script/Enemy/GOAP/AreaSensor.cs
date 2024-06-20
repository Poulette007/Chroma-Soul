using Enemy.GOAP.Behaviors;
using UnityEngine;

namespace Enemy.Sensors
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class AreaSensor : MonoBehaviour
    {
        public CapsuleCollider2D Collider;
        public delegate void PlayerEnterAreaEvent(Transform bot);
        public delegate void PlayerExitAreaEvent(Vector3 lastKnownPosition);

        public event PlayerEnterAreaEvent OnPlayerEnter;
        public event PlayerExitAreaEvent OnPlayerExit;

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
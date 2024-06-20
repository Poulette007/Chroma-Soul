using Enemy.GOAP.Behaviors;
using UnityEngine;

namespace Enemy.Sensors
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class BossSensor : MonoBehaviour
    {
        public CapsuleCollider2D Collider;
        public delegate void BotEnterEvent(Transform bot);
        //public delegate void BotExitEvent(Vector3 lastKnownPosition);

        public event BotEnterEvent OnBotDetected;
        //public event BotExitEvent OnBotDetected;

        private void Awake()
        {
            Collider = GetComponent<CapsuleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out OrcBrain bot))
            {
                Debug.Log("Boss sensor -- enter ");
                OnBotDetected?.Invoke(bot.transform);
            }
        }
    }
}
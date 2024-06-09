using CrashKonijn.Goap.Behaviours;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy.GOAP.Behaviors
{
    public class GoapSetBinder : MonoBehaviour
    {
        [SerializeField] private GoapRunnerBehaviour GoapRunner;

        private void Awake()
        {
            AgentBehaviour agent = GetComponent<AgentBehaviour>();
            agent.GoapSet = GoapRunner.GetGoapSet("Orc");
        }
    }
}
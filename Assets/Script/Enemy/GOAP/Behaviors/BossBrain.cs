using System;
using System.Collections.Generic;
using CrashKonijn.Goap.Behaviours;
using Enemy.GOAP.Actions;
using Enemy.GOAP.Config;
using Enemy.GOAP.Goals;
using Enemy.GOAP.Sensors;
using Enemy.Sensors;
using UnityEngine;

namespace Enemy.GOAP.Behaviors
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class BossBrain : MonoBehaviour, IInjectable
    {
        [SerializeField] private BossSensor bossSensor;
        private AgentBehaviour agentBehaviour;
        [SerializeField] private BossConfigSO bossConfig;
        private List<OrcBrain> detectedBots = new List<OrcBrain>();

        private void Awake()
        {
            agentBehaviour = GetComponent<AgentBehaviour>();
        }
        private void Start()
        {
            if (bossSensor != null)
            {
                bossSensor.OnBotDetected += BossEntityDetected;
            }
            else
            {
                Debug.LogError("BossEntityDetectorSensor not assigned!");
            }
        }
        private void OnDestroy()
        {
            if (bossSensor != null)
            {
                bossSensor.OnBotDetected -= BossEntityDetected;
            }
        }

        private void BossEntityDetected(Transform botTransform)
        {
            Debug.Log("Boss detected a bot: " + botTransform.name);
            
            //bossConfig.detectableLayerMask;
            OrcBrain orcBrain = botTransform.GetComponent<OrcBrain>();
            if (orcBrain != null)
            {
                orcBrain.OnBossDetected();
            }
        }

        public void Inject(DependencyInjector injector)
        {
            bossConfig = injector.bossConfig;
        }
    }
}

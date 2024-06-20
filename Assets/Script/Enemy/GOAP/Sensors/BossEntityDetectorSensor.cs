using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Enemy.GOAP.Config;
using UnityEngine;

namespace Enemy.GOAP.Sensors 
{
    public class BossEntityDetectorSensor : LocalTargetSensorBase, IInjectable
    {
        private BossConfigSO bossConfig;
        private Collider2D[] collider = new Collider2D[1];
        public delegate void BotDetectedDelegate(Transform botTransform);
        public event BotDetectedDelegate OnBotDetected;
        public override void Created() {}
        public override void Update() {}

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            if (Physics2D.OverlapCircleNonAlloc(agent.transform.position, bossConfig.sensorRadius,
                collider, bossConfig.detectableLayerMask) > 0)
                {
                    if (bossConfig.currentDetectedBots < bossConfig.maxDetectedBots)
                    {
                        Debug.Log("Boss entity Sensor enter add bot");
                        bossConfig.currentDetectedBots++;

                        OnBotDetected?.Invoke(collider[0].transform);
                    }
                }
                return null;
        }

        public void Inject(DependencyInjector injector)
        {
            bossConfig = injector.bossConfig;
        }
    }

}

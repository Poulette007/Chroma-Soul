using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Enemy.GOAP.Config;
using UnityEngine;

namespace Enemy.GOAP.Sensors
{
    public class WanderTargetSensor : LocalTargetSensorBase, IInjectable
    {
        private BotActionConfigSO botActionConfig;
        public override void Created() {}
        public override void Update() {}

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            Vector2 position = GetRandomDirection(agent); 
            // Debug.Log("WanderTargetSensor -- Sense ");
            return new PositionTarget(position);
        }
        private Vector2 GetRandomDirection(IMonoAgent agent)
        {
            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            Vector2 randomPosition = (Vector2)agent.transform.position + direction * botActionConfig.WanderRadius;
            return randomPosition;
        }

        public void Inject(DependencyInjector injector)
        {
            botActionConfig = injector.botActionConfig;
        }
    }
}
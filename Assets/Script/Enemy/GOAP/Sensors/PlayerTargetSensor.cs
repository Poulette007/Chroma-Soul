using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Enemy.GOAP.Config;
using UnityEngine;

namespace Enemy.GOAP.Sensors 
{
    public class PlayerTargetSensor : LocalTargetSensorBase, IInjectable
    {
        private BotActionConfigSO attackConfig;
        private Collider2D[] collider = new Collider2D[1];
        public override void Created() {}
        public override void Update() {}

        public void Inject(DependencyInjector injector)
        {
            attackConfig = injector.botActionConfig;
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            Debug.Log("KillPlayer -- Sense");
            if (Physics2D.OverlapCircleNonAlloc(agent.transform.position, attackConfig.sensorRadius,
             collider, attackConfig.AttackableLayerMask) > 0) {
                return new TransformTarget(collider[0].transform);
            }
            return null;
        }
    }
}
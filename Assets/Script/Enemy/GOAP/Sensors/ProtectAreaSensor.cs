using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Enemy.GOAP.Config;
using UnityEngine;

namespace Enemy.GOAP.Sensors 
{
    public class ProtectAreaSensor : LocalTargetSensorBase, IInjectable
    {
        BotActionConfigSO botActionConfig;

        private Collider2D[] collider = new Collider2D[1];
        public override void Created() {}
        public override void Update() {}

        public void Inject(DependencyInjector injector)
        {  
            botActionConfig = injector.botActionConfig;
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            //Debug.Log("ProtectAreaSensor -- Sense");
            if (botActionConfig.isPlayerInArea && botActionConfig.isProtectingArea)
            {
                if (Physics2D.OverlapCircleNonAlloc(agent.transform.position, botActionConfig.maxRange,
                collider, botActionConfig.AttackableLayerMask) > 0)
                {
                    //Debug.Log(collider[0].transform.name + "ProtectAreaSensor -- Attack Action");
                    return new TransformTarget(collider[0].transform);
                }
            }
            else if (botActionConfig.isProtectingArea)
            {
                Vector2 position = GetRandomDirection(agent); 
                return new PositionTarget(position);
            }
            return null;
        }
        private Vector2 GetRandomDirection(IMonoAgent agent)
        {
            Vector2 randomPosition; 
            do {
                Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                randomPosition = (Vector2)agent.transform.position + direction * botActionConfig.WanderRadius;
                
            } while (Vector2.Distance(randomPosition, botActionConfig.CenterPosition) > botActionConfig.maxRange);
            
            return randomPosition;
        }

        
    }
}
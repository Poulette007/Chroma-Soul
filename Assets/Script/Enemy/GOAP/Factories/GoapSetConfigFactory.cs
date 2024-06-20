using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Resolver;
using UnityEngine;

using Enemy.GOAP.Actions;
using Enemy.GOAP.Goals;
using Enemy.GOAP.Sensors;
using Enemy.GOAP.Targets;
using Enemy.GOAP.WorldKeys;
using UnityEngine.Audio;

namespace Enemy.GOAP.Factories
{
    [RequireComponent(typeof(DependencyInjector))]
    public class GoapSetConfigFactory : GoapSetFactoryBase
    {
        private DependencyInjector injector;
        public override IGoapSetConfig Create()
        {
            injector = GetComponent<DependencyInjector>();
            GoapSetBuilder builder = new("Orc");
            
            BuildAction(builder);
            BuildGoals(builder);
            BuildSensors(builder);


            return builder.Build(); 
        }
        private void BuildGoals(GoapSetBuilder builder)
        {
            builder.AddGoal<WanderGoal>()
                .AddCondition<IsWandering>(Comparison.GreaterThanOrEqual, 1);

            builder.AddGoal<KillPlayerGoal>()
                .AddCondition<PlayerHealth>(Comparison.SmallerThanOrEqual, 0);
            
            builder.AddGoal<ProtectAreaGoal>()
                .AddCondition<isProtectingArea>(Comparison.GreaterThanOrEqual, 1);

        }
        private void BuildAction(GoapSetBuilder builder)
        {
            builder.AddAction<WanderAction>()
                .SetTarget<WanderTarget>()
                .AddEffect<IsWandering>(EffectType.Increase)
                .SetBaseCost(5)
                .SetInRange(10);
                
            builder.AddAction<MeleeAction>()
                .SetTarget<PlayerTarget>()
                .AddEffect<PlayerHealth>(EffectType.Decrease)
                .SetBaseCost(injector.botActionConfig.MeleeAttackCost)
                .SetInRange(injector.botActionConfig.sensorRadius);
            
            builder.AddAction<ProtectAreaAction>()
                .SetTarget<ProtectTarget>()
                .AddEffect<isProtectingArea>(EffectType.Increase)
                .SetBaseCost(5)
                .SetInRange(10);
                
        }
        private void BuildSensors(GoapSetBuilder builder)
        {
            builder.AddTargetSensor<WanderTargetSensor>()
                .SetTarget<WanderTarget>();

            builder.AddTargetSensor<PlayerTargetSensor>()
                .SetTarget<PlayerTarget>();

            builder.AddTargetSensor<ProtectAreaSensor>()
                .SetTarget<ProtectTarget>();
        }
    }
}



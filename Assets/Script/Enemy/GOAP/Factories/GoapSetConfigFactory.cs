using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Resolver;


using Enemy.GOAP.Actions;
using Enemy.GOAP.Goals;
using Enemy.GOAP.Sensors;
using Enemy.GOAP.Targets;
using Enemy.GOAP.WorldKeys;

namespace Enemy.GOAP.Factories
{
    public class GoapSetConfigFactory : GoapSetFactoryBase
    {
        public override IGoapSetConfig Create()
        {
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
        }
        private void BuildAction(GoapSetBuilder builder)
        {
            builder.AddAction<WanderAction>()
                .SetTarget<WanderTarget>()
                .AddEffect<IsWandering>(EffectType.Increase)
                .SetBaseCost(5)
                .SetInRange(10);
        }
        private void BuildSensors(GoapSetBuilder builder)
        {
            builder.AddTargetSensor<WanderTargetSensor>()
                .SetTarget<WanderTarget>();
        }
    }
}



//using System.Numerics;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Classes.Injectors;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using Enemy.GOAP.Config;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy.GOAP.Actions
{
    public class MeleeAction : ActionBase<AttackData>, IInjectable
    {
        private BotActionConfigSO attackConfig;
        public override void Created() {}

        public override void End(IMonoAgent agent, AttackData data)
        {
            //data.animator.SetBool(AttackData.ATTACK, false); 
        }

        public void Inject(DependencyInjector injector)
        {
            attackConfig = injector.botActionConfig;
        }

        public override ActionRunState Perform(IMonoAgent agent, AttackData data, ActionContext context)
        {
            Debug.Log("Melee Action --- Perform");
            data.Timer -= context.DeltaTime;
            //data.Target != null &&
            bool shouldAttack =  Vector3.Distance(data.Target.Position, agent.transform.position) <= attackConfig.MeleeAttackRadius;
            //data.animator.SetBool(AttackData.ATTACK, shouldAttack);

            if (shouldAttack)
            {
                Debug.Log("Melee Action --- Enemy Should Attack");
                agent.transform.LookAt(data.Target.Position);
            }
            return data.Timer > 0? ActionRunState.Continue: ActionRunState.Stop;
        }

        public override void Start(IMonoAgent agent, AttackData data)
        {
            data.Timer = attackConfig.AttackDelay;
        }
    }
}
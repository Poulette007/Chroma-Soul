using UnityEngine;
using Enemy.GOAP.Config;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Classes;

namespace Enemy.GOAP.Actions
{
    public class ProtectAreaAction : ActionBase<AttackData>, IInjectable
    {
        private BotActionConfigSO botActionConfig;

        public override void Created() {}

        public override void End(IMonoAgent agent, AttackData data) {}

        public void Inject(DependencyInjector injector)
        {
            botActionConfig= injector.botActionConfig;
        }

        public override ActionRunState Perform(IMonoAgent agent, AttackData data, ActionContext context)
        {
            //Debug.Log("ProtectAreaAction -- Perform");
            data.Timer -= context.DeltaTime;
            bool shouldAttack =  Vector3.Distance(data.Target.Position, agent.transform.position) <= botActionConfig.MeleeAttackRadius;
            //data.animator.SetBool(AttackData.ATTACK, shouldAttack);

            if (shouldAttack)
            {
                //Debug.Log("Protect Action --- EnemyShouldAttack");
                agent.transform.LookAt(data.Target.Position);
            }
            if (botActionConfig.ChangeAction)
            {
                //Debug.Log("Player no longer in area");
                botActionConfig.ChangeAction = false; 
                return ActionRunState.Stop;
            }
            return data.Timer > 0? ActionRunState.Continue: ActionRunState.Stop;
        }

        public override void Start(IMonoAgent agent, AttackData data)
        {
            data.Timer = Random.Range(botActionConfig.WaitRangeBetweenWanders.x, botActionConfig.WaitRangeBetweenWanders.y);
        }
    }
}
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;
using System.Collections;
using Enemy.GOAP.Config;

namespace Enemy.GOAP.Actions
{
    public class WanderAction : ActionBase<CommonData>, IInjectable
    {
        private BotActionConfigSO botActionConfig;
        public override void Created() {}

        public override void End(IMonoAgent agent, CommonData data) {}

        public void Inject(DependencyInjector injector) {
            botActionConfig = injector.botActionConfig;
        }

        public override ActionRunState Perform(IMonoAgent agent, CommonData data, ActionContext context) {
            //Debug.Log("WanderAction -- Perform");
            data.Timer -= context.DeltaTime;
            if (data.Timer > 0) {
                return ActionRunState.Continue;
            }
            return ActionRunState.Stop;
        }

        public override void Start(IMonoAgent agent, CommonData data) {
            data.Timer = Random.Range(botActionConfig.WaitRangeBetweenWanders.x, botActionConfig.WaitRangeBetweenWanders.y);
        }
    }
}
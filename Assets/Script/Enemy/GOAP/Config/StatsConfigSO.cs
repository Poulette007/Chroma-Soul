using UnityEngine;

namespace Enemy.GOAP.Config
{
    [CreateAssetMenu(menuName = "AI/Stats ConfigSO", fileName = "Stats Config", order = 3)]

    public class StatsConfigSO : ScriptableObject
    {
        public string botName = "Orc";
        public float pv = 10f;

        public bool haveMana = true;
        public float manaRestorationRate = 1f;
        public float maxMana = 20f;

        public bool haveStamia = true;
        public float stamiaDepletionRate = 0.25f;
        public float maxStamia = 20f;
        public float stamiaRestorationRatePerSeconde = 5f;
        public float stamiaAcceptableLimit = 5f;
        public LayerMask layerMask; 
    }

}
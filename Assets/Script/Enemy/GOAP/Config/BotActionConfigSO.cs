using Enemy.GOAP.Config;
using UnityEditor;
using UnityEngine;

namespace Enemy.GOAP.Config
{
    [CreateAssetMenu(menuName = "AI/Bot Action ConfigSO", fileName = "Bot Action Config", order = 1)]

    public class BotActionConfigSO : ScriptableObject
    {
        [Header("Attack Settings")]   
        public float sensorRadius = 10f;
        public float MeleeAttackRadius = 1f;
        public float AttackDelay = 1f;
        public int MeleeAttackCost = 1;
        public LayerMask AttackableLayerMask;

        
        [Space(20)]
        [Header("Wander Settings")] 
        public Vector2 WaitRangeBetweenWanders = new(2,5);
        public float WanderRadius = 5f;

        [Space(20)]
        [Header("Protect Settings")] 
        public bool isProtectingArea = false;
        public float maxRange = 10f;
        [HideInInspector]
        public bool isPlayerInArea = false;
        [HideInInspector]
        public bool ChangeAction = false;
        [HideInInspector]
        public Vector2 CenterPosition = new(0, 0);
    }
}
[CustomEditor(typeof(BotActionConfigSO))]
public class BotActionConfigSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Draws the default inspector

        BotActionConfigSO script = (BotActionConfigSO)target;

        // Displays the value but makes it non-editable
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.Toggle("Is Player In Area", script.isPlayerInArea);
        EditorGUILayout.Toggle("Change Action", script.ChangeAction);
        EditorGUILayout.Vector2Field("Center Position", script.CenterPosition);
        EditorGUI.EndDisabledGroup();
    }
}
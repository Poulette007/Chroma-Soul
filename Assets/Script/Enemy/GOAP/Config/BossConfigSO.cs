using Enemy.GOAP.Config;
using UnityEditor;
using UnityEngine;

namespace Enemy.GOAP.Config
{
    [CreateAssetMenu(menuName = "AI/Boss ConfigSO", fileName = "Boss Config", order = 2)]

    public class BossConfigSO : ScriptableObject
    {
        public float sensorRadius = 10f;
        public LayerMask detectableLayerMask;
        public int maxDetectedBots = 3;  // Nombre maximum de bots pouvant être détectés simultanément
        
        [HideInInspector]
        public int currentDetectedBots;
    }

}
public class BossConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Draws the default inspector

        BossConfigSO script = (BossConfigSO)target;

        // Displays the value but makes it non-editable
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.Toggle("Is Player In Area", script.currentDetectedBots != 0);
        EditorGUI.EndDisabledGroup();
    }
}
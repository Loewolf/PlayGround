using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TaskTester))]
public class TilesUpdaterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TaskTester taskTester = (TaskTester)target;
        if (GUILayout.Button("Reset Task"))
        {
            taskTester.ResetTask();
        }
    }
}

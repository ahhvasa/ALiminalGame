using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomBuilder))]
public class RoomBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoomBuilder builder = (RoomBuilder)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Build"))
        {
            builder.Build();
        }

        if (GUILayout.Button("Clear"))
        {
            builder.ClearBuilding();
        }
    }
}
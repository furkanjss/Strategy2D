using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(GridGenerator))]
public class GridGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridGenerator gridGenerator = (GridGenerator)target;

        EditorGUILayout.Space();

        if (GUILayout.Button("Generate Grid", GUILayout.MinHeight(30)))
        {
            gridGenerator.GenerateGrid();
        }

    }
}
#endif
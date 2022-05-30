using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DirtGenerator))]
public class EditorDirtGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        DirtGenerator dirtGen = (DirtGenerator)target;

        if (DrawDefaultInspector() && dirtGen.autoUpdate)
        {
            dirtGen.GenerateDirt();
        }

        if (GUILayout.Button("Generate"))
        {
            dirtGen.GenerateDirt();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RagdollScrch1))]
public class RagdollEditorScrch1 : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RagdollScrch1 ragdoll = (RagdollScrch1)target;

        if (GUILayout.Button("Setup Ragdoll"))
        {
            ragdoll._RagdollSetup();
        }
    }
}

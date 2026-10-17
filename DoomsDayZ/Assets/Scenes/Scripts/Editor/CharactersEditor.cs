using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using IndieGameCharacterControllerSystem;

namespace IndieGameEditor
{
    [CustomEditor(typeof(CharacterRagdoll))]
    public class CharactersEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CharacterRagdoll characterRagdoll = (CharacterRagdoll)target;

            if (GUILayout.Button("Setup Ragdoll"))
            {
                characterRagdoll.setupRagdollParts();
                if (characterRagdoll._RagdollParts.Count > 0)
                {
                    Debug.Log("Successfully setup ragdoll");
                }
            }
        }
    }
}


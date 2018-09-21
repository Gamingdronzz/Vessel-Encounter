#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

namespace VesselEncounter.Editor
{
    public class UiMenuOptions
    {
#if UNITY_EDITOR

        [MenuItem("GameObject/UI/Custom/Text", false, 100)]
        public static void CreateText()
        {
            GameObject go = EditorGUIUtility.Load("UiTemplates/TextTemplate.prefab") as GameObject;
            go = GameObject.Instantiate<GameObject>(go);
            go.name = "Text";
            go.transform.SetParent(Selection.activeTransform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
        }

        [MenuItem("GameObject/UI/Custom/TextMesh Pro", false, 100)]
        public static void CreateTextMeshText()
        {
            GameObject go = EditorGUIUtility.Load("UiTemplates/TextTemplateTextMesh.prefab") as GameObject;
            go = GameObject.Instantiate<GameObject>(go);
            go.name = "Text";
            go.transform.SetParent(Selection.activeTransform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
        }

        [MenuItem("GameObject/UI/Custom/Button With Text", false, 100)]
        public static void CreateButtonWithText()
        {
            GameObject go = EditorGUIUtility.Load("UiTemplates/ButtonTemplateWithText.prefab") as GameObject;
            go = GameObject.Instantiate<GameObject>(go);
            go.name = "Button";
            go.transform.SetParent(Selection.activeTransform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
        }

        [MenuItem("GameObject/UI/Custom/Toggle", false, 100)]
        public static void CreateToggle()
        {
            GameObject go = EditorGUIUtility.Load("UiTemplates/ToggleTemplate.prefab") as GameObject;
            go = GameObject.Instantiate<GameObject>(go);
            go.name = "Toggle";
            go.transform.SetParent(Selection.activeTransform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
        }

#endif
    }
}
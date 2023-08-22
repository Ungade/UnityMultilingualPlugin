using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MultilingualPlugin
{
    [Serializable]
    public class MultilingualObject<T>
    {
        [SerializeField] private List<T> _values = new List<T>(new T[104]);
        public T Value => Values[Multilingual.AllLanguagesList.IndexOf(Multilingual.CurrentLanguage)];

        public List<T> Values { get => _values; set => _values = value; }
    }

    [CustomPropertyDrawer(typeof(MultilingualObject<>))]
    public class IngredientDrawer : PropertyDrawer
    {
        private bool GetValuesFoldout(string key)
        {
            if (EditorPrefs.HasKey(key) == false)
            {
                return true;
            }
            return EditorPrefs.GetBool(key);
        }

        private void SetValuesFoldout(string key, bool value)
        {
            EditorPrefs.SetBool(key, value);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string valuesFoldoutKey = $"{property.name}_ValuesFoldout";
            SerializedProperty valueslistProperty = property.FindPropertyRelative("_values");
            SetValuesFoldout(valuesFoldoutKey, EditorGUI.Foldout(position, GetValuesFoldout(valuesFoldoutKey), label, true));
            if (GetValuesFoldout(valuesFoldoutKey))
            {
                valueslistProperty.arraySize = Multilingual.AllLanguagesList.Count;
                foreach (KeyValuePair<AllLanguages, int> languageWithIndex in Multilingual.ActiveLanguagesWithIndexes)
                {
                    EditorGUILayout.PropertyField(valueslistProperty.GetArrayElementAtIndex(languageWithIndex.Value), new GUIContent($"{string.Concat(Enumerable.Repeat(" ", 5))}{languageWithIndex.Key.ToString().Replace('_', ' ')}"));
                }
            }
        }
    }
}
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PropertyCollectionSetter : EditorWindow
{
    Transform parentTransform;
    MonoBehaviour targetMonoBehaviour;
    string propertyName;
    [MenuItem("Tools/Property Collection Setter")]
    public static void ShowWindow()
    {
        GetWindow(typeof(PropertyCollectionSetter));
    }

    private void OnGUI()
    {
        parentTransform = EditorGUILayout.ObjectField("Parent Transform", parentTransform, typeof(Transform), true) as Transform;
        targetMonoBehaviour = EditorGUILayout.ObjectField("Target Mono Behaviour", targetMonoBehaviour, typeof(MonoBehaviour), true) as MonoBehaviour;

        EditorGUILayout.LabelField("Check these fields in target script before setting the collection");
        propertyName = EditorGUILayout.TextField("Property Name (Object Collection)", propertyName);

        if (parentTransform && targetMonoBehaviour && propertyName.Length > 0)
        {
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Set Collection Using Transform"))
            {
                SetCollection();
            }
        }
    }

    private void SetCollection()
    {
        FieldInfo fieldInfo = targetMonoBehaviour.GetType().GetField(propertyName);
        if (fieldInfo != null)
        {
            Type fieldType = fieldInfo.FieldType;
            bool isArray = fieldType.IsArray;
            Type argumentType = isArray? fieldType.GetElementType() : fieldType.GetGenericArguments()[0];
            Debug.Log("Field type is " + fieldType.ToString() + "; argument type is " + argumentType.ToString());

            var listType = typeof(List<>).MakeGenericType(argumentType);
            var list = (IList)Activator.CreateInstance(listType);

            for (int i = 0; i < parentTransform.childCount; ++i)
            {
                Transform child = parentTransform.GetChild(i);
                if (child.gameObject.activeSelf)
                {
                    object component = parentTransform.GetChild(i).GetComponentInChildren(argumentType);
                    if (component != null) list.Add(component);
                }
            }
            if (isArray)
            {
                object array = Array.CreateInstance(argumentType, list.Count);
                list.CopyTo((Array)array, 0);
                fieldInfo.SetValue(targetMonoBehaviour, array);
            }
            else fieldInfo.SetValue(targetMonoBehaviour, list);
        }
        else
        {
            Debug.Log("Variable with name " + propertyName + " isn't found.");
        }
    }
}
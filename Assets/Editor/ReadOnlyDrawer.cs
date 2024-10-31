using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    //handles the rendering of a property in the inspector
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; //disables the field and makes it non interactable
        EditorGUI.PropertyField(position, property, label, true); //basic function to render the field
        GUI.enabled = true; //Ensure that the other fields that come after are interactable
    }
}

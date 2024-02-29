#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts.Internal
{

[CustomPropertyDrawer(typeof(NotePad))]
internal class NotePadAttributeDrawer : PropertyDrawer
{
    private SerializedProperty _serializedProperty;
    private SerializedProperty _foldoutProperty;
    private SerializedProperty _textProperty;

    private bool _editMode;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _serializedProperty = property;
        _textProperty ??= property.FindPropertyRelative("text");
        _foldoutProperty ??= property.FindPropertyRelative("wasFoldout");

        if (_editMode)
        {
            if (GUILayout.Button("Close", GUILayout.Width(50)))
                _editMode = false;
            
            _text = EditorGUILayout.TextArea(_text);
        }
        else
        {
            EditorGUILayout.BeginHorizontal();

            _opened = EditorGUILayout.Foldout(_opened, property.displayName);

            if (GUILayout.Button("Edit", GUILayout.Width(50)))
                _editMode = true;
        
            EditorGUILayout.EndHorizontal();

            if (!_opened)
                return;

            var count = _text.Split("\n").Length;
            var style = new GUIStyle(GUI.skin.label) {richText = true};

            Color color = GUI.color;
            GUI.color = new Color(color.r, color.g, color.b, .66f);
            EditorGUILayout.LabelField(_text, style, GUILayout.Height(count * EditorGUIUtility.singleLineHeight));
            GUI.color = color;
        }
    }

    private bool _opened
    {
        get => _foldoutProperty.boolValue;
        set
        {
            _foldoutProperty.boolValue = value;
            _serializedProperty.serializedObject.ApplyModifiedProperties();
        }
    }
    
    private string _text
    {
        get
        {
            var text = _textProperty.stringValue;
            return string.IsNullOrEmpty(text) ? "No notes found!" : text;
        }
        set
        {
            _textProperty.stringValue = value;
            _serializedProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}

}
#endif

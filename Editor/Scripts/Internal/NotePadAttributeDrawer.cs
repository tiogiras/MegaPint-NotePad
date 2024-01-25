#if UNITY_EDITOR
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Scripts.Internal
{

[CustomPropertyDrawer(typeof(NotePad))]
public class NotePadAttributeDrawer : PropertyDrawer
{
    private const string Path = "NotePad/User Interface/NotePad";

    private GroupBox _open;
    private GroupBox _closed;

    private TextField _textInput;
    private Foldout _foldout;
    private Label _notes;

    private Button _btnOpen;
    private Button _btnClose;

    private SerializedProperty _serializedProperty;
    private SerializedProperty _foldoutProperty;
    private SerializedProperty _textProperty;
    private bool _opened;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var template = Resources.Load<VisualTreeAsset>(Path);

        TemplateContainer root = template.Instantiate();

        _serializedProperty = property;
        _textProperty = property.FindPropertyRelative("text");
        _foldoutProperty = property.FindPropertyRelative("wasFoldout");

        _open = root.Q <GroupBox>("Open");
        _closed = root.Q <GroupBox>("Closed");

        _foldout = _closed.Q <Foldout>("Foldout");
        _notes = _closed.Q <Label>("Notes");
        _btnOpen = _closed.Q <Button>("BTN_Open");

        _textInput = _open.Q <TextField>("TextInput");
        _btnClose = _open.Q <Button>("BTN_Close");
        
        _open.style.display = DisplayStyle.None;
        _closed.style.display = DisplayStyle.Flex;

        var propertyName = ObjectNames.NicifyVariableName(property.name);
        
        _foldout.text = propertyName;
        _foldout.value = _foldoutProperty.boolValue;

        _notes.text = _textProperty.stringValue;

        _textInput.value = _textProperty.stringValue;

        RegisterCallbacks();

        return root;
    }

    private void RegisterCallbacks()
    {
        _btnOpen.clicked += Toggle;
        _btnClose.clicked += Toggle;

        _textInput.RegisterValueChangedCallback(OnTextChanged);
        _foldout.RegisterValueChangedCallback(OnFoldoutChanged);
    }

    private void OnFoldoutChanged(ChangeEvent <bool> evt)
    {
        _foldoutProperty.boolValue = evt.newValue;
        
        _serializedProperty.serializedObject.ApplyModifiedProperties();
    }

    private void OnTextChanged(ChangeEvent <string> evt)
    {
        _textProperty.stringValue = evt.newValue;
        _notes.text = evt.newValue;

        _serializedProperty.serializedObject.ApplyModifiedProperties();
    }

    private void Toggle()
    {
        _opened = !_opened;
        
        _open.style.display = _opened ? DisplayStyle.Flex : DisplayStyle.None;
        _closed.style.display = _opened ? DisplayStyle.None : DisplayStyle.Flex;
    }
}

}
#endif

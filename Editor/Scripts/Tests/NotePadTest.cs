#if UNITY_EDITOR
using System.Collections;
using MegaPint.TestScripts;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace MegaPint.Editor.Scripts.Tests
{

/// <summary> NotePad specific unit tests </summary>
public class NotePadTest
{
    private GameObject _gameObject;
    private NotePadTestMono _notepad;
    
    #region Tests

    [Test] [Order(0)]
    public void Add()
    {
        var gameObject = new GameObject("NotePad test");
        _notepad = gameObject.AddComponent <NotePadTestMono>();

        Selection.activeObject = gameObject;
    }

    [UnityTest] [Order(1)]
    public IEnumerator Change()
    {
        _notepad.notepad.text = "Hello World!";
        _notepad.notepad.wasFoldout = true;

        Selection.activeObject = null;
        
        yield return null;
        
        Selection.activeObject = _gameObject;

        Assert.AreEqual(_notepad.notepad.text, "Hello World!");
        Assert.AreEqual(_notepad.notepad.wasFoldout, true);
    }

    #endregion
}

}
#endif

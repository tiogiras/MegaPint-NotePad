#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("tiogiras.megapint.notepad.editor.tests")]
namespace MegaPint.TestScripts
{

/// <summary> Class added to gameObject during unit test </summary>
internal class NotePadTestMono : MonoBehaviour
{
    public NotePad notepad;
}

}
#endif

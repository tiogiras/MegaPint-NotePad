#if UNITY_EDITOR
using Editor.Scripts.GUI;

namespace Editor.Scripts
{

internal static partial class DisplayContent
{
    #region Private Methods

    // Called by reflection
    // ReSharper disable once UnusedMember.Local
    private static void NotePad(DisplayContentReferences refs)
    {
        InitializeDisplayContent(
            refs,
            new TabSettings
            {
                info = true,
                guides = true,
                help = true
            },
            new TabActions
            {
                info = root =>
                {
                    GUIUtility.ActivateLinks(root, null);
                },
                help = root =>
                {
                    GUIUtility.ActivateLinks(root, null);
                }
            });
    }

    #endregion
}

}
#endif

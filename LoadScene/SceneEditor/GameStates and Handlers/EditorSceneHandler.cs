using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;

namespace LoadScene.SceneEditor
{
    public interface EditorSceneHandler
    {
        IMission LoadSceneEditor();


        void LoadScreen();

        
        ScreenBase GetScreen();
        
    }
}
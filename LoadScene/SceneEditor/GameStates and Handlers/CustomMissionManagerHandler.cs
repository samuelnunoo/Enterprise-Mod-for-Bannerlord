using SandBox;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;

namespace LoadScene.SceneEditor
{
    public class CustomMissionManagerHandler : CampaignMissionManager, EditorSceneHandler
    {
        private ScreenBase _screen;
        
        IMission EditorSceneHandler.LoadSceneEditor() { return CustomMissionManager.OpenSceneEditor(); }
      
        void EditorSceneHandler.LoadScreen() { 
            Game.Current.GameStateManager.CleanAndPushState((GameState) Game.Current.GameStateManager.CreateState<CustomEditorState>(), 0);
            
        }

        ScreenBase EditorSceneHandler.GetScreen()
        {
            return _screen;
        }
        
    }
}
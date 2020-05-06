using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Screen;

namespace LoadScene.SceneEditor.MissionViews
{
    public class CustomMissionScreen: MissionScreen
    {
        public CustomMissionScreen(MissionState mt) : base(mt)
        {
        }
        
        public override bool MouseVisible => true; 
    }
}
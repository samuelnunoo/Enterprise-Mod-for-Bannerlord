
using SandBox.Quests.QuestBehaviors;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;
using TaleWorlds.MountAndBlade.View.Screen;

namespace LoadScene.SceneEditor.MissionViews


{
    partial class SceneEditorMissionView : MissionView
    {

        private Ray _mouseRay;
        private Vec3 rayBegin;
        private Vec3 rayEnd;

        public override void OnMissionScreenTick(float dt)
            {
                this.MissionScreen.SceneLayer.InputRestrictions.SetMouseVisibility(true);
                base.OnMissionScreenTick(dt); 
                //  CameraLogicTick()
                EntityInteractionsTick();
                
            
            }
            

            
            public override void OnMissionScreenInitialize()
            {

           
                base.OnMissionScreenInitialize();
          



            }
    }
    
    
    
    
    
}
using System.Collections.Generic;
using LoadScene.SceneEditor.MissionViews;
using SandBox;
using SandBox.Source.Missions;
using SandBox.Source.Missions.Handlers;
using SandBox.View.Missions;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Source.Missions;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers;
using TaleWorlds.MountAndBlade.View.Missions;

namespace LoadScene.SceneEditor
{
    
    
    //MissionManager | No need to Initialize?
    [MissionManager]
    public class CustomMissionManager 
    {
        [MissionMethod]
        public static Mission OpenSceneEditor()
        {
            return  MissionState.OpenNew(
                "SceneView", new MissionInitializerRecord("empire_village_002"), (Mission missionController) => new MissionBehaviour[]
                {
                    
          
                    
        },true, true,true );
        }
        
   
    }

    //View Creator Module | No need to Initialize?
    [ViewCreatorModule]
    class CustomViewCreator
    {
        
        [ViewMethod("SceneView")]
        public static MissionView[] OpenSceneEditor(Mission mission)
        {
            return new List<MissionView>
            {
                new SceneEditorMissionView()
            }.ToArray();
        }
    }


}
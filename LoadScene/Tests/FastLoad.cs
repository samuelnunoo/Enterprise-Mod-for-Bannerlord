using System;
using SandBox;
using TaleWorlds.Core;
using SandBox.ViewModelCollection.SaveLoad;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Screens;
using TaleWorlds.MountAndBlade;
using TaleWorlds.SaveSystem;

namespace LoadScene.Tests
{
    public class FastLoad : SaveLoadVM
    {
        public FastLoad( bool IsSaving) : base(IsSaving)
        {
            var game = base.SavedGamesList[1];
            LoadGameResult saveGameData = MBSaveLoad.LoadSaveGameData(game.Save.Name, Utilities.GetModulesNames());
            var result = saveGameData.LoadResult;
            MBGameManager.StartNewGame((MBGameManager) new CampaignGameManager(result));
         

        }
        
       
    }
}
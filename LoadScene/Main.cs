using LoadScene.ModelObjects;
using LoadScene.Tests;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using UIExtenderLib;

namespace LoadScene
{
    public class SubModule : MBSubModuleBase
    {

        private UIExtender _extender;
     
        
        //Initialize UIExtender
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            
            _extender = new UIExtender("ExampleUIMod");
            _extender.Register();
       



        }

        
        //Initialize CampaignBehaviors
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            
            Campaign campaign = game.GameType as Campaign;
            bool flag = (campaign == null);
            if (!flag)
            {
                CampaignGameStarter gameInitializer = (CampaignGameStarter) gameStarterObject;
                    this.AddBehaviors(gameInitializer);
              
            }
            

            
        }
        
        //Verify UIExtenderLib
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            _extender.Verify();
        }

        //Helper for CampaignBehaviors Initialze
        private void AddBehaviors(CampaignGameStarter gameInitializer)
        {
            gameInitializer.AddBehavior(EnterpriseCampaignBehavior.Instance);
        }

      
    }
    
}




using LoadScene.ModelObjects;
using LoadScene.Tests;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using UIExtenderLib;

namespace LoadScene
{
    public class SubModule : MBSubModuleBase
    {

        private UIExtender _extender;
        private bool bounce;
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            
            _extender = new UIExtender("ExampleUIMod");
            _extender.Register();
            bounce = false;



        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            var marketplace = new Marketplace();
            marketplace.PopulateMarket();
            
        }
    }
    
}




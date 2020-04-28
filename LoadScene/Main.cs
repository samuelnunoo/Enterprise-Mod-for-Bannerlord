using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using UIExtenderLib;

namespace LoadScene
{
    public class SubModule : MBSubModuleBase
    {

        private UIExtender _extender;
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            
            _extender = new UIExtender("ExampleUIMod");
            _extender.Register();
            
           
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            _extender.Verify();
        }

    
    }
}




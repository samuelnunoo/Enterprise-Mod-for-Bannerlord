
using LoadScene.NavigationElements;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Screen;

namespace LoadScene.UIElements
{
    [GameStateScreen(typeof(HireWorkerState))]
    public class HireWorkerScreen : ScreenBase, IGameStateListener
    {

        private HireWorkerVM _dataSource;
        private GauntletLayer _gauntletLayer;
        private readonly HireWorkerState _customState;
        
        public HireWorkerScreen(HireWorkerState customState)
        {
            this._customState = customState;
            this._customState.Listener = (IGameStateListener) this;
        }

        void IGameStateListener.OnActivate()
        {
            base.OnActivate();
            this._gauntletLayer = new GauntletLayer(1,"GauntletLayer");
            this._gauntletLayer.InputRestrictions.SetInputRestrictions(true,InputUsageMask.All);
            this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
            this._gauntletLayer.IsFocusLayer = true;
            ScreenManager.TrySetFocus((ScreenLayer) this._gauntletLayer);
            this.AddLayer((ScreenLayer) this._gauntletLayer);

            this._dataSource = new HireWorkerVM();
            this._gauntletLayer.LoadMovie("ClanScreen", this._dataSource);

        }

        void IGameStateListener.OnDeactivate()
        {
            this.OnDeactivate();
            this.RemoveLayer((ScreenLayer) this._gauntletLayer);
            this._gauntletLayer.IsFocusLayer = false;
            ScreenManager.TryLoseFocus((ScreenLayer) this._gauntletLayer);
        }

        void IGameStateListener.OnInitialize()
        {
        }

        void IGameStateListener.OnFinalize()
        {
            this._dataSource = (HireWorkerVM) null;
            this._gauntletLayer = (GauntletLayer) null;
        }


    }



    public class HireWorkerVM : ViewModel
    {

        public HireWorkerVM()
        {
        }
    }
}
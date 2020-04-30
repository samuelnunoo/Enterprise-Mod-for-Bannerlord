
using LoadScene.ModelObjects;
using LoadScene.NavigationElements;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Screen;

namespace LoadScene.UIElements
{
    [GameStateScreen(typeof(WorkerOverViewState))]
    public class WorkerOverviewScreen : ScreenBase, IGameStateListener
    {

        //Class Variables
        private WorkerOverviewVM _dataSource;
        private GauntletLayer _gauntletLayer;
        private readonly WorkerOverViewState _customState;
      
        
        
        // Set CustomState, Listener, and Worker
        public WorkerOverviewScreen(WorkerOverViewState customState)
        {
            this._customState = customState;
            this._customState.Listener = (IGameStateListener) this;
            
        }

        
        //Activate VM and Movie 
        void IGameStateListener.OnActivate()
        {
            base.OnActivate();
            
            //Template 
            this._gauntletLayer = new GauntletLayer(1,"GauntletLayer");
            this._gauntletLayer.InputRestrictions.SetInputRestrictions(true,InputUsageMask.All);
            this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
            this._gauntletLayer.IsFocusLayer = true;
            ScreenManager.TrySetFocus((ScreenLayer) this._gauntletLayer);
            this.AddLayer((ScreenLayer) this._gauntletLayer);

            
            //DataSource and Movie  | Change Movie!!!!
            this._dataSource = new WorkerOverviewVM(this._customState.Worker);
            this._gauntletLayer.LoadMovie("ClanScreen", this._dataSource);

        }

        void IGameStateListener.OnDeactivate()
        {
            //Template
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
            
            this._dataSource = (WorkerOverviewVM) null;
            this._gauntletLayer = (GauntletLayer) null;
        }


    }



    public class WorkerOverviewVM : ViewModel
    {


        private WorkerVM _workerVM;
        
        
        public WorkerOverviewVM(Worker worker)
        {
        }
    }
}
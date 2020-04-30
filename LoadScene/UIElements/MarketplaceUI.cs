
using LoadScene.ModelObjects;
using LoadScene.NavigationElements;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Screen;
using TaleWorlds.InputSystem;
using TaleWorlds.ObjectSystem;
using UIExtenderLib.Interface;

namespace LoadScene.UIElements
{
    
    
    [GameStateScreen(typeof(CustomState))]
    public class MarketplaceScreen : ScreenBase, IGameStateListener
    {
        private GauntletLayer _gauntletLayer;
        private readonly CustomState _customState;
        private MarketplaceVM _dataSource;
     


        public MarketplaceScreen(CustomState customState)
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
            
            this._dataSource = new MarketplaceVM();
            this._gauntletLayer.LoadMovie("ClanScreen2", this._dataSource);

        }
        
        void IGameStateListener.OnDeactivate()
        {
            this.OnDeactivate();
            this.RemoveLayer((ScreenLayer) this._gauntletLayer);
            this._gauntletLayer.IsFocusLayer = false;
            ScreenManager.TryLoseFocus((ScreenLayer) this._gauntletLayer);
        }
        
        void IGameStateListener.OnInitialize(){}
        
        void IGameStateListener.OnFinalize()
        {
            this._dataSource = (MarketplaceVM) null;
            this._gauntletLayer = (GauntletLayer) null;
        }
    }



    public class MarketplaceVM : ViewModel
    {
        
        
        
        //Class Variables 
        private MBBindingList<WorkerVM> _heros;
        private CustomNavigationHandler _navigationHandler;
        private EnterpriseCampaignBehavior _enterprise;
        
        public MarketplaceVM(){

            
            //Set Class Variables 
            this._heros = new MBBindingList<WorkerVM>();
            this._navigationHandler = new CustomNavigation();
            this._enterprise = Campaign.Current.GetCampaignBehavior<EnterpriseCampaignBehavior>();

            
            //Add Workers to HeroVM
            foreach (Worker worker in this._enterprise.GetWorkers)
            {
                this._heros.Add(new WorkerVM(worker.Character,worker));
            }
         
            
            
      
     

        }
        
        //HeroCharacter
        [DataSourceProperty] 
        public MBBindingList<WorkerVM> HeroCharacters
        {
            get { return this._heros; }

            set
            {
                if (value != this._heros)
                {
                    this._heros = value;
                }
                base.OnPropertyChanged("HeroCharacters");
            }
        }

        //Close Screen
        [DataSourceMethod]
        public void ExecuteClose()
        {
            Game.Current.GameStateManager.PopState(0);
        }



    }



}
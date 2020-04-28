using LoadScene.NavigationElements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map;
using UIExtenderLib.Interface;

namespace LoadScene.MapBar
{
    [ViewModelMixin]
    public class CustomMapVM : BaseViewModelMixin<MapNavigationVM>
    {
        private CustomNavigationHandler _navigationHandler;
        public CustomMapVM(MapNavigationVM vm) : base(vm)
        {
            this._navigationHandler = new CustomNavigation();
        }
            
            

        
        // Add Your MapBar Button Methods Here
        [DataSourceMethod]
        public void ExecuteOpenScene()
        {
         this._navigationHandler.OpenCustom();
        }
                
               
        }
    
    [UIExtenderLib.Interface.PrefabExtension("MapBar",
        "/Prefab/Window/Widget/Children/Widget/Children/ListPanel/Children/Widget")]
    public class PrefabExtension : PrefabExtensionInsertAsSiblingPatch
    {
        public override string Name => "ExampleButton";
        public override InsertType Type => InsertType.Append;
        
    }



    }



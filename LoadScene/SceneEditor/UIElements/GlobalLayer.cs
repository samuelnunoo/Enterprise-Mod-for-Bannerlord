using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;

namespace LoadScene.SceneEditor.UIElements
{
    public class testGlobalLayer: GlobalLayer
    {

        private GauntletLayer _gauntletLayer;
        private GauntletMovie _movie;
        
        public void Initialize()
        {
            
            this._gauntletLayer = new GauntletLayer(200,"GauntletLayer");
            this.Layer = _gauntletLayer;
            this._movie = this._gauntletLayer.LoadMovie("SceneEditor", new ItemVM(ItemVM.UsageType.Loot));
            






        }
    }
}
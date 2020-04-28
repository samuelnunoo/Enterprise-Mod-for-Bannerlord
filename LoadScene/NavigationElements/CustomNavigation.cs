
using SandBox.View.Map;
using TaleWorlds.Core;

namespace LoadScene.NavigationElements
{
    public class CustomNavigation : MapNavigationHandler, CustomNavigationHandler
    {

        private Game _game;

        public CustomNavigation()
        {
            this._game = Game.Current;
        }

        void CustomNavigationHandler.OpenCustom()
        {
            this._game.GameStateManager.PushState(this._game.GameStateManager.CreateState<CustomState>(), 0);
        }


        void CustomNavigationHandler.OpenOverview()
        {
            this._game.GameStateManager.PushState(this._game.GameStateManager.CreateState<HireWorkerState>(), 0);
        }
    }
}
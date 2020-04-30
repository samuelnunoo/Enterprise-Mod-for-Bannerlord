
using LoadScene.ModelObjects;
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


        void CustomNavigationHandler.OpenOverview(Worker worker)
        {
            //Create GameState
            WorkerOverViewState workerState = this._game.GameStateManager.CreateState<WorkerOverViewState>();
            
            //Add value to State 
            workerState.Worker = worker;
            
            //Push State
            this._game.GameStateManager.PushState(workerState, 0);
        }
        
    }
}
using TaleWorlds.Core;

namespace LoadScene.NavigationElements
{
    public class CustomState : GameState
    {
        private CustomStateHandler _handler;

        public override bool IsMenuState
        {
            get { return true; }
        }

        public CustomStateHandler Handler
        {
            get { return this._handler; }
            set { this._handler = value; }
            
        }

    }
    
    public class HireWorkerState: CustomState{}
    
}
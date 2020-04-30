using LoadScene.ModelObjects;
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

    
    
    
    //You can pass variables through GameStates
    public class WorkerOverViewState : CustomState
    {
        
        
        //Variable to Pass
        public Worker Worker { get; set; }
        
        
    }



}
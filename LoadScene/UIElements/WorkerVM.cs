using LoadScene.NavigationElements;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using UIExtenderLib.Interface;

namespace LoadScene.ModelObjects
{
    
    
    //This is a ViewModel to Replace HeroVM
    public class WorkerVM : HeroVM
    {
        private Worker _worker;
        private CustomNavigationHandler _navigationHandler;

        public WorkerVM(Hero hero, Worker worker) : base(hero)
        {
            this._worker = worker;
            this._navigationHandler = new CustomNavigation();

        }


        //OpenOverView Screen
        [DataSourceMethod]
        public void WorkerOverview()
        {
            this._navigationHandler.OpenOverview(this._worker);
        }
        
    }
}
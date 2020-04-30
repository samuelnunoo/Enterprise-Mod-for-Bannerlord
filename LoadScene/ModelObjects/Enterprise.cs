using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;

namespace LoadScene.ModelObjects
{
    public class EnterpriseCampaignBehavior : CampaignBehaviorBase
    {
      
        //Marketplace Model and Worker Roster
        private Marketplace _marketplace;
        private List<Worker> _workers; 
        
        //Instance to add to GameInitializer 
        public static readonly EnterpriseCampaignBehavior Instance = new EnterpriseCampaignBehavior();

        //Workerlist for Marketplace ViewModel
        public List<Worker> GetWorkers => _marketplace.GetMarketList;

        //Events to Register 
        public override void RegisterEvents()
        {
            this._marketplace = new Marketplace();
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this,new Action<CampaignGameStarter>(this.OnGameLoaded));
            
       
        }

        
        //Will use later to save a lot of Data
        public override void SyncData(IDataStore dataStore)
        {
        }
        
        //Method to Populate Markets 
        public void OnGameLoaded(CampaignGameStarter obj){
            
            this._marketplace.PopulateMarket();
        }

    
    }
}
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace LoadScene.ModelObjects
{
    public class Marketplace : CampaignBehaviorBase
    {

        private List<Worker> _workers; 
        
        public override void RegisterEvents()
        {
          
        }

        public override void SyncData(IDataStore dataStore)
        {

        }


        public void PopulateMarket()
        {


        }
        
        public void RandomWorker()
        {
            var worker = MBObjectManager.Instance.GetObjectTypeList<Hero>().GetRandomElement();
            var multiplier = new Traits();
        

        }
    }


}

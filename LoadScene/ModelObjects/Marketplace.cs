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
            if (this._workers.Count == 20)
            {
                return;
            }
            
            for (var i = 0; i < (20 - this._workers.Count); i++)
            {
                var worker = this.RandomWorker();
                this._workers.Add(worker);
            }

        }

        public List<Worker> GetMarketList => this._workers;
       
        
        public NormalWorker RandomWorker()
        {
            var hero = MBObjectManager.Instance.GetObjectTypeList<Hero>().GetRandomElement();
            var traits = new Traits().GetRandom3();
            var worker  = new NormalWorker(hero,traits);
            worker.DefaultStats();


            return worker;




        }
    }


}

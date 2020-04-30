using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace LoadScene.ModelObjects
{
    public class Marketplace : GameModel
    {
       

        //Internal List of Workers 
        private List<Worker> _workers;
        
        // Initialize List
        public Marketplace() {
            
            this._workers = new List<Worker>();
            
            
        }

        //Adds up to 20 workers to List
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

        
        //Clears the Market 
        public void ClearMarket()
        {
            this._workers = new List<Worker>();
        }

        
        //Public Getter for Market List 
        public List<Worker> GetMarketList => this._workers;
       
        
        //Generate Random Worker
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

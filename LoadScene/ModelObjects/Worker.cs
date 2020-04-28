using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Linq;
using System.Windows.Markup;


namespace LoadScene.ModelObjects
{

   
 
    
    public abstract class Worker : GameModel
    {

        public abstract  string Role { get; set; }
        public abstract int Tier { get; }
        public abstract CharacterObject Character { get; }
        public abstract double Experience { get; }
        
        public abstract double BaseCost { get; }
        public abstract double Salary { get; }

        public abstract double EXPMult { get; }
        public abstract double SalaryMult { get; }
        public abstract double EfficienceMult { get; }
        public abstract double StaminaMult { get; }
        public abstract double HappinessMult { get; }
        public abstract double TheftMult { get; }
        
        public abstract double Stamina { get; }
        public abstract double Happiness { get; }
    }
    
    
    public class Prisoner : Worker
    {

        private CharacterObject _character;
      
        public Prisoner(Hero hero )
        {
            this._character = hero.CharacterObject;
            
        }

        public override CharacterObject Character
        {
            get { return this._character; }
        }

       

    }

    public class Hired : Worker
    {
        public Hired(Hero hero) : base(hero)
        {
        }
    }

    public class Party : Worker
    {
        public Party(Hero hero) : base(hero){}
    }

  
    
  
  
    
    public class SalaryLogic : GameModel
    {
        
    }

 

}
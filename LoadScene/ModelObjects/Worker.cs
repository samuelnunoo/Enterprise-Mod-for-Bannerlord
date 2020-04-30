using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Linq;
using System.Windows.Markup;
using TaleWorlds.ObjectSystem;


namespace LoadScene.ModelObjects
{
    public abstract class Worker : GameModel
    {
        private string _Role;
        public abstract  string Role { get; set; }
        public abstract int Tier { get;  }
        public abstract CharacterObject Character { get; }
        public abstract double Experience { get; }
        
        public abstract double Level { get; }
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

    public class NormalWorker : Worker
    {
        private double _experience;
        private CharacterObject _character;
        private double _happiness;
        private string _role;
        private double _salary;
        private double _stamina;
        private int _tier;
        private double _cost;
        private double _level;

        
        private double _effmult;
        private double _happmult;
        private double _salarymult;
        private double _stammult;
        private double _theftmult;
        private double _expmult;
        
        public void DefaultStats()
        {
            
            this._tier = 0;
            this._level = 1;
            this._happiness = 100 * this._happmult;
            this._cost = 1000 * this._salarymult * ( this._level);
            this._stamina = 100 * this._stammult + (10 * this._level);
            this._role = "Gatherer";
            this._salary = 50 * this._salarymult * this._level;
           


        } 

        public NormalWorker(Hero hero, MultiplierObject obj)
        {
      
            this._character = hero.CharacterObject;
            this.SetMult(obj);
            

        }

        public void SetMult(MultiplierObject obj)
        {
            this._expmult = obj._exp;
            this._happmult = obj._happiness;
            this._salarymult = obj._salary;
            this._effmult = obj._efficiency;
            this._stammult = obj._stamina;
            this._theftmult = obj._theft;

        }

       

        public override double Level => _level;
        public override double Experience => _experience;
        public override CharacterObject Character => _character;
        public override double Happiness => _happiness;

        public override string Role
        {
            get => _role;
            set { }
        }

        public override double Salary => _salary;

        public override double Stamina => _stamina;

        public override int Tier => _tier;

        public override double BaseCost => _cost;

        public override double EfficienceMult => _effmult;

        public override double HappinessMult => _happmult;

        public override double SalaryMult => _salarymult;

        public override double StaminaMult => _stammult;

        public override double TheftMult => _theftmult;

        public override double EXPMult => _expmult;
        
        
    }


    public class test
    {
        public static void RandomWorker()


        {
         
        }
    }
    
   

}
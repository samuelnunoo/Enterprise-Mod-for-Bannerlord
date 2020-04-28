using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;

namespace LoadScene.ModelObjects
{
   
    public struct MultiplierObject {
        public double _salary;
        public readonly double  _exp;
        public double _stamina;
        public double _happiness;
        public double  _efficiency;
        public double _theft;

        public MultiplierObject(double salary, double exp, double stamina, double happiness, double efficiency, double theft)
        {
            this._salary = salary;
            this._exp = exp;
            this._stamina = stamina;
            this._happiness = happiness;
            this._efficiency = efficiency;
            this._theft = theft;
        }

    }
    
    public class Traits: GameModel
    {
        private MultiplierObject _Intuitive;
        private MultiplierObject _Workaholic;
        private MultiplierObject _FastLearner;
        private MultiplierObject _Optimistic;
        private MultiplierObject _Lazy;
        private MultiplierObject _Demanding;
        private MultiplierObject _Pessimistic;
        private MultiplierObject _Efficient;
        private MultiplierObject _Dumb;
        private MultiplierObject _Humble;
        private MultiplierObject _JackofAllTrades;
        private MultiplierObject _Thief;
        private MultiplierObject _Disorganized;
        private MultiplierObject _Terrible;
        
        public Traits()
        {
            this._Intuitive = new MultiplierObject(1.25, 1.25, 1, 1, 1.6, 1);
            this._Workaholic = new MultiplierObject(2,0.75,3,0.75,1,1);
            this._FastLearner = new MultiplierObject(2,3,1,1,1,1);
            this._Optimistic = new MultiplierObject(0.75,1,1,3,1,0.5);
            this._Lazy = new MultiplierObject(1.25,1,0.5,0.75,1,1.5);
            this._Demanding = new MultiplierObject(3,1.5,1,0.75,1,1.5);
            this._Pessimistic = new MultiplierObject(1,0.75,1,0.25,1,1.5);
            this._Efficient = new MultiplierObject(2,1,0.75,1,3,1);
            this._Dumb = new MultiplierObject(0.75,0.25,1.5,1,0.75,0.5);
            this._Humble = new MultiplierObject(0.25,0.75,1,1.25,0.75,0.25);
            this._JackofAllTrades = new MultiplierObject(1.5,1.5,1.5,1.5,1.5,1);
            this._Thief = new MultiplierObject(1,1,1,1,1,5);  
            this._Disorganized = new MultiplierObject(1,0.75,1,1,0.25,1);
            this._Terrible = new MultiplierObject(0.1,0.5,0.5,0.5,0.5,0.5);
            
        }
        
        public MultiplierObject Intuitive
        {
            get => _Intuitive;
        }
        public MultiplierObject Workaholic
        {
            get => _Workaholic;
           
        }
        public MultiplierObject FastLearner => _FastLearner;
        public MultiplierObject Optimistic => _Optimistic;
        public MultiplierObject Lazy => _Lazy;
        public MultiplierObject Demanding => _Demanding;
        public MultiplierObject Pessimistic => _Pessimistic;
        public MultiplierObject Efficient => _Efficient;
        public MultiplierObject Dumb => _Dumb;
        public MultiplierObject Humble => _Humble;
        public MultiplierObject JackofAllTrades => _JackofAllTrades;
        public MultiplierObject Thief => _Thief;
        public MultiplierObject Disorganized => _Disorganized;
        public MultiplierObject Terrible => _Terrible;
    }
    
    public class MultLogic : GameModel
    {
        private List<double> _exp;
        private List<double> _salary;
        private List<double> _efficiency;
        private List<double> _stamina;
        private List<double> _happiness;
        private List<double> _theft;
        
        private MultiplierObject _flatValues;

        
        public MultLogic(MultiplierObject Trait1, MultiplierObject Trait2, MultiplierObject Trait3)
        {

            this._exp = new List<double>();
            this._salary = new List<double>();
            this._efficiency = new List<double>();
            this._efficiency = new List<double>();
            this._stamina = new List<double>();
            this._happiness = new List<double>();
            this._theft = new List<double>();
            
            
            AddValues(Trait1);
            AddValues(Trait2);
            AddValues(Trait3);

        }
        
        public void AddValues(MultiplierObject trait)
        {
            this._exp.Add(trait._exp);
            this._salary.Add(trait._salary);
            this._efficiency.Add(trait._efficiency);
            this._stamina.Add(trait._stamina);
            this._happiness.Add(trait._happiness);
            this._theft.Add(trait._theft);
        }

        public MultiplierObject FlatValues()
        {
            
            double exp = this._exp.Average();
            double salary = this._salary.Average();
            double efficiency = this._efficiency.Average();
            double stamina = this._stamina.Average();
            double happiness = this._happiness.Average();
            double theft = this._theft.Average();
            
            
            this._flatValues = new MultiplierObject(salary,exp,stamina,happiness,efficiency,theft);
            return this._flatValues;




        }
        
    }
    
    

    
    
}
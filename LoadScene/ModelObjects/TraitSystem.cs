using System;
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
        private MultLogic _multLogic;
        private Dictionary<string, MultiplierObject> _Traits;
        public Traits()
        {
            this._Traits = new Dictionary<string, MultiplierObject>();
            this._Traits.Add("Intuitive",new MultiplierObject(1.25, 1.25, 1, 1, 1.6, 1));
            this._Traits.Add("Workaholic", new MultiplierObject(1.25, 1.25, 1, 1, 1.6, 1));
            this._Traits.Add("FastLearner", new MultiplierObject(2,3,1,1,1,1));
            this._Traits.Add("Optimistic",new MultiplierObject(0.75,1,1,3,1,0.5));
            this._Traits.Add("Lazy",new MultiplierObject(1.25,1,0.5,0.75,1,1.5));
            this._Traits.Add("Demanding",new MultiplierObject(3,1.5,1,0.75,1,1.5));
            this._Traits.Add("Pessimistic",new MultiplierObject(1,0.75,1,0.25,1,1.5));
            this._Traits.Add("Efficient",new MultiplierObject(2,1,0.75,1,3,1));
            this._Traits.Add("Dumb",new MultiplierObject(0.75,0.25,1.5,1,0.75,0.5));
            this._Traits.Add("Humble",new MultiplierObject(0.25,0.75,1,1.25,0.75,0.25));
            this._Traits.Add("JackOfAllTrades",new MultiplierObject(1.5,1.5,1.5,1.5,1.5,1));
            this._Traits.Add("Thief",new MultiplierObject(1,1,1,1,1,5));
            this._Traits.Add("Disorganized",new MultiplierObject(1,0.75,1,1,0.25,1));
            this._Traits.Add("Terrible",new MultiplierObject(0.1,0.5,0.5,0.5,0.5,0.5));
      
            
        }
        
        public Dictionary<string, MultiplierObject> getTraits => _Traits;

        public void GetRandom()
        {
            
            MBRandom
            
        }
        
        public static IEnumerable<TValue> RandomValues<TKey, TValue>(IDictionary<TKey, TValue> dict)
        {
            Random rand = new Random();
            List<TValue> values = Enumerable.ToList(dict.Values);
            int size = dict.Count;
            while(true)
            {
                yield return values[rand.Next(size)];
            }
        }
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
            
            var exp = this._exp.Average();
            var  salary = this._salary.Average();
            var efficiency = this._efficiency.Average();
            var stamina = this._stamina.Average();
            var happiness = this._happiness.Average();
            var theft = this._theft.Average();
            
            
            this._flatValues = new MultiplierObject(salary,exp,stamina,happiness,efficiency,theft);
            return this._flatValues;




        }
        
    }
    
    

    
    
}
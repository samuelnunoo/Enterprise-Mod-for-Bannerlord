using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
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
        public string _name;

        public MultiplierObject(string name,double salary, double exp, double stamina, double happiness, double efficiency, double theft)
        {
            this._salary = salary;
            this._exp = exp;
            this._stamina = stamina;
            this._happiness = happiness;
            this._efficiency = efficiency;
            this._theft = theft;
            this._name = name;
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
        private string _names;
        
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
            this._names = String.Empty; 
            
            
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
            this._names += trait._name + " ";

        }

        public MultiplierObject FlatValues()
        {
            
            var exp = this._exp.Average();
            var  salary = this._salary.Average();
            var efficiency = this._efficiency.Average();
            var stamina = this._stamina.Average();
            var happiness = this._happiness.Average();
            var theft = this._theft.Average();
            
            
            this._flatValues = new MultiplierObject(this._names,salary,exp,stamina,happiness,efficiency,theft);
            return this._flatValues;




        }
        
    }
    
    public  class  Traits: GameModel
    {
        private string _TraitNames;
        private Dictionary<string, MultiplierObject> _Traits;
        public Traits()
        {
            this._Traits = new Dictionary<string, MultiplierObject>();
            this._Traits .Add("Intuitive",new MultiplierObject("Intuitive",1.25, 1.25, 1, 1, 1.6, 1));
            this._Traits .Add("Workaholic", new MultiplierObject("Workaholic",1.25, 1.25, 1, 1, 1.6, 1));
            this._Traits .Add("FastLearner", new MultiplierObject("FastLearner",2,3,1,1,1,1));
            this._Traits .Add("Optimistic",new MultiplierObject("Optimistic",0.75,1,1,3,1,0.5));
            this._Traits .Add("Lazy",new MultiplierObject("Lazy",1.25,1,0.5,0.75,1,1.5));
            this._Traits .Add("Demanding",new MultiplierObject("Demanding",3,1.5,1,0.75,1,1.5));
            this._Traits .Add("Pessimistic",new MultiplierObject("Pessimistic",1,0.75,1,0.25,1,1.5));
            this._Traits .Add("Efficient",new MultiplierObject("Efficient",2,1,0.75,1,3,1));
            this._Traits .Add("Dumb",new MultiplierObject("Dumb",0.75,0.25,1.5,1,0.75,0.5));
            this._Traits .Add("Humble",new MultiplierObject("Humble",0.25,0.75,1,1.25,0.75,0.25));
            this._Traits .Add("JackOfAllTrades",new MultiplierObject("JackOfAllTrades",1.5,1.5,1.5,1.5,1.5,1));
            this._Traits .Add("Thief",new MultiplierObject("Thief",1,1,1,1,1,5));
            this._Traits .Add("Disorganized",new MultiplierObject("Disorganized",1,0.75,1,1,0.25,1));
            this._Traits .Add("Terrible",new MultiplierObject("Terrible",0.1,0.5,0.5,0.5,0.5,0.5));
            
        }
        
        public Dictionary<string, MultiplierObject> getTraits => _Traits;

        public MultiplierObject GetRandom()
        {
            var random = new Random();
            var index = random.Next(this._Traits.Count);
            KeyValuePair<string, MultiplierObject> pair = this._Traits.ElementAt(index);
            return pair.Value;


        }


        public MultiplierObject GetRandom3()
        {
            List<MultiplierObject> traits = new List<MultiplierObject>();


            for (var i = 0; i < 3; i++)
            {
                var trait = this.GetRandom();
                if (traits.Contains(trait))
                {
                    i -= 1;
                    continue;
                }

                traits.Add(trait);

            }
            

            var logic = new MultLogic(traits[0],traits[1],traits[2]);
            return logic.FlatValues();
        }
        
    
    }
    
   
}
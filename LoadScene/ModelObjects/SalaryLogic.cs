using TaleWorlds.Core;

namespace LoadScene.ModelObjects
{
    public class PriceLogicHandler : GameModel
    {
        public double GetSalary( double Level)
        {
            var salary = (3500 / 100) * Level;
            return salary;
        }

        public double GetBaseCost( double Level)
        {
            var price = 3500 * Level;
            return price;
        }
    }
    
    
}
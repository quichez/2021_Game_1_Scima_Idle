using System;
using System.Linq;

//Weighted Random Values
namespace WeightedChance
{
    public class WeightedParam
    {
        public Action Func { get; }
        public double Ratio { get; }

        public WeightedParam(Action func, double ratio)
        {
            Func = func;
            Ratio = ratio;
        }
    }

    public class WeightedRoll
    {
        public WeightedParam[] Parameters { get; }
        private Random _r;

        public double RatioSum { get { return Parameters.Sum(p => p.Ratio); } }

        public WeightedRoll(params WeightedParam[] parameters)
        {
            Parameters = parameters;
            _r = new Random();
        }

        public void Execute()
        {
            double num = _r.NextDouble() * RatioSum;
            foreach (var param in Parameters)
            {
                num -= param.Ratio;

                if (!(num <= 0))
                    continue;
                param.Func();
                return;
            }
        }
    }
}

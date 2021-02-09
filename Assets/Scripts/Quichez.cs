using System;

namespace Quichez
{
    public struct BigNumber<T>
    {
        public T Mantissa { get; private set; }
        public T Exponent { get; private set; }

        public BigNumber(T mant, T exp)
        {
            Mantissa = mant;
            Exponent = exp;
        }


        public BigNumber<T> ReturnSomething()
        {
            return new BigNumber<T>();
        }
    }
    
}

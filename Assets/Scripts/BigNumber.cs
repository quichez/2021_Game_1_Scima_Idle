using System;
using UnityEngine;

public class BigNumber
{
    public double Mantissa { get; private set; }
    public double Exponent { get; private set; }

    /// <summary>
    /// Return a new rounded BigNumber.
    /// </summary>
    public BigNumber Rounded => Round(this);
    
    /// <summary>
    /// Short-hand for creating a BigNumber of zero.
    /// </summary>
    public static BigNumber Zero => new BigNumber(0,0);
    
    public BigNumber()
    {
        Mantissa = 0;
        Exponent = 0;
    }

    public BigNumber(double number)
    {
        Mantissa = number;
        Exponent = 0;
        Fix();
    }

    public BigNumber(double mantissa, double exponent)
    {
        Mantissa = mantissa;
        Exponent = exponent;
        Fix();
    }

    public BigNumber(BigNumber original)
    {
        Mantissa = original.Mantissa;
        Exponent = original.Exponent;
        Fix();

    }
    /// <summary>
    /// Normalize and round the constructor. Only called in constructor.
    /// </summary>
    private void Fix()
    {
        if (Mantissa != 0.0 && (Math.Abs(Mantissa) >= 10.0 || Math.Abs(Mantissa) < 1.0))
        {
            double exp = Math.Floor(Math.Log10(Math.Abs(Mantissa)));
            Mantissa = Mantissa / Math.Pow(10.0, exp);
            Exponent += exp;
        }        
    }

    private BigNumber Round(BigNumber number)
    {
        double mant = Math.Round(number.Mantissa * Math.Pow(10.0, Math.Max(-14.0, Math.Min(number.Exponent, 14.0))));
        mant = mant / Math.Pow(10.0, Math.Max(-14.0, Math.Min(number.Exponent, 14.0)));
        return new BigNumber(mant, number.Exponent);
    }

    public static BigNumber RoundBigNumber(BigNumber number)
    {
        number.Mantissa = Math.Round(number.Mantissa * Math.Pow(10.0, Math.Min(Math.Abs(number.Exponent), 14.0)));
        number.Mantissa = number.Mantissa / Math.Pow(10.0, Math.Min(Math.Abs(number.Exponent), 14.0));
        return new BigNumber(number.Mantissa, number.Exponent);
    }

    /// <summary>
    /// 4-digit string representation of BigNumber.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (this == Zero)
            return "0";
        if (Exponent < 0)
            return Math.Ceiling(Mantissa).ToString();
        else if (Exponent <= 3 && Exponent >= 0)
            return Math.Round(Mantissa * Math.Pow(10.0, Exponent), 0).ToString();
        else if (Exponent >= 4 && Exponent <= 5)
            return (Math.Round(Mantissa * Math.Pow(10.0, 3.0)) / Math.Pow(10.0, 6 - Exponent)).ToString() + "K";
        else if (Exponent >= 6 && Exponent <= 8)
            return (Math.Round(Mantissa * Math.Pow(10.0, 3.0)) / Math.Pow(10.0, 9 - Exponent)).ToString() + "M";
        else
            return Math.Round(Mantissa, 4).ToString("F3") + "e" + Exponent.ToString();
    }    

    #region Overrides
    public override bool Equals(object obj)
    {
        var number = obj as BigNumber;
        return number != null &&
               Mantissa == number.Mantissa &&
               Exponent == number.Exponent;
    }

    public override int GetHashCode()
    {
        var hashCode = 779356851;
        hashCode = hashCode * -1521134295 + Mantissa.GetHashCode();
        hashCode = hashCode * -1521134295 + Exponent.GetHashCode();
        return hashCode;
    }
    #endregion

    public static BigNumber operator +(BigNumber current, BigNumber amount)
    {
        double mant;
        double exp = current.Exponent - amount.Exponent;

        if (Math.Abs(exp) > 15.0)
        {
            if (current.Exponent > amount.Exponent)
                return new BigNumber(current);
            else
                return new BigNumber(amount);
        }
        else if (exp < 0)
        {
            mant = current.Mantissa + amount.Mantissa * Math.Pow(10.0, -exp);
            return new BigNumber(mant, current.Exponent);
        }
        else
        {
            mant = current.Mantissa * Math.Pow(10.0, exp) + amount.Mantissa;
            return new BigNumber(mant, amount.Exponent);
        }
    }

    public static BigNumber operator -(BigNumber from, BigNumber amount)
    {
        double mant;
        double exp = from.Exponent - amount.Exponent;

        if (Math.Abs(exp) > 15.0)
        {
            if (from.Exponent > amount.Exponent)
                return new BigNumber(from);
            else
                return new BigNumber(-amount);
        }            
        else if (exp < 0)
        {
            mant = from.Mantissa - amount.Mantissa * Math.Pow(10.0, -exp);
            return new BigNumber(mant, from.Exponent);
        }
        else
        {
            mant = from.Mantissa * Math.Pow(10.0, exp) - amount.Mantissa;
            return new BigNumber(mant, amount.Exponent);
        }
    }

    public static BigNumber operator *(BigNumber factorA, BigNumber factorB)
    {
        double mant = factorA.Mantissa * factorB.Mantissa;
        double exp = factorA.Exponent + factorB.Exponent;

        return new BigNumber(mant, exp);
    }

    public static BigNumber operator /(BigNumber dividend, BigNumber divisor)
    {
        if (dividend.Mantissa == 0)
            return Zero;

        // Figure out which method below preserves accuracy best
        double mant;
        double exp = dividend.Exponent - divisor.Exponent;

        if (Math.Abs(exp) > 15.0)
            throw new ArgumentException("The difference in magnitude is too great.");
        if (exp > 0.0)
            mant = (dividend.Mantissa * Math.Pow(10.0, exp) / divisor.Mantissa); // Method 1
        else if (exp < 0.0)
            mant = dividend.Mantissa / (divisor.Mantissa * Math.Pow(10.0, -exp)); // Method 2
        else
            mant = dividend.Mantissa / divisor.Mantissa;

        return new BigNumber(mant, 0.0);
    }

    public static BigNumber operator +(BigNumber bigNum, float amount)
    {
        BigNumber temp = new BigNumber(amount);
        return bigNum + temp;

    }

    public static BigNumber operator *(BigNumber bigNum, float factor)
    {
        double mant = bigNum.Mantissa * factor;
        return new BigNumber(mant, bigNum.Exponent);
    }

    public static bool operator ==(BigNumber a, BigNumber b)
    {
        return (a.Mantissa == b.Mantissa && a.Exponent == b.Exponent);
    }

    public static bool operator !=(BigNumber a, BigNumber b)
    {
        return (a.Mantissa != b.Mantissa || a.Exponent != b.Exponent);
    }

    public static bool operator >(BigNumber a, BigNumber b)
    {        
        if (a.Exponent > b.Exponent)        //e3 > e2
        {
            if (a.Mantissa < b.Mantissa)
            {
                if (a.Mantissa < 0.0)//-1e3 < 2e2{
                {
                    return false;
                }
                else //1e3 > 2e2
                    return true;
            }
            else
            {
                if (a.Mantissa > 0.0)   //1e3 > 2e2
                    return true;
                else  //-1e3 < -2e2
                    return false;
            }
        }
        else if(a.Exponent < b.Exponent) //e-1 < e0
        {
            if (a.Mantissa < b.Mantissa) //
            {
                if (a.Mantissa < 0.0)  //-2e2 > -1e3, -2e-1 > -1e0?
                    return true;
                else //1e2 < 2e3
                    return false;   
            }
            else // a >= b
            {
                if (a.Mantissa < 0.0)  //-1e-1 > -2e0,  -1e-1 < 0e0
                    return true;
                else
                {
                    if (b.Mantissa > 0) //  2e-1 < 1e0
                        return false;
                    else // 1e-1 > -2e0                        
                        return true;   //1e2 < 2e3  8e-1 < 0e0
                }
            }
        }
        else
        {
            return a.Mantissa > b.Mantissa;
        }

    }

    public static bool operator <(BigNumber a, BigNumber b)
    {        
        if (a.Exponent < b.Exponent)
        {
            if (a.Exponent >= 0.0) //e0 < e0
            {
                if (a.Mantissa < b.Mantissa)
                {
                    if (b.Mantissa <= 0.0)
                        return false;
                    else
                        return true;
                }
                else // a.m >= b.m
                {
                    if (a.Mantissa > 0.0)
                        return true;
                    else
                    {
                        //Debug.Log("here!");
                        return false;
                    }
                }

            }
            else   //e-1 vs e-2
            {
                if(a.Mantissa < b.Mantissa)
                {
                    if (b.Mantissa <= 0.0)  //-2e-1 vs -1e-2
                        return true;
                    else
                    {
                        //Debug.Log("here2!");
                        return false;
                    }
                }
                else
                {
                    if (a.Mantissa < 0.0) // -1e-1 vs -2e-2
                        return true;
                    else
                    {
                        //Debug.Log("here3!");
                        return false;
                    }
                }
            }
        }
        else  // e2 > e1
        {
            if (a.Mantissa < b.Mantissa) // 8e2 vs 9e1
            {
                if (a.Mantissa > 0.0)
                    return false;
                else
                    return true;
            }
            else // 9e2 vs 8e1
            {
                if (a.Mantissa < 0.0)
                    return true;
                else
                    return false;
            }
        } 
    }

    public static bool operator >=(BigNumber a, BigNumber b)
    {
        return (a > b || a == b);
            
    }

    public static bool operator <=(BigNumber a, BigNumber b)
    {
        return (a < b || a == b);
                 
    }

    public static BigNumber operator -(BigNumber number)
    {
        return new BigNumber(number * -1.0f);
    }

    public static float ReturnAsFloat(BigNumber number)
    {
        if (number.Mantissa == 0)
            return 0.0f;

        if (number.Exponent >= 7)
            throw new System.ComponentModel.WarningException("Exponents larger than 7 will return floats with reduced accuracy.");
        return (float)(number.Mantissa * Math.Pow(10.0, number.Exponent));
    }

    public static BigNumber Max(BigNumber minimum, BigNumber current)
    {        
        if (current < minimum)
        {
            
            return new BigNumber(minimum);
        }
        else
            return new BigNumber(current);
    }

    public static BigNumber Clamp(BigNumber minimum, BigNumber maximum, BigNumber clampedNumber)
    {
        
        if (clampedNumber > maximum)
            return new BigNumber(maximum);
        else if (clampedNumber < minimum)
            return new BigNumber(minimum);
        else
            return new BigNumber(clampedNumber);
    }
}

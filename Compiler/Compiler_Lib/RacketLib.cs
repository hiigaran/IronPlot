﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CompilerLib
{
    public static class FunctionLib
    {
        public static ObjBox Plus(List<RacketNum> args)
        {
            
            if (args.Count == 0) // plust can take zero args
                return new ObjBox(new RacketInt(0), typeof(RacketInt));

            var sum = 0;

            Type t = args[0].GetType();

            for (int i = 0; i < args.Count; i++)
            {
                RacketNum num = args[i];
                if (num.GetType() != t)
                    throw new RuntimeException("all numbers in plus must be of same type");
                sum = sum + num.getValue();     
            }
            if (t == typeof(RacketInt))
            {
                return new ObjBox(new RacketInt(sum), typeof(RacketInt));
            }
            else if (t == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(sum), typeof(RacketFloat));
            }
            else if (t == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(sum), typeof(RacketComplex));
            }
            else
                throw new RuntimeException("do not understand type of RacketNum in plus");
        }

        public static ObjBox Mult(List<RacketNum> args)
        {

            if (args.Count == 0) // plust can take zero args
                return new ObjBox(new RacketInt(1), typeof(RacketInt));

            var sum = 1;

            Type t = args[0].GetType();

            for (int i = 0; i < args.Count; i++)
            {
                RacketNum num = args[i];
                if (num.GetType() != t)
                    throw new RuntimeException("all numbers in plus must be of same type");
                sum = sum * num.getValue();
            }
            if (t == typeof(RacketInt))
            {
                return new ObjBox(new RacketInt(sum), typeof(RacketInt));
            }
            else if (t == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(sum), typeof(RacketFloat));
            }
            else if (t == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(sum), typeof(RacketComplex));
            }
            else
                throw new RuntimeException("do not understand type of RacketNum in mult");
        }

        public static ObjBox Sub(List<RacketNum> args)
        {
            if (args.Count < 1)
                throw new RuntimeException("Subtraction expects at least 1 argument");

            Type t = args[0].GetType();
            var sum = args[0].getValue();

            if (args.Count == 1)
                sum = sum * -1;

            for (int i = 0; i < args.Count; i++)
            {
                RacketNum num = args[i];
                if (num.GetType() != t)
                    throw new RuntimeException("all numbers in plus must be of same type");
                sum = sum - num.getValue();
            }

            if (t == typeof(RacketInt))
            {
                return new ObjBox(new RacketInt(sum), typeof(RacketInt));
            }
            else if (t == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(sum), typeof(RacketFloat));
            }
            else if (t == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(sum), typeof(RacketComplex));
            }
            else
                throw new RuntimeException("do numt understand RacketNum type in subtraction");
        }

        public static ObjBox Div(List<RacketNum> args)
        {
            if (args.Count < 1)
                throw new RuntimeException("Division expects at least 1 argument");

            Type t = args[0].GetType();
            var sum = args[0].getValue();

            if (args.Count == 1)
                sum = 1 / sum;

            for (int i = 0; i < args.Count; i++)
            {
                RacketNum num = args[i];
                if (num.GetType() != t)
                    throw new RuntimeException("all numbers in plus must be of same type");
                sum = sum / num.getValue();
            }

            if (t == typeof(RacketInt))
            {
                return new ObjBox(new RacketInt(sum), typeof(RacketInt));
            }
            else if (t == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(sum), typeof(RacketFloat));
            }
            else if (t == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(sum), typeof(RacketComplex));
            }
            else
                throw new RuntimeException("do numt understand RacketNum type in division");
        }

        public static ObjBox Mod(List<RacketNum> args)
        {
            if (args.Count != 2)
                throw new RuntimeException("Modulo expects 2 arguments");

            Type t = args[0].GetType();
            Type t2 = args[1].GetType();
            if (t2 != t)
                throw new RuntimeException("types must be the same in Mod");

            if (t == typeof(RacketInt))
            {
                return new ObjBox(new RacketInt(args[0].getValue() % args[1].getValue()), typeof(RacketInt));
            }
            else if (t == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(args[0].getValue() % args[1].getValue()), typeof(RacketFloat));
            }
            else if (t == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(args[0].getValue() % args[1].getValue()), typeof(RacketComplex));
            }
            else
                throw new RuntimeException("do numt understand RacketNum type in subtraction");
        }

        public static ObjBox LessThan(List<RacketNum> args)
        {
            if (args.Count < 2)
                throw new RuntimeException("< expects at least 2 arguments");

            Type t = args[0].GetType();
            var min = args[0].getValue();
            Boolean tru = new Boolean();
            tru = false;
            for (int i = 0; i < args.Count; i++)
            {
                RacketNum num = args[i];
                if (num.GetType() != t)
                    throw new RuntimeException("all numbers in < must be of same type");
                if (num.getValue() <= min)
                    return new ObjBox(tru, typeof(Boolean));
                min = num.getValue();
            }
            tru = true;
            return new ObjBox(tru, typeof(Boolean));
        }

        public static ObjBox LessThanEqual(List<RacketNum> args)
        {
            if (args.Count < 2)
                throw new RuntimeException("<= expects at least 2 arguments");

            Type t = args[0].GetType();
            var min = args[0].getValue();
            Boolean tru = new Boolean();
            tru = false;
            for (int i = 0; i < args.Count; i++)
            {
                RacketNum num = args[i];
                if (num.GetType() != t)
                    throw new RuntimeException("all numbers in <= must be of same type");
                if (num.getValue() < min)
                    return new ObjBox(tru, typeof(Boolean));
                min = num.getValue();
            }
            tru = true;
            return new ObjBox(tru, typeof(Boolean));
        }

        public static ObjBox GreaterThan(List<RacketNum> args)
        {
            if (args.Count < 2)
                throw new RuntimeException("> expects at least 2 arguments");

            Type t = args[0].GetType();
            var max = args[0].getValue();
            Boolean tru = new Boolean();
            tru = false;
            for (int i = 0; i < args.Count; i++)
            {
                RacketNum num = args[i];
                if (num.GetType() != t)
                    throw new RuntimeException("all numbers in > must be of same type");
                if (num.getValue() >= max)
                    return new ObjBox(tru, typeof(Boolean));
                max = num.getValue();
            }
            tru = true;
            return new ObjBox(tru, typeof(Boolean));
        }

        public static ObjBox GreaterThanEqual(List<RacketNum> args)
        {
            if (args.Count < 2)
                throw new RuntimeException(">= expects at least 2 arguments");

            Type t = args[0].GetType();
            var max= args[0].getValue();
            Boolean tru = new Boolean();
            tru = false;
            for (int i = 0; i < args.Count; i++)
            {
                RacketNum num = args[i];
                if (num.GetType() != t)
                    throw new RuntimeException("all numbers in >= must be of same type");
                if (num.getValue() > max)
                    return new ObjBox(tru, typeof(Boolean));
                max = num.getValue();
            }
            tru = true;
            return new ObjBox(tru, typeof(Boolean));
        }

        public static ObjBox Map(FunctionHolder function, List<RacketPair> lists)
        {
            List<ObjBox> returnedValues = new List<ObjBox>();
            List<Object> args = new List<Object>();
            bool restNull = false;
            int listLength = -1;
            while(! restNull)
            {
                args.Clear();
                for(int i = 0; i < lists.Count; i++)
                {
                    if (listLength == -1)
                        listLength = lists[i].length();
                    if (lists[i].length() != listLength)
                        throw new RuntimeException("Lists must be of same length");

                    args.Add(lists[i].car());
                    ObjBox rest = lists[i].cdr();

                    if (rest.getType() == typeof(voidObj))
                    {
                        restNull = true;
                    }
                    else
                    {
                        lists[i] = (RacketPair)rest.getObj();
                    }
                }
                returnedValues.Add(function.invoke(args));
            }
            
            return new ObjBox(new RacketPair(returnedValues), typeof(RacketPair));
        }
    }

    public interface RacketNum
    {
        dynamic getValue();
        ObjBox Plus(RacketNum other);
        ObjBox Sub(RacketNum other);
        ObjBox Mult(RacketNum other);
        ObjBox Div(RacketNum other);
        ObjBox Mod(RacketNum other);

        Boolean RealQ(RacketNum other);
        Boolean ComplexQ(RacketNum other);
        Boolean FloatQ(RacketNum other);
        Boolean IntegerQ(RacketNum other);
    }

    public class RacketInt : RacketNum
    {
        public int value { private set; get; }

        public RacketInt(int _value)
        {
            value = _value;
        }

        public dynamic getValue()
        {
            return value;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().GetInterfaces().Contains(typeof(RacketNum)))
            {
                if(obj.GetType() == typeof(RacketInt))
                {
                    return this.value == ((RacketInt) obj).value;
                }
                if(obj.GetType() == typeof(RacketFloat))
                {
                    return this.value == ((RacketFloat) obj).value;
                }
                if(obj.GetType() == typeof(RacketComplex))
                {
                    return this.value == ((RacketComplex) obj).value;
                }
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static implicit operator int(RacketInt m)
        {
            return m.value;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public ObjBox Plus(RacketNum other)
        {
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketInt(value + ((RacketInt)other).value), typeof(RacketInt));
            }
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(value + ((RacketFloat)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(value + ((RacketComplex)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Sub(RacketNum other)
        {
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketInt(value - ((RacketInt)other).value), typeof(RacketInt));
            }
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(value - ((RacketFloat)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(value - ((RacketComplex)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Mult(RacketNum other)
        {
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketInt(value * ((RacketInt)other).value), typeof(RacketInt));
            }
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(value * ((RacketFloat)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(value * ((RacketComplex)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Div(RacketNum other)
        {
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketFloat(((Double) value) / ((RacketInt)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(value / ((RacketFloat)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox( new RacketComplex(value / ((RacketComplex)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Mod(RacketNum other)
        {
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketInt(value % ((RacketInt)other).value), typeof(RacketInt));
            }
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(value % ((RacketFloat)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketComplex))
            {
                throw new InvalidOperationException("Cannot use mod operation with a complex number");
            }
            throw new NotImplementedException();
        }

        bool RacketNum.RealQ(RacketNum other)
        {
            return true;
        }

        bool RacketNum.ComplexQ(RacketNum other)
        {
            return false;
        }

        bool RacketNum.FloatQ(RacketNum other)
        {
            return false;
        }

        bool RacketNum.IntegerQ(RacketNum other)
        {
            return true;
        }
    }

    public class RacketFloat : RacketNum
    {
        public Double value { private set; get; }
        
        public RacketFloat(Double _value)
        {
            value = _value;
        }

        public dynamic getValue()
        {
            return value;
        }

        public static implicit operator Double(RacketFloat m)
        {
            return m.value;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().GetInterfaces().Contains(typeof(RacketNum)))
            {
                if (obj.GetType() == typeof(RacketInt))
                {
                    return this.value == ((RacketInt)obj).value;
                }
                if (obj.GetType() == typeof(RacketFloat))
                {
                    return this.value == ((RacketFloat)obj).value;
                }
                if (obj.GetType() == typeof(RacketComplex))
                {
                    return this.value == ((RacketComplex)obj).value;
                }
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public ObjBox Plus(RacketNum other)
        {
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(value + ((RacketFloat)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketFloat(value + ((RacketInt)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(value + ((RacketComplex)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Sub(RacketNum other)
        {
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(value - ((RacketFloat)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketFloat(value - ((RacketInt)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(value - ((RacketComplex)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Mult(RacketNum other)
        {
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(value * ((RacketFloat)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketFloat(value * ((RacketInt)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(value * ((RacketComplex)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Div(RacketNum other)
        {
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketFloat(value / ((RacketFloat)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketFloat(value / ((RacketInt)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(value / ((RacketComplex)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Mod(RacketNum other)
        {
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox( new RacketFloat(value % ((RacketFloat)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketFloat(value % ((RacketInt)other).value), typeof(RacketFloat));
            }
            if (other.GetType() == typeof(RacketComplex))
            {
                throw new InvalidOperationException("Cannot use mod operation with a complex number");
            }
            throw new NotImplementedException();
        }

        public bool RealQ(RacketNum other)
        {
            return true;
        }

        public bool ComplexQ(RacketNum other)
        {
            return false;
        }

        public bool FloatQ(RacketNum other)
        {
            return true;
        }

        public bool IntegerQ(RacketNum other)
        {
            return false;
        }
    }

    public class RacketComplex : RacketNum
    {
        public Complex value { private set; get; }

        public RacketComplex(Complex _value)
        {
            value = _value;
        }

        public dynamic getValue()
        {
            return value;
        }

        public static implicit operator Complex(RacketComplex m)
        {
            return m.value;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().GetInterfaces().Contains(typeof(RacketNum)))
            {
                if (obj.GetType() == typeof(RacketInt))
                {
                    return this.value == ((RacketInt)obj).value;
                }
                if (obj.GetType() == typeof(RacketFloat))
                {
                    return this.value == ((RacketFloat)obj).value;
                }
                if (obj.GetType() == typeof(RacketComplex))
                {
                    return this.value == ((RacketComplex)obj).value;
                }
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public ObjBox Plus(RacketNum other)
        {
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(value + ((RacketComplex)other).value), typeof(RacketComplex));
            }
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketComplex(value + ((RacketInt)other).value), typeof(RacketComplex));
            }
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketComplex(value + ((RacketFloat)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Sub(RacketNum other)
        {
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(value - ((RacketComplex)other).value), typeof(RacketComplex));
            }
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketComplex(value - ((RacketInt)other).value), typeof(RacketComplex));
            }
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketComplex(value - ((RacketFloat)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Mult(RacketNum other)
        {
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(value * ((RacketComplex)other).value), typeof(RacketComplex));
            }
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketComplex(value * ((RacketInt)other).value), typeof(RacketComplex));
            }
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketComplex(value * ((RacketFloat)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Div(RacketNum other)
        {
            if (other.GetType() == typeof(RacketComplex))
            {
                return new ObjBox(new RacketComplex(value / ((RacketComplex)other).value), typeof(RacketComplex));
            }
            if (other.GetType() == typeof(RacketInt))
            {
                return new ObjBox(new RacketComplex(value / ((RacketInt)other).value), typeof(RacketComplex));
            }
            if (other.GetType() == typeof(RacketFloat))
            {
                return new ObjBox(new RacketComplex(value / ((RacketFloat)other).value), typeof(RacketComplex));
            }
            throw new NotImplementedException();
        }

        public ObjBox Mod(RacketNum other)
        {
            throw new InvalidOperationException("Cannot use mod operation with a complex number");
        }

        public bool RealQ(RacketNum other)
        {
            return false;
        }

        public bool ComplexQ(RacketNum other)
        {
            return true;
        }

        public bool FloatQ(RacketNum other)
        {
            return false;
        }

        public bool IntegerQ(RacketNum other)
        {
            return false;
        }
    }

    public class RacketPair
    {
        private ObjBox value;
        private ObjBox rest;
        private Boolean Null;
        private int Length;

        public RacketPair(ObjBox _value, ObjBox _rest)
        {
            value = _value;
            rest = _rest;
            Null = false;
            if (_rest.getType() == typeof(voidObj))
                Length = 1;
            else
                Length = 2;
        }

        public RacketPair()
        {
            value = null;
            rest = null;
            Null = true;
            Length = 0;
        }

        public Boolean isNull()
        {
            return Null || value.getType() == typeof(voidObj);
        }

        public int length()
        {
            return Length;
        }

        public RacketPair(List<ObjBox> list)
        {
            if (list.Count == 0)
            {
                value = null;
                rest = null;
                Null = true;
                Length = 0;
                return;
            }
            value = list[0];
            Null = false;
            list.RemoveAt(0);
            Length = list.Count;
            rest = new ObjBox(new RacketPair(list), typeof(RacketPair));
        }

        public ObjBox car()
        {
            if (Null)
            {
                throw new RuntimeException("Contract violation car excpected pair was given null");
            }
            return value;
        }

        public ObjBox cdr()
        {
            if (Null)
            {
                throw new RuntimeException("Contract violation cdr excpected pair was given null");
            }
            return rest;
        }

        public override String ToString()
        {
            String lhs;
            String rhs;

            if (value.getType() == typeof(RacketPair))
            {
                lhs = ((RacketPair)value.getObj()).printList();
            }
            else { lhs = value.getObj().ToString(); }

            if (rest.getType() == typeof(RacketPair))
            {
                rhs = ((RacketPair)rest.getObj()).printList();
            }
            else 
            {
                if (rest.getType() == typeof(voidObj))
                {
                    return lhs;
                }
                rhs = rest.getObj().ToString(); 
            }

            return "(" + lhs + " " +  rhs + ")";
        }   

        public String printList()
        {
            String lhs;
            String rhs;
            if (value == null)
                return "";

            else if (value.getType() == typeof(RacketPair))
            {
                lhs = ((RacketPair)value.getObj()).printList();
            }
            else { lhs = value.getObj().ToString(); }
            if (rest == null)
                rhs = "";

            else if (rest.getType() == typeof(RacketPair))
            {
                rhs = ((RacketPair)rest.getObj()).printList();
            }
            else 
            {
                if (rest.getType() == typeof(voidObj))
                {
                    return lhs;
                } 
                rhs = rest.getObj().ToString();
            }

            return lhs + " " +  rhs;
        }
    }

    public class primeGener : System.Collections.IEnumerable
    {
        static bool visited = true;

        bool[] seive;
        int current;
        int max;
        bool done;

        // a generator that produces prime numbers
        public primeGener(int _max = 101)
        {
            max = _max;
            done = false;
            seive = new bool[max];
            current = 2;
        }

        public int getNext()
        {
            if (done)
            {
                return -1;
            }
            int ret = current;
            int visit = 2;
            while ( (visit*current) < max)
            {
                seive[visit*current] = visited;
                visit += 1;
            }

            current += 1;
            while (seive[current] == visited)
            {
                current += 1;
                if (current == max)
                {
                    done = true;
                    break;
                }
            }

            return ret;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            while (true)
            {
                int ret = getNext();
                if (done)
                {
                    yield return ret;
                    break;
                }
                yield return ret;
            }
        }
    }
}

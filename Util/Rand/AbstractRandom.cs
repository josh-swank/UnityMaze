using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.Rand
{
    public abstract class AbstractRandom : IRandom
    {

        protected AbstractRandom(ulong? seed = null)
        {
            Seed = seed;
        }

        public ulong? Seed { get; }

        public abstract float NextUnitF();
        public abstract double NextUnitD();

        public virtual uint NextUInt32()
        {
            return (uint)(NextUnitD() * ((ulong)uint.MaxValue + 1));
        }

        public virtual ulong NextUInt64()
        {
            return ((ulong)NextUInt32() << 32) + NextUInt32();
        }

        public virtual int NextInt32()
        {
            return NextInt32(int.MaxValue);
        }
        public virtual int NextInt32(int max)
        {
            if (max <= 0)
                throw new ArgumentException("Max must be greater than zero.");
            return (int)(NextUnitD() * max);
        }

        public virtual int NextInt32(int min, int max)
        {
            if (max <= min)
                throw new ArgumentException("Max must be greater than min.");
            return (int)(NextUnitD() * ((long)max - min) + min);
        }

        protected static uint SeedAsUInt32(ulong seed)
        {
            // Shrink long to fit int
            return (uint)(seed / (ulong.MaxValue / uint.MaxValue));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.Rand
{
    public class FclRandom : AbstractRandom
    {
        private readonly Random _random;

        public FclRandom(ulong? seed = null) : base(seed)
        {
            _random = seed.HasValue ? new Random((int)SeedAsUInt32(seed.Value)) : new Random();
        }

        public override float NextUnitF()
        {
            return (float)NextUnitD();
        }

        public override double NextUnitD()
        {
            return _random.NextDouble();
        }
    }
}

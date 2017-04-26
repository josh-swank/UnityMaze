using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.Rand
{
    public interface IRandom
    {

        ulong? Seed { get; }

        float NextUnitF();
        double NextUnitD();

        uint NextUInt32();
        ulong NextUInt64();

        int NextInt32();
        int NextInt32(int max);
        int NextInt32(int min, int max);
    }
}

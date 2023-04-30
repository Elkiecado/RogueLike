using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueSharp1.Interfaces
{
    public interface IActor
    {
        string Name { get; set; }
        int Awaraness { get; set; }
    }
}

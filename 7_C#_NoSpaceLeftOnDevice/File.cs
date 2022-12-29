using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode7
{
    class File
    {
        public string Name { get; set; }
        public int Size { get; set; }

        public File(string name, int size)
        {
            this.Name = name;
            this.Size = size;
        }
    }
}

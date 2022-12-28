using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode7
{
    class Directory
    {
        public string Name { get; set; }
        public Directory? ParentDirectory { get; set; }
        public List<Directory> SubDirectories { get; set; } = new List<Directory>();
        public List<File> Files { get; set; } = new List<File>();
        public int Size { get; set; }


        public Directory(string name)
        {
            this.Name = name;

        }

        public int GetDirectorySize()
        {
            if (Files == null || Files.Count() == 0)
            {
                return 0;
            }

            int size = 0;
            for (int i = 0; i < Files.Count; i++)
            {
                size += Files[i].Size;
            }
            return size;
        }


        public void PrintFilesRecursively()
        {
            List<File> files = null;
            List<Directory> subDirs = null;

            // First, process all the files directly under this folder
            files = this.Files;
            foreach (var f in files)
            {
                Console.WriteLine($"Filename: {f.Name}\tFilesize: {f.Size}");
            }

            // Now find all the subdirectories under this directory.
            subDirs = this.SubDirectories;

            if (!subDirs.Any())
            {
                return;
            }
            foreach (var dir in subDirs)
            {
                // Resursive call for each subdirectory.
                dir.PrintFilesRecursively();
            }
        }

    }

}

using AdventOfCode7;
using Directory = AdventOfCode7.Directory; //Using my Directory Class, not the standard one. 
using File = AdventOfCode7.File; // 


//Reading Commands from File
List<string> commands = ReadCommands();

//Constructing DirectoryTree
Directory root = new ("/");
Directory currentdir = root;
var allDirs = new List<Directory> { root };
ConstructDirectoryTree();

//Part 1
var sumDirSizeUnder100k = allDirs.Select(GetDirectorySizeRecursively).Where(dirSize => dirSize <= 100000).Sum();
Console.WriteLine($"Answer to Part 1: {sumDirSizeUnder100k}");

//Part 2
int spaceAvailableToSystem = 70_000_000;
int spaceRequiredForUpdate = 30_000_000;
int spaceUsed = GetDirectorySizeRecursively(root);
int spaceAvailable = spaceAvailableToSystem - spaceUsed;

var (dirToDel, dirSize) = allDirs
    .Select(d => (Directory: d, Size: GetDirectorySizeRecursively(d)))
    .OrderByDescending(x => spaceAvailable + x.Size >= spaceRequiredForUpdate)
    .ThenBy(x => x.Size)
    .FirstOrDefault();

Console.WriteLine($"Answer to Part 2: The directory you should delete for the update is {dirToDel.Name} with a size of {dirSize}");
Console.WriteLine($"The Remaining space after the Update will be {spaceAvailable - dirSize + spaceRequiredForUpdate}");


List<string> ReadCommands()
{
    List<string> commands = new List<string>();

    using (var reader = new StreamReader(@"C:\Users\stefan.stingl\source\repos\AdventOfCode7\AdventOfCode7\input.txt"))
    {
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            commands.Add(line);
        }
    }
    return commands;
}

int GetDirectorySizeRecursively(Directory directory)
{
    if (directory.SubDirectories.Any())
    {
        return directory.Size + directory.SubDirectories.Sum(GetDirectorySizeRecursively);
    }

    return directory.Size;
   
}

void ConstructDirectoryTree()
{
    foreach (var command in commands)
    {
        var splitcommand = command.Split(' ');
        //User Input
        if (splitcommand[0] == "$")
        {
            if (splitcommand[1] == "cd")
            {
                if (splitcommand[2] == "/")
                {
                    currentdir = root;
                }
                else if (splitcommand[2] == "..")
                {
                    currentdir = currentdir.ParentDirectory;
                }
                else
                {
                    AdventOfCode7.Directory? resultdir;
                    resultdir = currentdir.SubDirectories.Find(x => x.Name == splitcommand[2]);
                    if (resultdir != null)
                    {
                        currentdir = resultdir;
                    }
                }
            }
            else if (splitcommand[1] == "ls")
            {
                //Currently not necessary, just ignoring the ls, just looking at what comes after, since there are no other commands besides cd and ls
            }
        }
        else
        {
            if (command.Split(" ")[0] == "dir")
            {
                AdventOfCode7.Directory newDir = new(splitcommand[1]);
                newDir.ParentDirectory = currentdir;
                currentdir.SubDirectories.Add(newDir);
                allDirs.Add(newDir);
            }
            else
            {
                File newFile = new(splitcommand[1], int.Parse(splitcommand[0]));
                currentdir.Files.Add(newFile);
                currentdir.Size += newFile.Size;
            }
        }
    }

}

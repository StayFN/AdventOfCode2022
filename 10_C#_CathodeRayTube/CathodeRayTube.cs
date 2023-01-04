var input = File.ReadAllLines("C:\\Users\\stefa\\VS_repos\\AOC10\\AOC10\\input.txt");

// Determine Value of register at each iCycle.
int cycle = 1; // Current CPU Cycle
int registerValue = 1; // Current Register Value
int currentInstr = 0; // Current Instruction
List<int> registerValues = new List<int>(); //List of Register Values at each Cycle
while (currentInstr <= input.Length)
{
    registerValues.Add(registerValue);
        
    if (currentInstr < input.Length && input[currentInstr] != "noop")
    {   
        cycle++;
        registerValues.Add(registerValue);
        registerValue += int.Parse(input[currentInstr].Split(" ")[1]);
    }

    cycle++;
    currentInstr++;
}


// Part 1 Find the signal strength during the 20th, 60th, 100th, 140th, 180th, and 220th cycles. What is the sum of these six signal strengths?
// Signal Strength at Cycle C = registerValue at C * C 
var sumSignals = 0;
for (int i = 20; i <= 220; i+=40)
{
    sumSignals += registerValues[i - 1] * i;
}
Console.WriteLine($"Solution to Part 1: {sumSignals}");

//Part 2
Console.WriteLine($"Solution to Part 2:\n");
for (int iCycle = 0, rowIndex = 0; iCycle < registerValues.Count - 1; iCycle++, rowIndex++)
{
    //New CRT Row
    if (iCycle != 0 && iCycle % 40 == 0)
    {
        Console.Write("\n");
        rowIndex = 0;
    }
    
    //Determine if registerValue is within 1 position of current cycle if yes, print "#" if no print "." 
    var printChar = (registerValues[iCycle] >= rowIndex - 1 && registerValues[iCycle] <= rowIndex + 1) ? "#" : ".";
    Console.Write(printChar);
}
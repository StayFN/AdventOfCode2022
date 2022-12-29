string[] input = File.ReadAllLines(@"C:\Users\stefan.stingl\source\repos\AdventOfCode8\AdventOfCode8\input.txt");
int[,] forest = new int[input.Length, input[0].Length];
int maxHeight = 0;

//Initialize Forest Data Structure
InitializeForest();

//Part 1
var visibleTrees = new HashSet<(int, int)>();
CalcVisibleFromTop();
CalcVisibleFromBottom();
CalcVisibleFromLeft();
CalcVisibleFromRight();

Console.WriteLine($"In Total {visibleTrees.Count} Trees are visible from the Outside");

//Part 2
List<Tree> trees = CalcTreeScenicScores();
var bestTree = trees.MaxBy(x => x.ScenicScore);

Console.WriteLine($"The Best Tree is the Tree at {bestTree.i}, {bestTree.j}.");
Console.WriteLine($"It is {bestTree.Height} Heigh and has a Scenic Score of {bestTree.ScenicScore }");




List<Tree> CalcTreeScenicScores()
{
    int bestscore = 0;
    var trees = new List<Tree>();

    for (int row = 0; row < forest.GetLength(0); row++)
    {
        for (int col = 0; col < forest.GetLength(1); col++)
        {     
            int currHeight = forest[row, col];
            Tree tree = new Tree() { i = row, j = col, Height = currHeight, VisibleDown = 0, VisibleUp = 0,
                VisibleLeft = 0, VisibleRight = 0, ScenicScore = 0};


            //Determine Visible Left
            for (int leftCol = col - 1; leftCol >= 0; leftCol--)
            {
                tree.VisibleLeft++;
                if (forest[row, leftCol] >= currHeight)
                    break;
            }

            //Determine Visible Right
            for (int rightcol = col + 1; rightcol < forest.GetLength(1); rightcol++)
            {
                tree.VisibleRight++;
                if (forest[row, rightcol] >= currHeight)
                    break;
            }

            //Determine Visible Top
            for (int upRow = row - 1; upRow >= 0; upRow--)
            {
                tree.VisibleUp++;
                if (forest[upRow, col] >= currHeight)
                {
                    break;
                }
            }

            //Determine Visible Bottom
            for (int downRow = row + 1; downRow < forest.GetLength(0); downRow++)
            {
                tree.VisibleDown++;
                if (forest[downRow, col] >= currHeight)
                    break;
            }

            tree.ScenicScore = tree.VisibleLeft * tree.VisibleRight * tree.VisibleUp * tree.VisibleDown;
            trees.Add(tree);

        }
    }

    return trees;

}

void CalcVisibleFromTop()
{
    
    for (int j = 0; j < forest.GetLength(1); j++)
    {
        int lastHighest = -1;
        for (int i = 0; i < forest.GetLength(0); i++)
        {
            //No Tree behind a tree with maxHeight can be visible from the outside 
            if (lastHighest == maxHeight)
                break;

            int currHeight = forest[i, j];

            //Current Tree not visible, if height <= height of the last highest tree
            if (currHeight <= lastHighest)
                continue;

            //Tree is visible from the outside. 
            visibleTrees.Add((i, j));

            //Tree is new highest tree. 
            lastHighest = currHeight;
          
        }
    }
}

void CalcVisibleFromBottom()
{

    for (int j = 0; j < forest.GetLength(1); j++)
    {
        int lastHighest = -1;
        for (int i = forest.GetLength(0) - 1; i >= 0; i--)
        {
            if (lastHighest == maxHeight)
                break;

            int currHeight = forest[i, j];

            //Current Tree not visible, if height <= height of the last highest tree
            if (currHeight <= lastHighest)
                continue;

            //Tree is visible from the outside. 
            visibleTrees.Add((i, j));

            //Tree is new highest tree. 
            lastHighest = currHeight;
        }
    }
}

void CalcVisibleFromLeft()
{
    for (int i = 0; i < forest.GetLength(0); i++)
    {
        int lastHighest = -1;
        for (int j = 0; j < forest.GetLength(1); j++)
        {
            if (lastHighest == maxHeight)
                break;

            int currHeight = forest[i, j];

            //Current Tree not visible, if height <= height of the last highest tree
            if (currHeight <= lastHighest)
                continue;

            //Tree is visible from the outside. 
            visibleTrees.Add((i, j));

            //Tree is new highest tree. 
            lastHighest = currHeight;
        }
    }
}

void CalcVisibleFromRight()
{
    
    for (int i = 0; i < forest.GetLength(0); i++)
    {
        int lastHighest = -1;
        for (int j = forest.GetLength(1) - 1; j >= 0; j--)
        {
            if (lastHighest == maxHeight)
                break;

            int currHeight = forest[i, j];

            //Current Tree not visible, if height <= height of the last highest tree
            if (currHeight <= lastHighest)
                continue;

            //Tree is visible from the outside. 
            visibleTrees.Add((i, j));

            //Tree is new highest tree. 
            lastHighest = currHeight;
        }
    }
    
}

void InitializeForest()
{
    for (int i = 0; i < forest.GetLength(0); i++)
    {
        for (int j = 0; j < forest.GetLength(1); j++)
        {
            int currHeight = Convert.ToInt32(Char.GetNumericValue(input[i][j]));

            if (currHeight > maxHeight)
            {
                maxHeight = currHeight;
            }

            forest[i, j] = currHeight;
        }
    }
}

struct Tree
{
    public Tree(int i, int j, int height, int visibleTop, int visibleBottom, int visibleLeft, int visibleRight, int scenicScore)
    {
        this.i = i;
        this.j = j;
        Height = height;
        VisibleUp = visibleTop;
        VisibleDown = visibleBottom;
        VisibleLeft = visibleLeft;
        VisibleRight = visibleRight;
        ScenicScore = scenicScore;
    }

    public int i { get; set; }
    public int j { get; set; }
    public int Height { get; set; }

    public int VisibleUp { get; set; } = 0;
    public int VisibleDown { get; set; } = 0;
    public int VisibleLeft { get; set; } = 0;
    public int VisibleRight { get; set; } = 0;

    public int ScenicScore { get; set; }


}

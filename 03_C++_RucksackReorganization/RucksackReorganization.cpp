// Only works with latest C++20 Preview in Visual Studio 2022 (As of Dec 2022)
#include <fstream>
#include <vector>
#include <algorithm>
#include <map>
#include <iostream>
#include <format>

//Part 1
struct Backpack
{
    std::string content{};
    std::string firstCompartment{};
    std::string secondCompartment{};
    char sharedChar{};
};

//Part2
struct ElfGroup
{
    Backpack groupBackpacks[3]{};
    char Badge{};
};

std::vector<Backpack> ReadStringFile(std::string path);
std::vector<Backpack> GetCompartments(std::vector<Backpack> backpacks);
std::vector<Backpack> GetCommonChars(std::vector<Backpack> backpacks);
char GetCommonChar(std::string first, std::string second, std::string third = "");
int SumPriorities(std::vector<Backpack> backpacks, const int lowercaseConversion, const int uppercaseConversion);
int SumGroupBadges(std::vector<ElfGroup>, const int lowerConversion, const int upperConversion);
std::vector<ElfGroup> GetElfGroups(std::vector<Backpack> backpacks);


int main()
{
    const int lowercaseConversion = 96; // chars a-z should convert to 1-26 
    const int uppercaseConversion = 38; // chars A-Z should convert to 27-52 

    //Part 1
    std::vector<Backpack> backpacks = ReadStringFile("input.txt");
    backpacks = GetCompartments(backpacks);
    backpacks = GetCommonChars(backpacks);
    int prioritySum = SumPriorities(backpacks,lowercaseConversion, uppercaseConversion);
    std::cout << std::format("Sum of Priorities (Solution to Part 1): {}\n", prioritySum); //Currently (Dec. 2022) only available in Preview std:c++latest in Visual Studio 

    //Part 2 
    std::vector<ElfGroup> elfGroups = GetElfGroups(backpacks);

    int sumBadges = 0;
    for (auto& elfGroup : elfGroups)
    {
        elfGroup.Badge = GetCommonChar(elfGroup.groupBackpacks[0].content, elfGroup.groupBackpacks[1].content, elfGroup.groupBackpacks[2].content);
    }
    int badgesSum = SumGroupBadges(elfGroups, lowercaseConversion, uppercaseConversion);
    std::cout << std::format("Sum of Badges (Solution to Part 2): {}\n", badgesSum);
}   


std::vector<Backpack> ReadStringFile(std::string path)
{
    std::ifstream fs(path);
    std::vector<Backpack> backpackContents;

    if (fs.is_open())
    {
        std::string line;
        while (std::getline(fs, line))
        {
            //printf("%s\n", line.c_str());
            
            Backpack backpack;
            backpack.content = line;

            backpackContents.push_back(backpack);
        }
        fs.close();
    }

    return backpackContents;
}

std::vector<Backpack> GetCompartments(std::vector<Backpack> backpacks)
{  
    for (auto& backpack : backpacks)
    {
        backpack.firstCompartment = backpack.content.substr(0, backpack.content.length() / 2);
        backpack.secondCompartment = backpack.content.substr(backpack.content.length() / 2);
    }
    return backpacks;
}

std::vector<Backpack> GetCommonChars(std::vector<Backpack> backpacks)
{

    for (auto& bagpack : backpacks)
    {
        std::sort(bagpack.firstCompartment.begin(), bagpack.firstCompartment.end());
        std::sort(bagpack.secondCompartment.begin(), bagpack.secondCompartment.end());

        bagpack.sharedChar = GetCommonChar(bagpack.firstCompartment, bagpack.secondCompartment);
    }
    return backpacks;
}

char GetCommonChar(std::string first, std::string second, std::string third)
{
    std::map<char, int> charCountDic;

    for (char const& c1 : first)
    {
        if (!charCountDic.contains(c1))
        {
            charCountDic[c1] = 1;
        }
    }

    for (char const& c2 : second)
    {
        if (charCountDic.contains(c2))
        {
            if (third == "")
            {
                return c2;
            }
            else
            {
                charCountDic[c2] = 2;
            }
        }
    }

    for (char const& c3 : third)
    {
        if (charCountDic.contains(c3) && charCountDic[c3] == 2)
        {
            return c3;
        }
    }

}

int SumPriorities(std::vector<Backpack> backpacks, const int lowerConversion, const int upperConversion)
{
    int sum = 0;

    for (auto& backpack : backpacks)
    {
        if (backpack.sharedChar >= 'A' && backpack.sharedChar <= 'Z')
        {
            sum += (int)backpack.sharedChar - upperConversion;
        }
        else if (backpack.sharedChar >= 'a' && backpack.sharedChar <= 'z')
        {
            sum += (int)backpack.sharedChar - lowerConversion;
        }
    }

    return sum;
}

int SumGroupBadges(std::vector<ElfGroup> elfGroups, const int lowerConversion, const int upperConversion)
{
    int sum = 0;

    for (auto& elfGroup : elfGroups)
    {
        if (elfGroup.Badge >= 'A' && elfGroup.Badge <= 'Z')
        {
            sum += (int)elfGroup.Badge - upperConversion;
        }
        else if (elfGroup.Badge >= 'a' && elfGroup.Badge <= 'z')
        {
            sum += (int)elfGroup.Badge - lowerConversion;
        }
    }

    return sum;
}

std::vector<ElfGroup> GetElfGroups(std::vector<Backpack> backpacks)
{
    int i = 0;
    ElfGroup elfGroup{};
    std::vector<ElfGroup> elfGroups;
    for (auto& backpack : backpacks)
    {
        elfGroup.groupBackpacks[i++] = backpack;

        if (i == 3)
        {
            i = 0;
            elfGroups.push_back(elfGroup);
            elfGroup = {};
        }
    }

    return elfGroups;
}
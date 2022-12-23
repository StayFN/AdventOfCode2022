#include <fstream>
#include <sstream>
#include <string>
#include <vector>

int main()
{
    std::vector<std::vector<int>> vec;

    std::ifstream file_in("my_file.txt");
    if (!file_in) {/*error*/}

    std::string line;
    while (std::getline(file_in, line)) // Read next line to `line`, stop if no more lines.
    {
        // Construct so called 'string stream' from `line`, see while loop below for usage.
        std::istringstream ss(line);

        vec.push_back({}); // Add one more empty vector (of vectors) to `vec`.

        int x;
        while (ss >> x) // Read next int from `ss` to `x`, stop if no more ints.
            vec.back().push_back(x); // Add it to the last sub-vector of `vec`.
    }
}
#pragma warning(disable:4996) // To disable fopen Warning

#include <stdio.h>
#include <string.h>


#define BITS 100
#define MAXCHAR 100

struct elf_pairs
{
	int first_start;
	int first_end;
	int second_start;
	int second_end;
};

struct BITMAP
{
	unsigned value : 1;
};


int main()
{
	//Read in Data
	printf("Hello, World!");
	FILE* input_file = fopen("input.txt", "r");
	char rows[1000][100];
	
	char row[MAXCHAR];
	size_t i = 0;
	while (feof(input_file) != 1)
	{
		fgets(row, MAXCHAR, input_file);
		printf("Row %s", row);

		row[strlen(row) - 1] = '\0';
		strcpy(rows[i], row);
		i++;
	}
	fclose(input_file);


	//Construct Elf-Pairs 
	struct elf_pairs my_elfpairs[1000];	
	char range_first_elf[10];
	char range_second_elf[10];
	char current_row[100];
	char* token;
	const char* delimiter_elfs = ",";
	const char* delimiter_range = "-";

	for (size_t i = 0; i < sizeof(rows) / sizeof(rows[0]); i++)
	{
		strcpy(current_row, rows[i]);
		
		//Split 2 Elfs Ranges
		token = strtok(current_row, delimiter_elfs);
		strcpy(range_first_elf, token);
		token = strtok(NULL, delimiter_elfs);
		strcpy(range_second_elf, token);

		//Split the Ranges of the 2 Elfs into start and end each. 

		//First Elfs Range 
		token = strtok(range_first_elf, delimiter_range);
		my_elfpairs[i].first_start = atoi(token);
		token = strtok(NULL, delimiter_elfs);
		my_elfpairs[i].first_end = atoi(token);

		//Second Elfs Range
		token = strtok(range_second_elf, delimiter_range);
		my_elfpairs[i].second_start = atoi(token);
		token = strtok(NULL, delimiter_range);
		my_elfpairs[i].second_end = atoi(token);
	}
	

	// Part 1 Get how many assignment pairs does one overlap the other completely?
	int sum_fully_containing_pairs = 0;
	for (size_t i = 0; i < sizeof(my_elfpairs) / sizeof(my_elfpairs[0]); i++)
	{
		
		if ((my_elfpairs[i].first_start <= my_elfpairs[i].second_start && my_elfpairs[i].first_end >= my_elfpairs[i].second_end) || 
			(my_elfpairs[i].second_start <= my_elfpairs[i].first_start && my_elfpairs[i].second_end >= my_elfpairs[i].first_end))
		{
			sum_fully_containing_pairs += 1;
		}
	}

	//Part 2 Get how many assignment pairs overlap at all (overlap >= 1) BITMAP Approach
	struct BITMAP bits_range_first_elf[BITS];
	struct BITMAP bits_range_second_elf[BITS];
	int sum_overlapping_pairs = 0;

	for (size_t i = 0; i < sizeof(my_elfpairs) / sizeof(my_elfpairs[0]); i++)
	{
        //Reset Bitmaps
		for (size_t i = 0; i < BITS; i++)
		{
			bits_range_first_elf[i].value = 0;
			bits_range_second_elf[i].value = 0;
		}

        //Assign 1 to the bits of the elf ranges
		for (int j = my_elfpairs[i].first_start; j <= my_elfpairs[i].first_end; j++)
		{
			bits_range_first_elf[j].value = 1;
		}

		for (int j = my_elfpairs[i].second_start; j <= my_elfpairs[i].second_end; j++)
		{
			bits_range_second_elf[j].value = 1;
		}


        //Check if there is an overlap
		for (size_t i = 0; i < BITS; i++)
		{
			if (bits_range_first_elf[i].value == 1 && bits_range_second_elf[i].value == 1)
			{
				sum_overlapping_pairs += 1;
				break;
			}
		}
	}

	printf("\nPart 1: Count of Pairs where one completely overlaps the other: %i\n", sum_fully_containing_pairs);
	printf("\nPart 2: Count of Pairs where one overlaps the other by at least 1: %i\n", sum_overlapping_pairs);

	return 0;
	
}


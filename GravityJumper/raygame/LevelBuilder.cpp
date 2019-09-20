#include"LevelBuilder.h"
#include<fstream>

void LevelBuilder::Generate(std::string fileName) //Generate points from .txt file
{
	std::fstream file;
	file.open(fileName, std::ios::in);
	if (!file.is_open())
		return;
	std::string fileString;
	while (std::getline(file, fileString)) //Set fileString equal to the line grabbed by getLine
	{
		if (fileString.length() == 0) //Continue if the line is blank
			continue;
		std::string tempX; //New X position
		std::string tempY; //New Y position
		bool phase = false; //Switches from inputting X to Y
		for (int i = 0; i < fileString.length(); i++) //Parse through the line
		{
			std::string tempLetter = "x"; //Create a temporary string
			tempLetter[0] = fileString[i]; //Set the string equal to the char
			if (fileString[i] == ',') //When a comma is read
			{
				phase = true; //Switch from pushing to X to pushing to Y
				continue; //Go to the next letter
			}
			if(!phase)
			tempX.append(tempLetter); //Check for X
			else tempY.append(tempLetter); //Check for Y
		}
		points.push_back({ std::stof(tempX), std::stof(tempY) });
	}
}
void LevelBuilder::Draw(Vector2 cameraOffset, Vector2 screenOffset)
{
	for (int i = 0; i < points.size() - 1; i++)
		DrawLineEx({ points[i].x - cameraOffset.x + screenOffset.x, points[i].y - cameraOffset.y + screenOffset.y },
				   { points[(__int64)i + 1].x - cameraOffset.x + screenOffset.x, points[(__int64)i + 1].y - cameraOffset.y + screenOffset.y },
					 2, lineColor);
	DrawLineEx({ points[0].x - cameraOffset.x + screenOffset.x, points[0].y - cameraOffset.y + screenOffset.y },
			   { points[points.size() - 1].x - cameraOffset.x + screenOffset.x, points[points.size() - 1].y - cameraOffset.y + screenOffset.y },
				 2, lineColor);

	//Casting 'i' to __int64 :
	//Warning C26451: Arithmetic overflow :
	//Using operator '+' on a 4 byte value and then casting the result to a 8 byte value.
	//Cast the value to the wider type before calling operator '+' to avoid overflow.

}
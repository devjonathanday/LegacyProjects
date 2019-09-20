#pragma once
#include"raylib.h"
#include<vector>
#include<string>
#include<iostream>

class LevelBuilder
{
public:
	std::vector<Vector2> points;
	Color lineColor;

	void Generate(std::string fileName);
	void Draw();
};
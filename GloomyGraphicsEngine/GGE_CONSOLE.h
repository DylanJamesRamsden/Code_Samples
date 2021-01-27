#pragma once
#include <string>
#include <glad/glad.h>
#include <GLFW/glfw3.h>

#include "GGE_LEVEL.h"
#include "GGE_File_Reader.h"

#include <sys/stat.h>
#include <fstream>

class GGE_CONSOLE {
private:
	bool consoleStatus = false;

	string CommandText;

	//****FPS VARIABLES:********
	double previousTime = glfwGetTime();
	int frameCount = 0;
	bool requestedFPS = false;
	//**************************

	//****LEVEL VARIABLES:********
	bool requestedLevelChange = false;
	string nLevelPath;
	string nOptionsPath;
	//****************************

	//****MODEL VARIABLES:********
	bool requestedModelSpawn = false;
	string modelPath;
	glm::vec3 modelPosition;
	glm::vec3 modelScale;
	//****************************

	//****LEVEL INFORMATION VARIABLES:****
	bool requestedLevelInformation = false;
	//************************************

	std::vector<string> parse(string s) {
		std::vector<string> words;
		string word = "";
		s += " ";

		for (auto c : s) {
			if (c == ' ') {
				words.push_back(word);
				word = "";
			}
			else
			{
				word += c;
			}
		}

		return words;
	}

	void CommandCheck(std::vector<string> commandLine) {
		if (commandLine[0] == "fps") { //COMPLETE
			requestedFPS = true;
		}
		else if (commandLine[0] == "triangles") {
			requestedLevelInformation = true;
		}	
		else if (commandLine[0] == "load") {
			if (1 < commandLine.size()) {
				if (checkFileStatus(commandLine[1])) {
					if (2 < commandLine.size()) {
						if (checkFileStatus(commandLine[2])) {
							nLevelPath = commandLine[1];
							nOptionsPath = commandLine[2];
							std::cout << std::endl << "Level loaded: ..." << std::endl;
							requestedLevelChange = true;
						}
						else std::cout << std::endl << "->GGE_ERROR: LEVEL OPTIONS FILE DOES NOT EXIST..." << std::endl;
					}
					else {
						std::cout << std::endl << "->GGE_ERROR: LEVEL OPTIONS FILE PATH MISSING..." << std::endl;
					}
				}
				else
				{
					std::cout << std::endl << "->GGE_ERROR: LEVEL MAP INFORMATION FILE DOES NOT EXIST..." << std::endl;
				}
			}
			else {
				std::cout << std::endl << "->GGE_ERROR: LEVEL INFORMATION FILE PATH MISSING..." << std::endl;
			}
		}	//COMPLETE
		else if (commandLine[0] == "spawn") {
			if (1 < commandLine.size()) {
				if (checkFileStatus(commandLine[1])) {
					modelPath = commandLine[1];

					if (2 < commandLine.size() && 3 < commandLine.size() && 4 < commandLine.size()) {
						if (ASCIIcheck(commandLine[2]) && ASCIIcheck(commandLine[3]) && ASCIIcheck(commandLine[4])) {
							modelPosition = glm::vec3(std::stof(commandLine[2]), std::stof(commandLine[3]), std::stof(commandLine[4]));

							if (5 < commandLine.size() && 6 < commandLine.size() && 7 < commandLine.size()) {
								if (ASCIIcheck(commandLine[5]) && ASCIIcheck(commandLine[6]) && ASCIIcheck(commandLine[7])) {
									modelScale = glm::vec3(std::stof(commandLine[5]), std::stof(commandLine[6]), std::stof(commandLine[7]));
									requestedModelSpawn = true;
								}
								else std::cout << "->GGE_ERROR: INVALID FLOAT VALUES FOR MODEL SCALE, MODEL SPAWN TERMINATED..." << std::endl;
							}
							else std::cout << "->GGE_ERROR: MODEL SCALE MISSING VALUES, MODEL SPAWN TERMINATED..." << std::endl;
						}
						else std::cout << "->GGE_ERROR: INVALID FLOAT VALUES FOR MODEL POSITION, MODEL SPAWN TERMINATED..." << std::endl;
					}
					else std::cout << "->GGE_ERROR: MODEL POSITION MISSING VALUES, MODEL SPAWN TERMINATED..." << std::endl;
				}
				else std::cout << "->GGE_ERROR: MODEL FILE PATH DOES NOT EXIST, MODEL SPAWN TERMINATED..." << std::endl;
			}
			else std::cout << "->GGE_ERROR: NO MODEL FILE PATH FOUND, MODEL SPAWN TERMINATED..." << std::endl;
		}
		else if (commandLine[0] == "help") {
			displayHelp();
		}
		else {
			std::cout << std::endl << "->GGE_ERROR: INVALIED COMMAND*" << std::endl;
		}
	}

	void displayFPS() {
		std::cout << std::endl << "FPS: " << frameCount << std::endl;
	}

	void displayHelp() {
		std::cout << "HELP:" << std::endl;
		std::cout << "VIEW FPS: fps" << std::endl;

		std::cout << "VIEW TRIANGLES IN LEVEL: triangles" << std::endl;

		std::cout << "LOAD LEVEL: load levelmapnumber leveloptionsnumber" << std::endl;
		std::cout << "	E.G. load 1 1" << std::endl;

		std::cout << "SPAWN MODEL: spawn path positionX positionY positionZ scaleX scaleY scaleZ (E.G. spawn )" << std::endl;
		std::cout << "	E.G. spawn RESOURCES/MODELS/backpack/packback.obj 1 1 1 1 1 1" << std::endl;
	}

	bool ASCIIcheck(string word) {
		for (int i = 0; i < word.size(); i++) {
			int tempASCII = (int)word.at(i);

			if (tempASCII < 46 || tempASCII > 57) {
				return false;
			}
			else {
				if (tempASCII == 47) {
					return false;
				}
			}
		}

		return true;
	}
	
public:
	GGE_CONSOLE() {};

	void ActivateConsole() {
		std::cout << std::endl << "*CONSOLE ACTIVATED*" << std::endl;
		consoleStatus = true;
		CommandText = "";
	}

	void DeactivateConsole() {
		std::cout << std::endl <<  "*COMMAND ENTERED*" << std::endl;

		CommandCheck(parse(CommandText));

		consoleStatus = false;
	}

	bool CurrentConsoleStatus() {
		return consoleStatus;
	}

	void AddToCommand(unsigned int codepoint) {
		CommandText += (char)codepoint;
	}

	//CALCULATES THE FPS FOR US
	void CalculateFrameRate()
	{
		double currentTime = glfwGetTime();
		frameCount++;

		if (currentTime - previousTime >= 1.0)
		{
			if (requestedFPS == true) {
				displayFPS();
				requestedFPS = false;
			}

			frameCount = 0;
			previousTime = currentTime;
		}
	}

	//**********************************
	bool RequestedLevelChange() {
		if (requestedLevelChange == true) {
			requestedLevelChange = false;
			return true;
		}
		else return false;
	}

	string getNewLevelPath() {
		return nLevelPath;
	}

	string getNewOptionsPath() {
		return nOptionsPath;
	}
	//**********************************

	//**********************************
	bool RequestedModelSpawn() {
		if (requestedModelSpawn == true) {
			requestedModelSpawn = false;
			return true;
		}
		else return false;
	}

	string getModelPath() {
		return modelPath;
	}

	glm::vec3 getModelPosition() {
		return modelPosition;
	}

	glm::vec3 getModelScale() {
		return modelScale;
	}

	bool checkFileStatus(const std::string name) {
		ifstream ifile;
		ifile.open(name);
		if (ifile) {
			return true;
		}
		else {
			return false;
		}
	}
	//**********************************

	//**********************************
	bool RequestedLevelInformation() {
		if (requestedLevelInformation) {
			requestedLevelInformation = false;
			return true;
		}
		else return false;
	}
	//**********************************
};

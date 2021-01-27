#pragma once
#include <iostream>
#include <fstream>
#include <sstream> 
#include <vector>
#include<string>
using namespace std;

class GGE_File_Reader {
private:
	const char* vertex_Shader_Source;
	const char* fragment_Shader_Source;

	vector<string> Map_Source;

	string FloorTextureIndex;
	string WallTextureIndex;

	bool WolfensteinTexturesActive = false;
	bool TorchActive = false;
	bool DirectionalActive = false;
	bool SkyboxActive = false;

public:
	GGE_File_Reader(string msName, string loName) {
		map_Source_Extract(msName);
		level_Options_Extract(loName);

		std::cout << "COMPILED GGE_FILE_READER" << std::endl;
	}

	void map_Source_Extract(string msName) {
		fstream file;
		file.open(msName);

		string line;
		while (getline(file, line)) {
			Map_Source.push_back(line);
		}

		if (Map_Source.size() == 0) {
			Map_Source.push_back("");
		}

		file.close();
	}

	void resetMapSource() {
		Map_Source.clear();
	}

	void level_Options_Extract(string msName) {

		fstream file;

		if (GetFileLength(msName) == 18) {
			file.open(msName);
		}
		else
		{
			file.open("RESOURCES\\Level2_Options.txt");
			std::cout << "->GGE_ERROR: FILE LAYOUT INCORRECT, DEFAULT OPTIONS USED..." << std::endl;
		}

		string line;
		int z = 0;
		while (getline(file, line)) {
			if (z == 4) {
				if (line == "true" || line == "True") {
					WolfensteinTexturesActive = true;
				}
				else WolfensteinTexturesActive = false;
			}
			if (z == 6) { 	//FLOOR TEXTURE
				if (stoi(line) < 10) {
					FloorTextureIndex = line;
				}
				else {
					std::cout << "->GGE_ERROR: FLOOR TEXTURE OUT OF RANGE, SET TO DEFAULT..." << std::endl;
					FloorTextureIndex = "0";
				}
			}
			else if (z == 8) {	//WALL TEXTURE
				if (stoi(line) < 10) {
					WallTextureIndex = line;
				}
				else {
					std::cout << "->GGE_ERROR: WALL TEXTURE OUT OF RANGE, SET TO DEFAULT..." << std::endl;
					WallTextureIndex = "0";
				}
			}
			else if (z == 12) { //TORCH STATUS
				if (line == "true" || line == "True") {
					TorchActive = true;
				}
				else TorchActive = false;
			}
			else if (z == 14) { //DIRECTIONAL STATUS
				if (line == "true" || line == "True") {
					DirectionalActive = true;
				}
				else DirectionalActive = false;
			}
			else if (z == 16) { //DIRECTIONAL STATUS
				if (line == "true" || line == "True") {
					SkyboxActive = true;
				}
				else SkyboxActive = false;
			}
			z++;
		}

			file.close();
	}

	vector<string> get_Map_Source() {
		return Map_Source;
	}

	const char* get_Vertex_Shader_Source() {
		return vertex_Shader_Source;
	}

	const char* get_Fragment_Shader_Source() {
		return fragment_Shader_Source;
	}

	string getFloorTextureIndex() {
		return FloorTextureIndex;
	}

	string getWallTextureIndex() {
		return WallTextureIndex;
	}

	bool getWolfensteinTexturesStatus() {
		return WolfensteinTexturesActive;
	}

	bool getTorchStatus() {
		return TorchActive;
	}

	bool getDirectionalStatus() {
		return DirectionalActive;
	}

	bool getSkyboxStatus() {
		return SkyboxActive;
	}

	void extractNewLevelInformation(string msName, string loName) {
		resetMapSource();

		map_Source_Extract(msName);
		level_Options_Extract(loName);

		std::cout << "COMPILED GGE_FILE_READER" << std::endl;
	}

	int GetFileLength(string fileName) {
		fstream file;
		file.open(fileName);

		int lineCount = 0;
		string line;
		while (getline(file, line)) {
			lineCount++;
		}

		file.close();

		return lineCount;
	}
};
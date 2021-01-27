#pragma once
#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <iostream>

#include <vector>
#include <string>

#include "GGE_TEXTURE_LIBRARY.h"
#include "GGE_CAMERA.h"

#include "GGE_WALL.h"
#include "GGE_NORMAL_WALL.h"
#include "GGE_CORNER_WALL.h"
#include "GGE_FLAT_WALL.h"
#include "GGE_DOOR_WALL.h"
#include "GGE_POINT_LIGHT.h"
#include "GGE_MODEL.h"

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>
using namespace std;

class GGE_LEVEL {
private:
	vector<string> levelInformation;
	vector<GGE_WALL> levelWalls;
	vector<GGE_WALL> levelFloors;
	vector<GGE_POINT_LIGHT> levelPointLights;

	vector <GGE_MODEL> levelModels;
	vector <glm::vec3> modelPositions;
	vector <glm::vec3> modelSizes;

	int NumberOfExtraWalls = 0;

	void levelCreation(float X, float Y, float Z) {
		float tempX = X;
		float tempY = Y;
		float tempZ = Z;

		int lineNumber = 0;
		for (auto& line : levelInformation) {
			tempX = 0;
			for (int x = 0; x < line.size(); x++) {
				if (line.at(x) == 'B') {
					string surr[3][3];
					surr[0][0] = levelInformation[lineNumber - 1][x - 1];
					surr[0][1] = levelInformation[lineNumber - 1][x];
					surr[0][2] = levelInformation[lineNumber - 1][x + 1];

					surr[1][0] = levelInformation[lineNumber][x - 1];
					surr[1][1] = levelInformation[lineNumber][x];
					surr[1][2] = levelInformation[lineNumber][x + 1];

					surr[2][0] = levelInformation[lineNumber + 1][x - 1];
					surr[2][1] = levelInformation[lineNumber + 1][x];
					surr[2][2] = levelInformation[lineNumber + 1][x + 1];

					CreateBlock(tempX, tempY, tempZ, WallType::Brick, surr);
				}
				else if (line.at(x) == 'W') {
					string surr[3][3];
					surr[0][0] = levelInformation[lineNumber - 1][x - 1];
					surr[0][1] = levelInformation[lineNumber - 1][x];
					surr[0][2] = levelInformation[lineNumber - 1][x + 1];

					surr[1][0] = levelInformation[lineNumber][x - 1];
					surr[1][1] = levelInformation[lineNumber][x];
					surr[1][2] = levelInformation[lineNumber][x + 1];

					surr[2][0] = levelInformation[lineNumber + 1][x - 1];
					surr[2][1] = levelInformation[lineNumber + 1][x];
					surr[2][2] = levelInformation[lineNumber + 1][x + 1];

					CreateBlock(tempX, tempY, tempZ, WallType::Wood, surr);
				}
				else if (line.at(x) == 'I') {
					string surr[3][3];
					surr[0][0] = levelInformation[lineNumber - 1][x - 1];
					surr[0][1] = levelInformation[lineNumber - 1][x];
					surr[0][2] = levelInformation[lineNumber - 1][x + 1];

					surr[1][0] = levelInformation[lineNumber][x - 1];
					surr[1][1] = levelInformation[lineNumber][x];
					surr[1][2] = levelInformation[lineNumber][x + 1];

					surr[2][0] = levelInformation[lineNumber + 1][x - 1];
					surr[2][1] = levelInformation[lineNumber + 1][x];
					surr[2][2] = levelInformation[lineNumber + 1][x + 1];

					CreateBlock(tempX, tempY, tempZ, WallType::Ice, surr);
				}
				else if (line.at(x) == '_') {
					glm::vec3 startPos = glm::vec3(tempX, tempY - 0.5, tempZ);
					GGE_FLAT_WALL Cap(Direction::Cap, startPos);
					levelFloors.push_back(Cap);
				}
				else if (line.at(x) == 'E') {
					glm::vec3 startPos = glm::vec3(tempX - 0.5, tempY - 0.5, tempZ);
					GGE_NORMAL_WALL tempNW(Direction::East, startPos);
					levelWalls.push_back(tempNW);

					//CreateBlock(tempX, tempY, tempZ);

					//std::cout << " -WALL CREATED (Verts:" + to_string(tempNW.getVerts().size()) + ")" << std::endl;
				}
				else if (line.at(x) == 'N') {
					glm::vec3 startPos = glm::vec3(tempX, tempY - 0.5, tempZ - 0.5);
					GGE_NORMAL_WALL tempNW(Direction::North, startPos);
					levelWalls.push_back(tempNW);

					//std::cout << " -WALL CREATED (Verts:" + to_string(tempNW.getVerts().size()) + ")" << std::endl;
				}
				else if (line.at(x) == '<') {
					glm::vec3 startPos = glm::vec3(tempX, tempY - 0.5, tempZ - 0.5);
					GGE_DOOR_WALL tempNW(Direction::North, startPos);
					tempNW.setWallType(WallType::Door);
					levelWalls.push_back(tempNW);

					startPos = glm::vec3(tempX, tempY - 0.5, tempZ);
					GGE_FLAT_WALL Cap(Direction::Cap, startPos);
					levelFloors.push_back(Cap);

					NumberOfExtraWalls += 2;
					//std::cout << " -DOOR CREATED (Verts:" + to_string(tempNW.getVerts().size()) + ")" << std::endl;
				}
				else if (line.at(x) == '^') {
					glm::vec3 startPos = glm::vec3(tempX - 0.5, tempY - 0.5, tempZ);
					GGE_DOOR_WALL tempNW(Direction::East, startPos);
					tempNW.setWallType(WallType::Door);
					levelWalls.push_back(tempNW);

					startPos = glm::vec3(tempX, tempY - 0.5, tempZ);
					GGE_FLAT_WALL Cap(Direction::Cap, startPos);
					levelFloors.push_back(Cap);

					NumberOfExtraWalls += 2;
					//std::cout << " -DOOR CREATED (Verts:" + to_string(tempNW.getVerts().size()) + ")" << std::endl;
				}
				else if (line.at(x) == 'C') {
					glm::vec3 startPos = glm::vec3(tempX, tempY - 0.5, tempZ);
					if (lineNumber > 0 && x < line.size() - 1) 	//CHECK DOWN && RIGHT
					{
						if (levelInformation.at(lineNumber + 1)[x] != '*' && line[x + 1] != '*') {
							GGE_CORNER_WALL tempNW(Direction::East, Direction::South, startPos);
							levelWalls.push_back(tempNW);

							//std::cout << " -CORNER CREATED (Verts:" + to_string(tempNW.getVerts().size()) + ")" << std::endl;
						}
					}
					if (lineNumber < levelInformation.size() && x > 0) //CHECK UP && LEFT
					{
						if (levelInformation.at(lineNumber + 1)[x] != '*' && line[x - 1] != '*') {
							GGE_CORNER_WALL tempNW(Direction::West, Direction::South, startPos);
							levelWalls.push_back(tempNW);

							//std::cout << " -CORNER CREATED (Verts:" + to_string(tempNW.getVerts().size()) + ")" << std::endl;
						}
					}
					if(lineNumber > 0 && x < line.size() - 1) //CHECK DOWN && LEFT
					{
						if (levelInformation.at(lineNumber - 1)[x] != '*' && line[x + 1] != '*') {
							GGE_CORNER_WALL tempNW(Direction::East, Direction::North, startPos);
							levelWalls.push_back(tempNW);

							//std::cout << " -CORNER CREATED (Verts:" + to_string(tempNW.getVerts().size()) + ")" << std::endl;
						}
					}
					if (lineNumber < levelInformation.size() && x > 0) //CHECK DOWN && LEFT
					{
						if (levelInformation.at(lineNumber - 1)[x] != '*' && line[x - 1] != '*') {
							GGE_CORNER_WALL tempNW(Direction::West, Direction::North, startPos);
							levelWalls.push_back(tempNW);

							//std::cout << " -CORNER CREATED (Verts:" + to_string(tempNW.getVerts().size()) + ")" << std::endl;
						}
					}
					NumberOfExtraWalls += 1;
				}
				else if (line.at(x) == 'L') {
					glm::vec3 startPos = glm::vec3(tempX, -0.3f, tempZ);
					GGE_POINT_LIGHT tempLight(startPos.x, startPos.y, startPos.z);
					levelPointLights.push_back(tempLight);

					startPos = glm::vec3(tempX, tempY - 0.5, tempZ);
					GGE_FLAT_WALL Cap(Direction::Cap, startPos);
					levelFloors.push_back(Cap);
				}
				else if (line.at(x) == 'G') {
					int modelType = rand() % 2;

					if (modelType == 0) {
						GGE_MODEL gun("RESOURCES/MODELS/mp44/MP44.fbx");
						levelModels.push_back(gun);
						modelPositions.push_back(glm::vec3(tempX, 0.0f, tempZ));
						modelSizes.push_back(glm::vec3(0.01f, 0.01f, 0.01f));

						glm::vec3 startPos = glm::vec3(tempX, tempY - 0.5, tempZ);
						GGE_FLAT_WALL Cap(Direction::Cap, startPos);
						levelFloors.push_back(Cap);
					}
					else
					{
						GGE_MODEL grenade("RESOURCES/MODELS/grenade/grenade.obj");
						levelModels.push_back(grenade);
						modelPositions.push_back(glm::vec3(tempX, 0.0f, tempZ));
						modelSizes.push_back(glm::vec3(0.01f, 0.01f, 0.01f));

						glm::vec3 startPos = glm::vec3(tempX, tempY - 0.5, tempZ);
						GGE_FLAT_WALL Cap(Direction::Cap, startPos);
						levelFloors.push_back(Cap);
					}
				}
				tempX += 1;
			}
			tempZ += 1;
			lineNumber++;
		}
		/*std::cout << NumberOfExtraWalls << std::endl;*/
	}

	void AssignExternals(float sX, float sY, float sZ, float size) {
		/*float floor[48] = {
			sX + (size / 2), sY, sZ - (size / 2), 0.0f, 1.0f, 0.0f, 20.0f, 20.0f,
			sX + (size / 2), sY, sZ + (size / 2), 0.0f, 1.0f, 0.0f, 20.0f, 0.0f,
			sX - (size / 2), sY, sZ - (size / 2), 0.0f, 1.0f, 0.0f, 0.0f, 20.0f,

			sX + (size / 2), sY, sZ + (size / 2), 0.0f, 1.0f, 0.0f, 20.0f, 0.0f,
			sX - (size / 2), sY, sZ + (size / 2), 0.0f, 1.0f, 0.0f, 0.0f, 0.0f,
			sX - (size / 2), sY, sZ - (size / 2), 0.0f, 1.0f, 0.0f, 0.0f, 20.0f
		};

		for (int i = 0; i < 48; i++) {
			vertices[i] = floor[i];
		}*/
	}

	void ClearLevel() {
		levelInformation.clear();
		levelWalls.clear();
		levelPointLights.clear();
		levelFloors.clear();

		for (auto m : levelModels) {
			for (auto mesh : m.meshes) {
				mesh.DeleteMeshInfo();
			}
		}
		levelModels.clear();
		modelPositions.clear();
		modelSizes.clear();
	}

	int sizeofWallsVector() {
		return levelWalls.size();
	}

	int sizeofFloorsVector() {
		return levelFloors.size();
	}
	
	void CreateBlock(float startX, float startY, float startZ, WallType t, string surr[3][3]) {
		if (surr[1][0] != "*" && surr[1][0] != "B" && surr[1][0] != "W" && surr[1][0] != "I") {
			glm::vec3 startPos = glm::vec3(startX - 0.5, startY - 0.5, startZ - 0.5);
			GGE_NORMAL_WALL FN(Direction::North, startPos);
			FN.setWallType(t);
			levelWalls.push_back(FN);
		}

		if (surr[1][2] != "*" && surr[1][2] != "B" && surr[1][2] != "W" && surr[1][2] != "I") {
			glm::vec3 startPos = glm::vec3(startX + 0.5, startY - 0.5, startZ - 0.5);
			GGE_NORMAL_WALL SN(Direction::North, startPos);
			SN.setWallType(t);
			levelWalls.push_back(SN);
		}

		if (surr[0][1] != "*" && surr[0][1] != "B" && surr[0][1] != "W" && surr[0][1] != "I") {
			glm::vec3 startPos = glm::vec3(startX - 0.5, startY - 0.5, startZ - 0.5);
			GGE_NORMAL_WALL FE(Direction::East, startPos);
			FE.setWallType(t);
			levelWalls.push_back(FE);
		}

		if (surr[2][1] != "*" && surr[2][1] != "B" && surr[2][1] != "W" && surr[2][1] != "I") {
			glm::vec3 startPos = glm::vec3(startX - 0.5, startY - 0.5, startZ + 0.5);
			GGE_NORMAL_WALL SE(Direction::East, startPos);
			SE.setWallType(t);
			levelWalls.push_back(SE);
		}

		glm::vec3 startPos = glm::vec3(startX, startY + 0.5, startZ);
		GGE_FLAT_WALL Cap(Direction::Cap, startPos);
		Cap.setWallType(t);
		levelWalls.push_back(Cap);

		
	}

public:
	GGE_LEVEL(float sX, float sY, float sZ, vector<string> lInfo) {

		std::cout << "LEVEL DRAWN" << std::endl;

		levelInformation = lInfo;
		levelCreation(sX, sY, sZ);
		//AssignExternals(lInfo.at(0).size()/2, sY - 0.5f, lInfo.size()/2, 20.0f);
		//AssignVerts();
	}

	vector<GGE_POINT_LIGHT> getLevelPointLights() {
		return levelPointLights;
	}

	void AddModel(string modelPath, glm::vec3 pos, glm::vec3 scale) {
		GGE_MODEL model(modelPath);
		levelModels.push_back(model);
		modelPositions.push_back(pos);
		modelSizes.push_back(scale);
	}

	void DrawWall(GGE_TEXTURE_LIBRARY textureLibrary, bool wtActive) {
		//for (GGE_WALL& wall : levelWalls) {
		//	if (wtActive == true) {
		//		/*switch (wall.getWallType())
		//		{
		//		case Door:
		//			wall.Draw(textureLibrary.getMetalDoorTexture());
		//			break;
		//		case Wood:
		//			wall.Draw(textureLibrary.getWoodWallTexture());
		//			break;
		//		case Ice:
		//			wall.Draw(textureLibrary.getIceWallTexture());
		//			break;
		//		case Brick:
		//			wall.Draw(textureLibrary.getBrickWallTexture());
		//			break;
		//		default:
		//			break;
		//		}*/
		//		DrawBrickWalls(textureLibrary.getBrickWallTexture());
		//		DrawWoodWalls(textureLibrary.getWoodWallTexture());
		//		DrawIceWalls(textureLibrary.getIceWallTexture());
		//	}
		//	else
		//	{
		//		wall.Draw(textureLibrary.getWallTexture());
		//	}
		//}
		if (wtActive == true) {
			DrawBrickWalls(textureLibrary.getBrickWallTexture());
			DrawWoodWalls(textureLibrary.getWoodWallTexture());
			DrawIceWalls(textureLibrary.getIceWallTexture());
			DrawWolfDoors(textureLibrary.getMetalDoorTexture());
		}
		else
		{
			DrawDefaultWalls(textureLibrary.getWallTexture());
		}
	}

	void DrawWoodWalls(int Texture) {
		float verts[150000];
		int floatCounter = 0;

		for (auto& wall : levelWalls) {
			if (wall.getWallType() == WallType::Brick) {
				for (auto& f : wall.getVerts()) {
					verts[floatCounter] = f;
					floatCounter++;
				}
			}
		}

		unsigned int VBO, VAO;

		glGenVertexArrays(1, &VAO);
		glGenBuffers(1, &VBO);
		//glGenBuffers(1, &EBO);
		// bind the Vertex Array Object first, then bind and set vertex buffer(s), and then configure vertex attributes(s).
		glBindVertexArray(VAO);
		glBindBuffer(GL_ARRAY_BUFFER, VBO);
		glBufferData(GL_ARRAY_BUFFER, sizeof(verts), verts, GL_STATIC_DRAW);
		// position attribute
		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
		glEnableVertexAttribArray(0);
		// Normal attribute
		glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
		glEnableVertexAttribArray(1);
		// texture coord attribute
		glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
		glEnableVertexAttribArray(2);
		// note that this is allowed, the call to glVertexAttribPointer registered VBO as the vertex attribute's bound vertex buffer object so afterwards we can safely unbind
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		//DRAW WALLS
		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, Texture);
		for (int i = 1; i < floatCounter / 6; i++) {
			glDrawArrays(GL_TRIANGLES, (6 * i), 6);
		}

		glDeleteVertexArrays(1, &VAO);
		glDeleteBuffers(1, &VBO);
	}

	void DrawBrickWalls(int Texture) {
		float verts[150000];
		int floatCounter = 0;

		for (auto& wall : levelWalls) {
			if (wall.getWallType() == WallType::Wood) {
				for (auto& f : wall.getVerts()) {
					verts[floatCounter] = f;
					floatCounter++;
				}
			}
		}

		unsigned int VBO, VAO;

		glGenVertexArrays(1, &VAO);
		glGenBuffers(1, &VBO);
		//glGenBuffers(1, &EBO);
		// bind the Vertex Array Object first, then bind and set vertex buffer(s), and then configure vertex attributes(s).
		glBindVertexArray(VAO);
		glBindBuffer(GL_ARRAY_BUFFER, VBO);
		glBufferData(GL_ARRAY_BUFFER, sizeof(verts), verts, GL_STATIC_DRAW);
		// position attribute
		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
		glEnableVertexAttribArray(0);
		// Normal attribute
		glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
		glEnableVertexAttribArray(1);
		// texture coord attribute
		glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
		glEnableVertexAttribArray(2);
		// note that this is allowed, the call to glVertexAttribPointer registered VBO as the vertex attribute's bound vertex buffer object so afterwards we can safely unbind
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		//DRAW WALLS
		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, Texture);
		for (int i = 1; i < floatCounter / 6; i++) {
			glDrawArrays(GL_TRIANGLES, (6 * i), 6);
		}

		glDeleteVertexArrays(1, &VAO);
		glDeleteBuffers(1, &VBO);
	}

	void DrawIceWalls(int Texture) {
		float verts[150000];
		int floatCounter = 0;

		for (auto& wall : levelWalls) {
			if (wall.getWallType() == WallType::Ice) {
				for (auto& f : wall.getVerts()) {
					verts[floatCounter] = f;
					floatCounter++;
				}
			}
		}

		unsigned int VBO, VAO;

		glGenVertexArrays(1, &VAO);
		glGenBuffers(1, &VBO);
		//glGenBuffers(1, &EBO);
		// bind the Vertex Array Object first, then bind and set vertex buffer(s), and then configure vertex attributes(s).
		glBindVertexArray(VAO);
		glBindBuffer(GL_ARRAY_BUFFER, VBO);
		glBufferData(GL_ARRAY_BUFFER, sizeof(verts), verts, GL_STATIC_DRAW);
		// position attribute
		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
		glEnableVertexAttribArray(0);
		// Normal attribute
		glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
		glEnableVertexAttribArray(1);
		// texture coord attribute
		glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
		glEnableVertexAttribArray(2);
		// note that this is allowed, the call to glVertexAttribPointer registered VBO as the vertex attribute's bound vertex buffer object so afterwards we can safely unbind
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		//DRAW WALLS
		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, Texture);
		for (int i = 1; i < floatCounter / 6; i++) {
			glDrawArrays(GL_TRIANGLES, (6 * i), 6);
		}

		glDeleteVertexArrays(1, &VAO);
		glDeleteBuffers(1, &VBO);
	}

	void DrawWolfDoors(int Texture) {
		float verts[150000];
		int floatCounter = 0;

		for (auto& wall : levelWalls) {
			if (wall.getWallType() == WallType::Door) {
				for (auto& f : wall.getVerts()) {
					verts[floatCounter] = f;
					floatCounter++;
				}
			}
		}

		unsigned int VBO, VAO;

		glGenVertexArrays(1, &VAO);
		glGenBuffers(1, &VBO);
		//glGenBuffers(1, &EBO);
		// bind the Vertex Array Object first, then bind and set vertex buffer(s), and then configure vertex attributes(s).
		glBindVertexArray(VAO);
		glBindBuffer(GL_ARRAY_BUFFER, VBO);
		glBufferData(GL_ARRAY_BUFFER, sizeof(verts), verts, GL_STATIC_DRAW);
		// position attribute
		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
		glEnableVertexAttribArray(0);
		// Normal attribute
		glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
		glEnableVertexAttribArray(1);
		// texture coord attribute
		glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
		glEnableVertexAttribArray(2);
		// note that this is allowed, the call to glVertexAttribPointer registered VBO as the vertex attribute's bound vertex buffer object so afterwards we can safely unbind
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		//DRAW WALLS
		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, Texture);
		for (int i = 1; i < floatCounter / 6; i++) {
			glDrawArrays(GL_TRIANGLES, (6 * i), 6);
		}

		glDeleteVertexArrays(1, &VAO);
		glDeleteBuffers(1, &VBO);
	}

	void DrawDefaultWalls(int Texture) {
		float verts[150000];
		int floatCounter = 0;

		for (auto& wall : levelWalls) {
			for (auto& f : wall.getVerts()) {
				verts[floatCounter] = f;
				floatCounter++;
			}
		}

		unsigned int VBO, VAO;

		glGenVertexArrays(1, &VAO);
		glGenBuffers(1, &VBO);
		//glGenBuffers(1, &EBO);
		// bind the Vertex Array Object first, then bind and set vertex buffer(s), and then configure vertex attributes(s).
		glBindVertexArray(VAO);
		glBindBuffer(GL_ARRAY_BUFFER, VBO);
		glBufferData(GL_ARRAY_BUFFER, sizeof(verts), verts, GL_STATIC_DRAW);
		// position attribute
		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
		glEnableVertexAttribArray(0);
		// Normal attribute
		glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
		glEnableVertexAttribArray(1);
		// texture coord attribute
		glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
		glEnableVertexAttribArray(2);
		// note that this is allowed, the call to glVertexAttribPointer registered VBO as the vertex attribute's bound vertex buffer object so afterwards we can safely unbind
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		//DRAW WALLS
		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, Texture);
		for (int i = 1; i < floatCounter / 6; i++) {
			glDrawArrays(GL_TRIANGLES, (6 * i), 6);
		}

		glDeleteVertexArrays(1, &VAO);
		glDeleteBuffers(1, &VBO);
	}

	void DrawFloor(GGE_TEXTURE_LIBRARY textureLibrary, bool wtActive) {
			if (wtActive == true) {
				drawCombinedFloors(textureLibrary.getWolfFloorTexture());
			}
			else
			{
				drawCombinedFloors(textureLibrary.getFloorTexture());
			}
	}

	void drawCombinedFloors(int Texture) {
		float verts[150000];
		int floatCounter = 0;

		for (auto& wall : levelFloors) {
			for (auto& f : wall.getVerts()) {
				verts[floatCounter] = f;
				floatCounter++;
			}
		}

		unsigned int VBO, VAO;

		glGenVertexArrays(1, &VAO);
		glGenBuffers(1, &VBO);
		//glGenBuffers(1, &EBO);
		// bind the Vertex Array Object first, then bind and set vertex buffer(s), and then configure vertex attributes(s).
		glBindVertexArray(VAO);
		glBindBuffer(GL_ARRAY_BUFFER, VBO);
		glBufferData(GL_ARRAY_BUFFER, sizeof(verts), verts, GL_STATIC_DRAW);
		// position attribute
		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
		glEnableVertexAttribArray(0);
		// Normal attribute
		glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
		glEnableVertexAttribArray(1);
		// texture coord attribute
		glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
		glEnableVertexAttribArray(2);
		// note that this is allowed, the call to glVertexAttribPointer registered VBO as the vertex attribute's bound vertex buffer object so afterwards we can safely unbind
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		//DRAW WALLS
		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, Texture);
		for (int i = 1; i < floatCounter / 6; i++) {
			glDrawArrays(GL_TRIANGLES, (6 * i), 6);
		}

		glDeleteVertexArrays(1, &VAO);
		glDeleteBuffers(1, &VBO);
	}

	void DrawLights(GGE_SHADER_LIBRARY sLibrary, bool directionalStatus, bool torchStatus, GGE_CAMERA camera) {
		sLibrary.use();
		sLibrary.setVec3("viewPos", camera.Position);
		sLibrary.setFloat("material.shininess", 32.0f);
		sLibrary.setBool("DirectionalStatus", directionalStatus);
		sLibrary.setBool("SpotlightStatus", torchStatus);

		if (directionalStatus == true) {
			// directional light
			sLibrary.setVec3("dirLight.direction", 1.2f, 1.0f, 1.3f);
			sLibrary.setVec3("dirLight.ambient", 0.8f, 0.8f, 0.8f);
			sLibrary.setVec3("dirLight.diffuse", 0.8f, 0.8f, 0.8f);
			sLibrary.setVec3("dirLight.specular", 0.5f, 0.5f, 0.5f);
		} //DIRECTIONAL LIGHT

		sLibrary.setInt("AddedPointLights", getLevelPointLights().size());
		int LightNumber = 0;
		for (auto& pL : getLevelPointLights()) {
			sLibrary.setVec3("pointLights[" + to_string(LightNumber) + "].position", pL.position);
			sLibrary.setVec3("pointLights[" + to_string(LightNumber) + "].ambient", pL.ambient);
			sLibrary.setVec3("pointLights[" + to_string(LightNumber) + "].diffuse", pL.diffuse);
			sLibrary.setVec3("pointLights[" + to_string(LightNumber) + "].specular", pL.specular);
			sLibrary.setFloat("pointLights[" + to_string(LightNumber) + "].constant", pL.constant);
			sLibrary.setFloat("pointLights[" + to_string(LightNumber) + "].linear", pL.linear);
			sLibrary.setFloat("pointLights[" + to_string(LightNumber) + "].quadratic", pL.quadratic);

			LightNumber++;
		}

		if (torchStatus == true) {
			sLibrary.setVec3("spotLight.position", camera.Position);
			sLibrary.setVec3("spotLight.direction", camera.Front);
			sLibrary.setVec3("spotLight.ambient", 0.0f, 0.0f, 0.0f);
			sLibrary.setVec3("spotLight.diffuse", 1.0f, 1.0f, 1.0f);
			sLibrary.setVec3("spotLight.specular", 1.0f, 1.0f, 1.0f);
			sLibrary.setFloat("spotLight.constant", 1.0f);
			sLibrary.setFloat("spotLight.linear", 0.09);
			sLibrary.setFloat("spotLight.quadratic", 0.032);
			sLibrary.setFloat("spotLight.cutOff", glm::cos(glm::radians(12.5f)));
			sLibrary.setFloat("spotLight.outerCutOff", glm::cos(glm::radians(15.0f)));
		} //TORCH
	}

	void DrawModels(GGE_SHADER_LIBRARY sLibrary) {
		for (int i = 0; i < levelModels.size(); i++) {
			glm::mat4 model = glm::mat4(1.0f);
			glm::mat4 view = glm::mat4(1.0f);
			glm::mat4 projection = glm::mat4(1.0f);
			model = glm::translate(model, modelPositions[i]); // translate it down so it's at the center of the scene
			model = glm::scale(model, modelSizes[i]);	// it's a bit too big for our scene, so scale it down
			sLibrary.setMat4("model", model);
			levelModels[i].Draw(sLibrary);
		}
	}

	void setNewLevelInfo(float sX, float sY, float sZ, vector<string> lInfo) {
		std::cout << "LEVEL REDRAWN" << std::endl;

		ClearLevel();

		levelInformation = lInfo;
		levelCreation(sX, sY, sZ);
		AssignExternals(lInfo.at(0).size() / 2, sY - 0.5f, lInfo.size() / 2, 20.0f);
		//AssignVerts();
	}

	void DisplayLevelInformation() {
		int triangles = 0;
		//int verts = 0;

		for (auto m : levelModels) {
			triangles += m.getModelTriangles();
			//verts += m.getModelVertices();
		}

		for (auto w : levelWalls) {
			//verts += w.getWallVerts();
			triangles += w.getWallVerts() / 3;
		}

		//FLOOR
		//verts += 6;
		triangles += 2;

		std::cout << "LEVEL INFORMATION:" << std::endl;
		std::cout << "LEVEL TRIANGLES: " + to_string(triangles) << std::endl;
		//std::cout << "LEVEL VERTICES: " + to_string(verts) << std::endl;
	}

};
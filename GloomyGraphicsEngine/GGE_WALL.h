#pragma once
#include <glad/glad.h>
#include <GLFW/glfw3.h>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "GGE_ROOM.h"

enum Direction
{
	North,
	East,
	South,
	West,
	Cap
};

enum WallType
{
	Default,
	Wood,
	Floor,
	Ice,
	Brick,
	Door
};

const float WIDTH = 1;
const float HEIGHT = 1;

class GGE_WALL {
private:
	vector<float> Verts;
	float wallVertices[240];
	float Width;
	float Height;

	WallType wType;

	glm::vec3 StartPosition;

	glm::vec3 endPosition;

	void AssignVerts() {
		int z = 48;
		//for (GGE_WALL& wall : levelWalls) {
			for (int i = 0; i < getVerts().size(); i++) {
				wallVertices[z] = getVerts().at(i);
				z++;
			}
		//}
	}

public:
	GGE_WALL(glm::vec3 startPosition, float width = WIDTH, float height = HEIGHT) {
		Width = width;
		Height = height;

		StartPosition.x = startPosition.x;
		StartPosition.y = startPosition.y;
		StartPosition.z = startPosition.z;
	}

	//COME LOOK AT
	void setVertsValue(vector<float> tempVerts, Direction d) {
		for (int i = 0; i < tempVerts.size()/3; i++)
		{
			for (int x = 0; x < 8; x++)
			{
				if (x < 3) {
					Verts.push_back(tempVerts[(i * 3) + x]);
				}
				else if (x < 6)
				{
					if (d == North || d == South || d == Cap) {
						if (x == 3) {
							Verts.push_back(1.0f);
						}
						else if (x == 4) {
							Verts.push_back(0.0f);
						}
						else {
							Verts.push_back(0.0f);
						}
					}
					else if (d == East || d == West) {
						if (x == 3) {
							Verts.push_back(0.0f);
						}
						else if (x == 4) {
							Verts.push_back(0.0f);
						}
						else {
							Verts.push_back(1.0f);
						}
					}
				}
				else
				{
					if (i == 0)
					{
						if (x == 6) {
							//Verts.push_back((WallTNumber.x * (1.0f/6.0f)) + (1.0f / 6.0f));
							Verts.push_back(1.0f);
						}
						else {
							//Verts.push_back((WallTNumber.y * (1.0f / 19.0f)) + (1.0f / 19.0f));
							Verts.push_back(1.0f);
						}
					}
					else if (i == 1 || i == 3) {
						if (x == 6) {
							//Verts.push_back((WallTNumber.x * (1.0f / 6.0f)) + (1.0f / 6.0f));
							Verts.push_back(1.0f);
						}
						else {
							//Verts.push_back(WallTNumber.y * (1.0f / 19.0f));
							Verts.push_back(0.0f);
						}
					}
					else if (i == 2 || i == 5) {
						if (x == 6) {
							//Verts.push_back(WallTNumber.x * (1.0f / 6.0f));
							Verts.push_back(0.0f);
						}
						else {
							//Verts.push_back((WallTNumber.y * (1.0f / 19.0f)) + (1.0f / 19.0f));
							Verts.push_back(1.0f);
						}
					}
					else if (i == 4) {
						if (x == 6) {
							//Verts.push_back(WallTNumber.x * (1.0f / 6.0f));
							Verts.push_back(0.0f);
						}
						else {
							//Verts.push_back(WallTNumber.y * (1.0f / 19.0f));
							Verts.push_back(0.0f);
						}
					}
				} 
			}
		}

		AssignVerts();
	}

	void setCornerValue(vector<float> tempVerts, Direction d1, Direction d2) {
		vector<float> firstWall;
		vector<float> secondWall;
		for (int i = 0; i < tempVerts.size(); i++) {
			if (i < (tempVerts.size() / 2)) {
				firstWall.push_back(tempVerts.at(i));
			}
			else
			{
				secondWall.push_back(tempVerts.at(i));
			}
		}

		setVertsValue(firstWall, d1);
		setVertsValue(secondWall, d2);
	}

	void setDoorValue(vector<float> tempVerts, Direction d) {
		vector<float> firstWall;
		vector<float> secondWall;
		vector<float> thirdWall;
		for (int i = 0; i < tempVerts.size(); i++) {
			if (i < (tempVerts.size() / 3)) {
				firstWall.push_back(tempVerts.at(i));
			}
			else if (i >= (tempVerts.size() / 3) && i < ((tempVerts.size()/3) * 2))
			{
				secondWall.push_back(tempVerts.at(i));
			}
			else {
				thirdWall.push_back(tempVerts.at(i));
			}
		}

		setVertsValue(firstWall, d);
		setVertsValue(secondWall, d);
		setVertsValue(thirdWall, d);
	}

	vector<float> getVerts() {
		return Verts;
	}

	float getWidth() {
		return Width;
	}

	float getHeight() {
		return Height;
	}

	glm::vec3 getStartPosition()
	{
		return StartPosition;
	}

	glm::vec3 getEndPosition() {
		return endPosition;
	}

	void setEndPosition(float x, float y, float z) {
		endPosition.x = x;
		endPosition.y = y;
		endPosition.z = z;
	}

	int getWallVerts() {
		return Verts.size();
	}

	void Draw(int Texture) {
		unsigned int VBO, VAO;

		glGenVertexArrays(1, &VAO);
		glGenBuffers(1, &VBO);
		//glGenBuffers(1, &EBO);
		// bind the Vertex Array Object first, then bind and set vertex buffer(s), and then configure vertex attributes(s).
		glBindVertexArray(VAO);
		glBindBuffer(GL_ARRAY_BUFFER, VBO);
		glBufferData(GL_ARRAY_BUFFER, sizeof(wallVertices), wallVertices, GL_STATIC_DRAW);
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
		for (int i = 1; i < Verts.size() / 6; i++) {
			glDrawArrays(GL_TRIANGLES, (6 * i), 6);
		}

		glDeleteVertexArrays(1, &VAO);
		glDeleteBuffers(1, &VBO);
	}

	void setWallType(WallType t) {
		wType = t;
	}

	WallType getWallType() {
		return wType;
	}
};
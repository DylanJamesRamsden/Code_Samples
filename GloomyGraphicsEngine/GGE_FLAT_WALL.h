#pragma once
#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <iostream>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "GGE_WALL.h"

class GGE_FLAT_WALL : public GGE_WALL
{
public:
	GGE_FLAT_WALL(Direction d, glm::vec3 startPosition, float width = WIDTH, float height = HEIGHT) : GGE_WALL(startPosition, width, height) {
		setVertsValue(CreateVerts(d), d);
	}
private:
	vector<float> CreateVerts(Direction d) {
		vector<float> vertsIsolated;
		switch (d)
		{
		case Cap:
			//TOP RIGHT *
			vertsIsolated.push_back(getStartPosition().x + getWidth()/2);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z - getWidth() / 2);
			//BOTTOM RIGHT*
			vertsIsolated.push_back(getStartPosition().x + getWidth()/2);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z + getWidth() / 2);
			//TOP LEFT*
			vertsIsolated.push_back(getStartPosition().x - getWidth() / 2);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z - getWidth() / 2);

			//BOTTOM RIGHT*
			vertsIsolated.push_back(getStartPosition().x + getWidth()/2);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z + getWidth() / 2);
			//BOTTOM LEFT
			vertsIsolated.push_back(getStartPosition().x - getWidth()/2);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z + getWidth() / 2);
			//TOP LEFT*
			vertsIsolated.push_back(getStartPosition().x - getWidth() / 2);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z - getHeight() / 2);

		/*	sX + (size / 2), sY, sZ - (size / 2), 0.0f, 1.0f, 0.0f, 20.0f, 20.0f,
				sX + (size / 2), sY, sZ + (size / 2), 0.0f, 1.0f, 0.0f, 20.0f, 0.0f,
				sX - (size / 2), sY, sZ - (size / 2), 0.0f, 1.0f, 0.0f, 0.0f, 20.0f,

				sX + (size / 2), sY, sZ + (size / 2), 0.0f, 1.0f, 0.0f, 20.0f, 0.0f,
				sX - (size / 2), sY, sZ + (size / 2), 0.0f, 1.0f, 0.0f, 0.0f, 0.0f,
				sX - (size / 2), sY, sZ - (size / 2), 0.0f, 1.0f, 0.0f, 0.0f, 20.0f*/

			setEndPosition(getStartPosition().x, getStartPosition().y, getStartPosition().z + getWidth());
			break;
		default:

			break;
		}

		return vertsIsolated;
	}
};

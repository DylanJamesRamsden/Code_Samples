#pragma once
#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <iostream>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "GGE_WALL.h"

class GGE_NORMAL_WALL : public GGE_WALL
{
public:
	GGE_NORMAL_WALL(Direction d, glm::vec3 startPosition, float width = WIDTH, float height = HEIGHT) : GGE_WALL(startPosition, width, height) {
		setVertsValue(CreateVerts(d), d);
	}
private:
	vector<float> CreateVerts(Direction d) {
		vector<float> vertsIsolated;
		switch (d)
		{
			case North:
				//TOP RIGHT *
				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y + getHeight());
				vertsIsolated.push_back(getStartPosition().z + getWidth());
				//BOTTOM RIGHT*
				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y);
				vertsIsolated.push_back(getStartPosition().z + getWidth());
				//TOP LEFT*
				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y + getHeight());
				vertsIsolated.push_back(getStartPosition().z);

				//BOTTOM RIGHT*
				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y);
				vertsIsolated.push_back(getStartPosition().z + getWidth());
				//BOTTOM LEFT
				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y);
				vertsIsolated.push_back(getStartPosition().z);
				//TOP LEFT*
				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y + getHeight());
				vertsIsolated.push_back(getStartPosition().z);

				setEndPosition(getStartPosition().x, getStartPosition().y, getStartPosition().z + getWidth());
			break;
			case East:
				//TOP RIGHT
				vertsIsolated.push_back(getStartPosition().x + getWidth());
				vertsIsolated.push_back(getStartPosition().y + getHeight());
				vertsIsolated.push_back(getStartPosition().z);
				//BOTTOM RIGHT
				vertsIsolated.push_back(getStartPosition().x + getWidth());
				vertsIsolated.push_back(getStartPosition().y);
				vertsIsolated.push_back(getStartPosition().z);
				//TOP LEFT
				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y + getHeight());
				vertsIsolated.push_back(getStartPosition().z);

				//BOTTOM RIGHT
				vertsIsolated.push_back(getStartPosition().x + getWidth());
				vertsIsolated.push_back(getStartPosition().y);
				vertsIsolated.push_back(getStartPosition().z);
				//BOTTOM LEFT
				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y);
				vertsIsolated.push_back(getStartPosition().z);
				//TOP LEFT
				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y + getHeight());
				vertsIsolated.push_back(getStartPosition().z);

				setEndPosition(getStartPosition().x + getWidth(), getStartPosition().y, getStartPosition().z);
			break;
			case South:
				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y + getHeight());
				vertsIsolated.push_back(getStartPosition().z);

				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y);
				vertsIsolated.push_back(getStartPosition().z);

				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y);
				vertsIsolated.push_back(getStartPosition().z - getWidth());

				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y + getHeight());
				vertsIsolated.push_back(getStartPosition().z - getWidth());

				setEndPosition(getStartPosition().x, getStartPosition().y, getStartPosition().z - getWidth());
			break;
			case West:
				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y + getHeight());
				vertsIsolated.push_back(getStartPosition().z);

				vertsIsolated.push_back(getStartPosition().x);
				vertsIsolated.push_back(getStartPosition().y);
				vertsIsolated.push_back(getStartPosition().z);

				vertsIsolated.push_back(getStartPosition().x - getWidth());
				vertsIsolated.push_back(getStartPosition().y);
				vertsIsolated.push_back(getStartPosition().z);

				vertsIsolated.push_back(getStartPosition().x - getWidth());
				vertsIsolated.push_back(getStartPosition().y + getHeight());
				vertsIsolated.push_back(getStartPosition().z);

				setEndPosition(getStartPosition().x - getWidth(), getStartPosition().y, getStartPosition().z);
			break;
			default:

				break;
		}

		return vertsIsolated;
	}
};

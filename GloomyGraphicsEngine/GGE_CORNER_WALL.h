#pragma once
#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <iostream>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "GGE_WALL.h"

class GGE_CORNER_WALL : public GGE_WALL
{
public:
	GGE_CORNER_WALL(Direction d1, Direction d2, glm::vec3 startPosition, float width = WIDTH, float height = HEIGHT) : GGE_WALL(startPosition, width, height) {
		setCornerValue(CreateVerts(d1, d2), d1, d2);
	}
private:
	vector<float> CreateVerts(Direction d1, Direction d2) {
		vector<float> vertsIsolated;
		switch (d1)
		{
		case North:
			//TOP RIGHT *
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z - getWidth() / 2);
			//BOTTOM RIGHT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z - getWidth() / 2);
			//TOP LEFT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);

			//BOTTOM RIGHT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z - getWidth() / 2);
			//BOTTOM LEFT
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z);
			//TOP LEFT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);
			break;
		case East:
			//TOP RIGHT
			vertsIsolated.push_back(getStartPosition().x + getWidth() / 2);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);
			//BOTTOM RIGHT
			vertsIsolated.push_back(getStartPosition().x + getWidth() / 2);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z);
			//TOP LEFT
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);

			//BOTTOM RIGHT
			vertsIsolated.push_back(getStartPosition().x + getWidth() / 2);
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
			break;
		case South:
			//TOP RIGHT *
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z + getWidth() / 2);
			//BOTTOM RIGHT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z + getWidth() / 2);
			//TOP LEFT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);

			//BOTTOM RIGHT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z + getWidth() / 2);
			//BOTTOM LEFT
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z);
			//TOP LEFT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);
			break;
		case West:
			//TOP RIGHT
			vertsIsolated.push_back(getStartPosition().x - getWidth() / 2);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);
			//BOTTOM RIGHT
			vertsIsolated.push_back(getStartPosition().x - getWidth() / 2);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z);
			//TOP LEFT
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);

			//BOTTOM RIGHT
			vertsIsolated.push_back(getStartPosition().x - getWidth() / 2);
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
			break;
		default:

			break;
		}

		switch (d2)
		{
		case North:
			//TOP RIGHT *
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z - getWidth() / 2);
			//BOTTOM RIGHT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z - getWidth() / 2);
			//TOP LEFT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);

			//BOTTOM RIGHT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z - getWidth() / 2);
			//BOTTOM LEFT
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z);
			//TOP LEFT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);
			break;
		case East:
			//TOP RIGHT
			vertsIsolated.push_back(getStartPosition().x + getWidth() / 2);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);
			//BOTTOM RIGHT
			vertsIsolated.push_back(getStartPosition().x + getWidth() / 2);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z);
			//TOP LEFT
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);

			//BOTTOM RIGHT
			vertsIsolated.push_back(getStartPosition().x + getWidth() / 2);
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
			break;
		case South:
			//TOP RIGHT *
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z + getWidth() / 2);
			//BOTTOM RIGHT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z + getWidth() / 2);
			//TOP LEFT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);

			//BOTTOM RIGHT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z + getWidth() / 2);
			//BOTTOM LEFT
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z);
			//TOP LEFT*
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);
			break;
		case West:
			//TOP RIGHT
			vertsIsolated.push_back(getStartPosition().x - getWidth() / 2);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);
			//BOTTOM RIGHT
			vertsIsolated.push_back(getStartPosition().x - getWidth() / 2);
			vertsIsolated.push_back(getStartPosition().y);
			vertsIsolated.push_back(getStartPosition().z);
			//TOP LEFT
			vertsIsolated.push_back(getStartPosition().x);
			vertsIsolated.push_back(getStartPosition().y + getHeight());
			vertsIsolated.push_back(getStartPosition().z);

			//BOTTOM RIGHT
			vertsIsolated.push_back(getStartPosition().x - getWidth() / 2);
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
			break;
		default:

			break;
		}

		return vertsIsolated;
	}
};

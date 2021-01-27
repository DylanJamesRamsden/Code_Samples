#pragma once
#include <glad/glad.h>
#include <GLFW/glfw3.h>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

class GGE_POINT_LIGHT {
private:
public:
    glm::vec3 position;

    float constant;
    float linear;
    float quadratic;

    glm::vec3 ambient;
    glm::vec3 diffuse;
    glm::vec3 specular;

	GGE_POINT_LIGHT(float xP, float yP, float zP) {
		position = glm::vec3(xP, yP, zP);

        constant = 0.05f;
        linear = 0.09f;
        quadratic = 0.032f;

        ambient = glm::vec3(0.2f, 0.2f, 0.2f);
        diffuse = glm::vec3(0.05f, 0.05f, 0.05f);
        specular = glm::vec3(0.0f, 0.0f, 0.0f);
	}
};

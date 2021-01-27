#pragma once

#include <glad/glad.h>
#include <GLFW/glfw3.h>

#include "GGE_SHADER_LIBRARY.h"

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

//DEFINES MOVEMENTS FOR THE CAMERA
enum Camera_Movement
{
	FORWARD, //0
	BACKWARD, //1
	LEFT, //2
	RIGHT //3
};

//DEFUALT CAMERA VALUES (DECALRED AS CONSTANTS)
const float YAW = -90.0f;
const float PITCH = 0.0f;
const float SPEED = 2.5f;
const float SENSITIVITY = 0.1f;
const float ZOOM = 45.0f;

//CAMERA CLASS THAT CALCULATES:
//-PROCESSES INPUT
//-CORRESPONDING EULER ANGLES, VECTORS AND MATRICES
class GGE_CAMERA {
private:
	//CALCULATES THE FRONT VECTOR FOR THE CAMERA'S (UPDATED) EULER ANGLES
	void updateCameraVectors() {
		glm::vec3 front;
		front.x = cos(glm::radians(Yaw)) * cos(glm::radians(Pitch));
		front.y = sin(glm::radians(Pitch));
		front.z = sin(glm::radians(Yaw)) * cos(glm::radians(Pitch));
		Front = glm::normalize(front);
		//NORMALIZE VECTORS BECAUSE LENGTH GETS CLOSER TO 0 WHEN YOU LOOK UP OR DOWN, MEANS SLOWER MOVEMENT
		Right = glm::normalize(glm::cross(Front, WorldUp));
		Up = glm::normalize(glm::cross(Right, Front));
	}
public:
	//CAMERA ATTRIBUTES
	glm::vec3 Position;
	glm::vec3 Front;
	glm::vec3 Up;
	glm::vec3 Right;
	glm::vec3 WorldUp;
	//EULER ANGLES
	float Yaw; //MOVEMENT ON X-AXIS
	float Pitch; //MOVEMENT ON Y-AXIS
	//CAMERA OPTIONS AND SETTINGS
	float MovementSpeed;
	float MouseSensitivity;
	float Zoom;

	//CONSTRUCTOR THAT TAKES VECTORS
	GGE_CAMERA(glm::vec3 position = glm::vec3(0.0f, 0.0f, 0.0f), glm::vec3 up = glm::vec3(0.0f, 1.0f, 0.0f), float yaw = YAW, float pitch = PITCH) 
		: Front(glm::vec3(0.0f, 0.0f, -1.0f)), MovementSpeed(SPEED), MouseSensitivity(SENSITIVITY), Zoom(ZOOM) {
		Position = position;
		WorldUp = up;
		Yaw = yaw;
		Pitch = pitch;
		updateCameraVectors();

		std::cout << "INITIALIZED GGE_CAMERA" << std::endl;
	}
	//CONSTRUCTOR THAT TAKES SCALARS
	GGE_CAMERA(float posX, float posY, float posZ, float upX, float upY, float upZ, float yaw, float pitch) 
		: Front(glm::vec3(0.0f, 0.0f, -1.0f)), MovementSpeed(SPEED), MouseSensitivity(SENSITIVITY), Zoom(ZOOM) {
		Position = glm::vec3(posX, posY, posZ);
		WorldUp = glm::vec3(upX, upY, upZ);
		Yaw = yaw;
		Pitch = pitch;
		updateCameraVectors();

		std::cout << "INITIALIZED GGE_CAMERA" << std::endl;
	}

	glm::mat4 GetViewMatrix()
	{
		return glm::lookAt(Position, Position + Front, Up);
	}

	//PROCESS WHAT SHOULD BE DONE WITH EACH KEY PRESS
	void ProcessKeyboard(Camera_Movement direction, float deltaTime) {
		//HOW FAST THE CAMERA SHOULD BE MOVING
		float velocity = MovementSpeed * deltaTime;

		//IF FAMILY ON THE DIRECTION(KEY) CHOSEN
		if (direction == FORWARD) {
			Position += Front * velocity;
		}
		if (direction == BACKWARD) {
			Position -= Front * velocity;
		}
		if (direction == RIGHT) {
			Position += Right * velocity;
		}
		if (direction == LEFT) {
			Position -= Right * velocity;
		}
	}

	//PROCESS WHAT SHOULD BE DONE WHEN THE MOUSE IS MOVED(LOOKING AROUND)
	void ProcessMouseMovement(float xoffset, float yoffset, GLboolean constraintPitch = true) {
		xoffset *= MouseSensitivity;
		yoffset *= MouseSensitivity;

		Yaw += xoffset; 
		Pitch += yoffset;

		//IF STAMENET ENSURES THAT IF PITCH IS OUT OF BOUNDS SCREEN DOESNT GET FLIPPED
		if (constraintPitch) {
			if (Pitch > 89.0f)
				Pitch = 89.0f;
			if (Pitch < -89.0f)
				Pitch = -89.0f;
		}

		updateCameraVectors();
	}

	//YOFFSET REFERS TO THE VERTICAL WHEEL_AXIS
	void ProcessMouseScroll(float yoffset) {
		Zoom -= (float)yoffset;

		if (Zoom < 1.0f)
			Zoom = 1.0f;
		if (Zoom > 45.0f)
			Zoom = 45.0f;
	}

	void CreatePerspective(GGE_SHADER_LIBRARY sLibrary) {
		glm::mat4 model = glm::mat4(1.0f);
		glm::mat4 view = glm::mat4(1.0f);
		glm::mat4 projection = glm::mat4(1.0f);
		model = glm::rotate(model, glm::radians(0.0f), glm::vec3(0.0f, 1.0f, 0.0f)); //SMALL ROTATION
		view = GetViewMatrix();
		projection = glm::perspective(glm::radians(Zoom), (float)800.0f / (float)600.0f, 0.1f, 50.0f); //CREATES PERSPECTIVE

		sLibrary.setMat4("model", model);
		sLibrary.setMat4("view", view);
		sLibrary.setMat4("projection", projection);
	}

	void CreateSkyboxPerspective(GGE_SHADER_LIBRARY sLibrary) {
		glm::mat4 model = glm::mat4(1.0f);
		glm::mat4 view = glm::mat4(1.0f);
		glm::mat4 projection = glm::mat4(1.0f);
		view = glm::mat4(glm::mat3(GetViewMatrix())); // remove translation from the view matrix
		sLibrary.setMat4("view", view);
		sLibrary.setMat4("projection", projection);
	}
};
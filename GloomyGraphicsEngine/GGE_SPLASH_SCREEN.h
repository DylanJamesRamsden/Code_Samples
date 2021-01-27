#pragma once
#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <iostream>

#include <vector>
#include <string>

#include "GGE_TEXTURE_LIBRARY.h"

class GGE_SPLASH_SCREEN {
private:
	int SplashDuration;
	int DurationTimer = 0;
public:
    float vertices[48] = {
	1.0f,  1.0f, 0.0f,  0.0f, 0.0f, 0.0f,   1.0f,  0.0f,// top right
	1.0f, -1.0f, 0.0f,  0.0f, 0.0f, 0.0f,   1.0f, 1.0f,// bottom right
   -1.0f,  1.0f, 0.0f,  0.0f, 0.0f, 0.0f,  0.0f,  0.0f,// top left 

	1.0f, -1.0f, 0.0f,  0.0f, 0.0f, 0.0f,   1.0f, 1.0f,// bottom right
   -1.0f, -1.0f, 0.0f,  0.0f, 0.0f, 0.0f,  0.0f, 1.0f,// bottom left
   -1.0f,  1.0f, 0.0f,  0.0f, 0.0f, 0.0f,  0.0f,  0.0f,// top left 
    };

	GGE_SPLASH_SCREEN(int Duration) {
		SplashDuration = Duration;
	}

	int getDuration() {
		return SplashDuration;
	}

	int getDurationTimer() {
		return DurationTimer;
	}

	void IncrementTimer() {
		DurationTimer++;
	}

	void Draw(GGE_TEXTURE_LIBRARY tLibrary) {
		GGE_SPLASH_SCREEN sScreen(240);
		unsigned int S_VBO, S_VAO, S_EBO;
		glGenVertexArrays(1, &S_VAO);
		glGenBuffers(1, &S_VBO);
		glGenBuffers(1, &S_EBO);
		glBindVertexArray(S_VAO);
		glBindBuffer(GL_ARRAY_BUFFER, S_VBO);
		glBufferData(GL_ARRAY_BUFFER, sizeof(sScreen.vertices), sScreen.vertices, GL_STATIC_DRAW);
		// position attribute
		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
		glEnableVertexAttribArray(0);
		// color attribute
		glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
		glEnableVertexAttribArray(1);
		// texture coord attribute
		glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
		glEnableVertexAttribArray(2);

		glBindTexture(GL_TEXTURE_2D, tLibrary.getSplashTexture());
		glBindVertexArray(S_VAO);
		glDrawArrays(GL_TRIANGLES, 0, 6);
	}
};
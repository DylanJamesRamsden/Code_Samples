#pragma once
#include <glad/glad.h>
#include <GLFW/glfw3.h>

#include "stb_image.h"
#include <iostream>
#include <filesystem>

#include<string>

class GGE_TEXTURE_LIBRARY {
private:
	unsigned int FloorTexture;
	unsigned int WallTexture;
	unsigned int SplashScreen;

	unsigned int WoodWallTexture;
	unsigned int BrickWallTexture;
	unsigned int IceWallTexture;
	unsigned int WolfFloorTexture;
	unsigned int MetalDoorTexture;

	vector<unsigned int> SkyboxTextures;

	//unsigned int WolfensteinSpecular;
public:
	GGE_TEXTURE_LIBRARY(string FloorIndex, string WallIndex) {
		AssignFloorTexture(FloorIndex);
		AssignWallTexture(WallIndex);
		AssignSplashTexture("RESOURCES\\GGE_LOGO.jpg");

		AssignBrickWallTexture();
		AssignIceWallTexture();
		AssignWoodWallTexture();
		AssignWolfFloorTexture();
		AssignMetalDoorTexture();

		AssignSkyboxTextures();
	}

	unsigned int loadTexture(char const* path)
	{
		unsigned int textureID;
		glGenTextures(1, &textureID);

		int width, height, nrComponents;
		unsigned char* data = stbi_load(path, &width, &height, &nrComponents, 0);
		if (data)
		{
			GLenum format;
			if (nrComponents == 1)
				format = GL_RED;
			else if (nrComponents == 3)
				format = GL_RGB;
			else if (nrComponents == 4)
				format = GL_RGBA;

			glBindTexture(GL_TEXTURE_2D, textureID);
			glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
			glGenerateMipmap(GL_TEXTURE_2D);

			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

			stbi_image_free(data);
		}
		else
		{
			std::cout << "Texture failed to load at path: " << path << std::endl;
			stbi_image_free(data);
		}

		return textureID;
	}

	void AssignWallTexture(string index) {
		unsigned int textureID;
		glGenTextures(1, &textureID);

		int width, height, nrComponents;
		string path = "RESOURCES\\\Textures\\" + index + ".JPG";
		unsigned char* data = stbi_load(path.c_str(), &width, &height, &nrComponents, 0);
		if (data)
		{
			GLenum format;
			if (nrComponents == 1)
				format = GL_RED;
			else if (nrComponents == 3)
				format = GL_RGB;
			else if (nrComponents == 4)
				format = GL_RGBA;

			glBindTexture(GL_TEXTURE_2D, textureID);
			glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
			glGenerateMipmap(GL_TEXTURE_2D);

			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

			stbi_image_free(data);
		}
		else
		{
			std::cout << "Texture failed to load at path: " << path << std::endl;
			stbi_image_free(data);
		}

		WallTexture = textureID;
	}

	void AssignBrickWallTexture() {
		unsigned int textureID;
		glGenTextures(1, &textureID);

		int width, height, nrComponents;
		string path = "RESOURCES\\\Textures\\WOLFENSTEIN\\BRICK.BMP";;
		unsigned char* data = stbi_load(path.c_str(), &width, &height, &nrComponents, 0);
		if (data)
		{
			GLenum format;
			if (nrComponents == 1)
				format = GL_RED;
			else if (nrComponents == 3)
				format = GL_RGB;
			else if (nrComponents == 4)
				format = GL_RGBA;

			glBindTexture(GL_TEXTURE_2D, textureID);
			glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
			glGenerateMipmap(GL_TEXTURE_2D);

			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

			stbi_image_free(data);
		}
		else
		{
			std::cout << "Texture failed to load at path: " << path << std::endl;
			stbi_image_free(data);
		}

		BrickWallTexture = textureID;
	}

	void AssignIceWallTexture() {
		unsigned int textureID;
		glGenTextures(1, &textureID);

		int width, height, nrComponents;
		string path = "RESOURCES\\\Textures\\WOLFENSTEIN\\ICE.BMP";;
		unsigned char* data = stbi_load(path.c_str(), &width, &height, &nrComponents, 0);
		if (data)
		{
			GLenum format;
			if (nrComponents == 1)
				format = GL_RED;
			else if (nrComponents == 3)
				format = GL_RGB;
			else if (nrComponents == 4)
				format = GL_RGBA;

			glBindTexture(GL_TEXTURE_2D, textureID);
			glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
			glGenerateMipmap(GL_TEXTURE_2D);

			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

			stbi_image_free(data);
		}
		else
		{
			std::cout << "Texture failed to load at path: " << path << std::endl;
			stbi_image_free(data);
		}

		IceWallTexture = textureID;
	}

	void AssignWoodWallTexture() {
		unsigned int textureID;
		glGenTextures(1, &textureID);

		int width, height, nrComponents;
		string path = "RESOURCES\\\TEXTURES\\WOLFENSTEIN\\WOOD.BMP";
		unsigned char* data = stbi_load(path.c_str(), &width, &height, &nrComponents, 0);
		if (data)
		{
			GLenum format;
			if (nrComponents == 1)
				format = GL_RED;
			else if (nrComponents == 3)
				format = GL_RGB;
			else if (nrComponents == 4)
				format = GL_RGBA;

			glBindTexture(GL_TEXTURE_2D, textureID);
			glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
			glGenerateMipmap(GL_TEXTURE_2D);

			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

			stbi_image_free(data);
		}
		else
		{
			std::cout << "Texture failed to load at path: " << path << std::endl;
			stbi_image_free(data);
		}

		WoodWallTexture = textureID;
	}

	void AssignWolfFloorTexture() {
		unsigned int textureID;
		glGenTextures(1, &textureID);

		int width, height, nrComponents;
		string path = "RESOURCES\\\TEXTURES\\WOLFENSTEIN\\FLOOR.JPG";
		unsigned char* data = stbi_load(path.c_str(), &width, &height, &nrComponents, 0);
		if (data)
		{
			GLenum format;
			if (nrComponents == 1)
				format = GL_RED;
			else if (nrComponents == 3)
				format = GL_RGB;
			else if (nrComponents == 4)
				format = GL_RGBA;

			glBindTexture(GL_TEXTURE_2D, textureID);
			glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
			glGenerateMipmap(GL_TEXTURE_2D);

			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

			stbi_image_free(data);
		}
		else
		{
			std::cout << "Texture failed to load at path: " << path << std::endl;
			stbi_image_free(data);
		}

		WolfFloorTexture = textureID;
	}

	void AssignMetalDoorTexture() {
		unsigned int textureID;
		glGenTextures(1, &textureID);

		int width, height, nrComponents;
		string path = "RESOURCES\\\Textures\\WOLFENSTEIN\\DOOR.BMP";
		unsigned char* data = stbi_load(path.c_str(), &width, &height, &nrComponents, 0);
		if (data)
		{
			GLenum format;
			if (nrComponents == 1)
				format = GL_RED;
			else if (nrComponents == 3)
				format = GL_RGB;
			else if (nrComponents == 4)
				format = GL_RGBA;

			glBindTexture(GL_TEXTURE_2D, textureID);
			glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
			glGenerateMipmap(GL_TEXTURE_2D);

			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

			stbi_image_free(data);
		}
		else
		{
			std::cout << "Texture failed to load at path: " << path << std::endl;
			stbi_image_free(data);
		}

		MetalDoorTexture = textureID;
	}

	void AssignFloorTexture(string index) {
		unsigned int textureID;
		glGenTextures(1, &textureID);

		int width, height, nrComponents;
		string path = "RESOURCES\\\Textures\\" + index + ".JPG";
		unsigned char* data = stbi_load(path.c_str(), &width, &height, &nrComponents, 0);
		if (data)
		{
			GLenum format;
			if (nrComponents == 1)
				format = GL_RED;
			else if (nrComponents == 3)
				format = GL_RGB;
			else if (nrComponents == 4)
				format = GL_RGBA;

			glBindTexture(GL_TEXTURE_2D, textureID);
			glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
			glGenerateMipmap(GL_TEXTURE_2D);

			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

			stbi_image_free(data);
		}
		else
		{
			std::cout << "Texture failed to load at path: " << path << std::endl;
			stbi_image_free(data);
		}

		FloorTexture = textureID;
	}

	void AssignSplashTexture(string Path) {
		unsigned int textureID;
		glGenTextures(1, &textureID);

		int width, height, nrComponents;
		string path = Path;
		unsigned char* data = stbi_load(path.c_str(), &width, &height, &nrComponents, 0);
		if (data)
		{
			GLenum format;
			if (nrComponents == 1)
				format = GL_RED;
			else if (nrComponents == 3)
				format = GL_RGB;
			else if (nrComponents == 4)
				format = GL_RGBA;

			glBindTexture(GL_TEXTURE_2D, textureID);
			glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
			glGenerateMipmap(GL_TEXTURE_2D);

			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
			glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

			stbi_image_free(data);
		}
		else
		{
			std::cout << "Texture failed to load at path: " << path << std::endl;
			stbi_image_free(data);
		}

		SplashScreen = textureID;
	}

	void AssignSkyboxTextures() {
		/*unsigned int right = loadTexture("RESOURCES\\\Textures\\SKYBOX\\single.JPG");
		SkyboxTextures.push_back(right);*/

		/*unsigned int left = loadTexture("RESOURCES\\\Textures\\SKYBOX\\left.PNG");
		SkyboxTextures.push_back(left);

		unsigned int front = loadTexture("RESOURCES\\\Textures\\SKYBOX\\front.PNG");
		SkyboxTextures.push_back(front);

		unsigned int back = loadTexture("RESOURCES\\\Textures\\SKYBOX\\back.PNG");
		SkyboxTextures.push_back(back);

		unsigned int top = loadTexture("RESOURCES\\\Textures\\SKYBOX\\top.PNG");
		SkyboxTextures.push_back(top);

		unsigned int bottom = loadTexture("RESOURCES\\\Textures\\SKYBOX\\bot.PNG");
		SkyboxTextures.push_back(bottom);*/
	}

	void ResetTextures()
	{
		glDeleteTextures(1, &FloorTexture);
		glDeleteTextures(1, &WallTexture);
	}

	int getFloorTexture() {
		return FloorTexture;
	}

	int getWallTexture() {
		return WallTexture;
	}

	int getBrickWallTexture() {
		return BrickWallTexture;
	}

	int getIceWallTexture() {
		return IceWallTexture;
	}

	int getWoodWallTexture() {
		return WoodWallTexture;
	}

	int getWolfFloorTexture() {
		return WolfFloorTexture;
	}

	int getMetalDoorTexture() {
		return MetalDoorTexture;
	}

	int getSplashTexture() {
		return SplashScreen;
	}

	vector<unsigned int> getSkyboxTextures() {
		return SkyboxTextures;
	}

	void reAssignTextures(string FloorIndex, string WallIndex) {
		AssignFloorTexture(FloorIndex);
		AssignWallTexture(WallIndex);
	}
};
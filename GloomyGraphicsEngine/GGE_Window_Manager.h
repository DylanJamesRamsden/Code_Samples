#pragma once
#include <glad/glad.h>
#include <GLFW/glfw3.h>

#include <iostream>

class GGE_Window_Manager {
private:
	GLFWwindow* Window;
public:
    GGE_Window_Manager(int SCR_WIDTH, int SCR_HEIGHT) {
        std::cout << "COMPILED GGE_WINDOW_MANAGER" << std::endl;

        initalizeGLFW();
        windowInitalize(SCR_WIDTH, SCR_HEIGHT);
    }

    void initalizeGLFW() {
        glfwInit();
        glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
        glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
        glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
    }

    void windowInitalize(int SCR_WIDTH, int SCR_HEIGHT) {
        if (!windowCreation(SCR_WIDTH, SCR_HEIGHT))
        {
            std::cout << "  -WINDOW_CREATION_FAILED" << std::endl;
        }
        else std::cout << "  -WINDOW_CREATION_SUCCEEDED" << std::endl;
    }

    //METHOD THAT CREATES THE WINDOW
    bool windowCreation(int SCR_WIDTH, int SCR_HEIGHT) {
        //THE SIZE OF THE WINDOW AND THE NAME OF THE WINDOW
        Window = glfwCreateWindow(SCR_WIDTH, SCR_HEIGHT, "Gloomy Graphics Engine", NULL, NULL);
        if (Window == NULL)
        {
            std::cout << "Failed to create GLFW window" << std::endl;
            glfwTerminate();
            return false;
        }
        //MAKES THE WINDOW THE MAIN CONTEXT ON THE THREAD
        glfwMakeContextCurrent(Window);
        glfwInit();
        //glfwSetFramebufferSizeCallback(Window, framebuffer_size_callback);

        return true;
    }

    void framebuffer_size_callback(GLFWwindow* Window, int width, int height)
    {
        glViewport(0, 0, width, height);
    }

    GLFWwindow* getWindow() {
        return Window;
    }
};
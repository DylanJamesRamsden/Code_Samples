#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <iostream>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "GGE_File_Reader.h"
#include "GGE_Window_Manager.h"
#include "GGE_SHADER_LIBRARY.h"
#include "GGE_LEVEL.h"
#include "GGE_CAMERA.h"
#include "GGE_TEXTURE_LIBRARY.h"
#include "GGE_SPLASH_SCREEN.h"
#include "GGE_MODEL.h"
#include "GGE_CONSOLE.h"
#include "GGE_SKYBOX.h"

#include <iostream>

#define STB_IMAGE_IMPLEMENTATION

//CAMERA
GGE_CAMERA camera(glm::vec3(0.0f, 0.0f, 3.0f));
float lastX = 800 / 2.0f;
float lastY = 600 / 2.0f;
bool firstMouse = true;

//TIME (BETWEEN LAST FRAME AND CURRENT FRAME)
float deltaTime = 0.0f;
float lastFrame = 0.0f;

// lighting
glm::vec3 lightPos(0.0f, 2.0f, 2.0f);

GGE_CONSOLE EngineConsole;

void mouse_callback(GLFWwindow* window, double xpos, double ypos);
void scroll_callback(GLFWwindow* window, double xoffset, double yoffset);
void processInput(GLFWwindow* window, GGE_TEXTURE_LIBRARY tLib);
void framebuffer_size_callback(GLFWwindow* window, int width, int height);
unsigned int loadTexture(char const* path);
void character_callback(GLFWwindow* window, unsigned int codepoint);
unsigned int loadCubemap(vector<std::string> faces);

int main() {
    GGE_File_Reader fReader(
        "RESOURCES\\Map2_Information_Source.txt",
        "RESOURCES\\Level2_Options.txt");

    GGE_Window_Manager wManager(800, 500);

    // tell GLFW to capture our mouse
    glfwSetFramebufferSizeCallback(wManager.getWindow(), framebuffer_size_callback);
    glfwSetCursorPosCallback(wManager.getWindow(), mouse_callback);
    glfwSetScrollCallback(wManager.getWindow(), scroll_callback);
    glfwSetInputMode(wManager.getWindow(), GLFW_CURSOR, GLFW_CURSOR_DISABLED);

    //INITIALIZES GLAD, GLAD MANAGES FUNCTION POINTERS FOR OPENGL
    //GLFWGETPROCADDRESS() DEFINES THE CORRECT FUNCTION BASED ON WHAT OS WE ARE COMPILING FOR
    if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress))
    {
        std::cout << "Failed to initialize GLAD" << std::endl;
        return -1;
    }

    GGE_SHADER_LIBRARY sLibrary("RESOURCES\\SHADERS\\VertexShader.txt", 
        "RESOURCES\\SHADERS\\FragmentShader.txt");

    GGE_SHADER_LIBRARY modelLibrary("RESOURCES\\SHADERS\\ModelVertexShader.txt",
        "RESOURCES\\SHADERS\\ModelFragmentShader.txt");

    GGE_SHADER_LIBRARY SplashScreenShader("RESOURCES\\SHADERS\\SplashVertexShader.txt",
        "RESOURCES\\SHADERS\\SplashFragmentShader.txt");

    GGE_SHADER_LIBRARY SkyboxShader("RESOURCES\\SHADERS\\SkyboxVertexShader.txt",
        "RESOURCES\\SHADERS\\SkyboxFragmentShader.txt");

    GGE_TEXTURE_LIBRARY tLibrary(fReader.getFloorTextureIndex(), fReader.getWallTextureIndex());

    GGE_LEVEL WolfensteinLevel(0, 0, 0, fReader.get_Map_Source());

    GGE_SPLASH_SCREEN sScreen(240);

    GGE_SKYBOX Skybox(10);

    //glEnable(GL_CULL_FACE);
    //glCullFace(GL_BACK);
    

    //KNOWN AS THE RENDER LOOP
    //KEEPS DRAWING IMAGES AND HANDLING USER INPUT UNTIL THE WINDOW IS TOLD TO CLOSE
    while (!glfwWindowShouldClose(wManager.getWindow()))
    {
        //TIME LOGIC
        float currentFrame = glfwGetTime();
        deltaTime = currentFrame - lastFrame;
        lastFrame = currentFrame;

        EngineConsole.CalculateFrameRate();

        if (sScreen.getDurationTimer() < sScreen.getDuration()) {
            SplashScreenShader.use();
            sScreen.Draw(tLibrary);
            sScreen.IncrementTimer();
        }
        else {
            processInput(wManager.getWindow(), tLibrary);

            //IF LEVEL CHANGE IS REQUESTED
            if (EngineConsole.RequestedLevelChange()) {
                glDeleteProgram(sLibrary.ID);
                sLibrary.ResetShaders("RESOURCES\\SHADERS\\VertexShader.txt",
                    "RESOURCES\\SHADERS\\FragmentShader.txt");

                fReader.extractNewLevelInformation(EngineConsole.getNewLevelPath(), EngineConsole.getNewOptionsPath());

                tLibrary.ResetTextures();
                tLibrary.reAssignTextures(fReader.getFloorTextureIndex(), fReader.getWallTextureIndex());

                WolfensteinLevel.setNewLevelInfo(0, 0, 0, fReader.get_Map_Source());
            }

            if (EngineConsole.RequestedModelSpawn()) {
                WolfensteinLevel.AddModel(EngineConsole.getModelPath(), EngineConsole.getModelPosition(), EngineConsole.getModelScale());
            }

            if (EngineConsole.RequestedLevelInformation()) {
                WolfensteinLevel.DisplayLevelInformation();
            }

            //RENDERING COMMANDS PLACED HERE SO THEY EXECUTE EVERY FRAME OF THE LOOP
            //CLEARS THE SCREEN OF A SPECIFIC COLOR< WE NEED TO DO THIS OR ELSE WE WILL SEE THE PREVIOUS FRAMES COLOR
            glClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
            glEnable(GL_DEPTH_TEST);

            if (fReader.getSkyboxStatus() == true) {
                Skybox.Draw(SkyboxShader, camera);
            }

            //LIGHTS
            WolfensteinLevel.DrawLights(sLibrary, fReader.getDirectionalStatus(), fReader.getTorchStatus(), camera);

            //CREATE PERSPECTIVE
            camera.CreatePerspective(sLibrary);

            WolfensteinLevel.DrawFloor(tLibrary, fReader.getWolfensteinTexturesStatus());

            WolfensteinLevel.DrawWall(tLibrary, fReader.getWolfensteinTexturesStatus());
            //WolfensteinLevel.DrawBrickWalls(tLibrary.getBrickWallTexture());

            WolfensteinLevel.DrawModels(sLibrary);
        }

        glfwSwapBuffers(wManager.getWindow());
        glfwPollEvents();
    }
    
    glDeleteProgram(sLibrary.ID);
    /*glDeleteVertexArrays(1, &VAO);
    glDeleteBuffers(1, &VBO);*/

    //CALLED AT THE END OF THE RENDER LOOP
    //CLEANS AND DELETES ALL OF GLFW RESOURCES THAT WERE ALLOCATED
    glfwTerminate();
    return 0;
}

//A GLFW INPUT METHOD
//TAKES THE WINDOW AS A PARAMETER
void processInput(GLFWwindow* window, GGE_TEXTURE_LIBRARY tLib)
{
    if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
        glfwSetWindowShouldClose(window, true);

    if (!EngineConsole.CurrentConsoleStatus()) {
        if (glfwGetKey(window, GLFW_KEY_GRAVE_ACCENT) == GLFW_PRESS) {
            EngineConsole.ActivateConsole();
            glfwSetCharCallback(window, character_callback);
        }

        if (glfwGetKey(window, GLFW_KEY_W) == GLFW_PRESS)
            camera.ProcessKeyboard(Camera_Movement(FORWARD), deltaTime);
        if (glfwGetKey(window, GLFW_KEY_S) == GLFW_PRESS)
            camera.ProcessKeyboard(Camera_Movement(BACKWARD), deltaTime);
        if (glfwGetKey(window, GLFW_KEY_A) == GLFW_PRESS)
            camera.ProcessKeyboard(Camera_Movement(LEFT), deltaTime);
        if (glfwGetKey(window, GLFW_KEY_D) == GLFW_PRESS)
            camera.ProcessKeyboard(Camera_Movement(RIGHT), deltaTime);
    }
    else
    {
        if (glfwGetKey(window, GLFW_KEY_ENTER) == GLFW_PRESS) {
            EngineConsole.DeactivateConsole();
            glfwSetCharCallback(window, NULL);
        }
    }
}

void mouse_callback(GLFWwindow* window, double xpos, double ypos)
{
    if (firstMouse)
    {
        lastX = xpos;
        lastY = ypos;
        firstMouse = false;
    }

    float xoffset = xpos - lastX;
    float yoffset = lastY - ypos; // reversed since y-coordinates go from bottom to top

    lastX = xpos;
    lastY = ypos;

    camera.ProcessMouseMovement(xoffset, yoffset);
}

void scroll_callback(GLFWwindow* window, double xoffset, double yoffset)
{
    camera.ProcessMouseScroll(yoffset);
}

//TAKES IN A GLFW WINDOW AND NEW DIMENSIONS FOR THE WINDOW
//THIS METHOD IS CALLED EVERYTIME THE WINDOW IS RESIZED
void framebuffer_size_callback(GLFWwindow* window, int width, int height)
{
    glViewport(0, 0, width, height);
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

void character_callback(GLFWwindow* window, unsigned int codepoint)
{
    std::cout << (char)codepoint;
    EngineConsole.AddToCommand(tolower(codepoint));
}

unsigned int loadCubemap(vector<std::string> faces)
{
    unsigned int textureID;
    glGenTextures(1, &textureID);
    glBindTexture(GL_TEXTURE_CUBE_MAP, textureID);

    int width, height, nrChannels;
    for (unsigned int i = 0; i < faces.size(); i++)
    {
        unsigned char* data = stbi_load(faces[i].c_str(), &width, &height, &nrChannels, 0);
        if (data)
        {
            glTexImage2D(GL_TEXTURE_CUBE_MAP_POSITIVE_X + i, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
            stbi_image_free(data);
        }
        else
        {
            std::cout << "Cubemap texture failed to load at path: " << faces[i] << std::endl;
            stbi_image_free(data);
        }
    }
    glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_R, GL_CLAMP_TO_EDGE);

    return textureID;
}
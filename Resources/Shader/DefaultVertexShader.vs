#version 330 core

layout (location = 0) in vec3 vertexPosition;
layout (location = 1) in vec3 vertexNormals;
layout (location = 2) in vec2 vertexTextureCoordinates;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec2 textureCoordinates;                  

void main() {
	gl_Position = projection * view * model * vec4(vertexPosition.xyz, 1.0f);
	textureCoordinates = vertexTextureCoordinates;
}

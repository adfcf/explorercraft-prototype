#version 330 core

out vec4 fragmentColor;
in vec2 textureCoordinates;

uniform sampler2DArray tex;

void main() {
	fragmentColor = texture(tex, vec3(textureCoordinates.st, 0));
}

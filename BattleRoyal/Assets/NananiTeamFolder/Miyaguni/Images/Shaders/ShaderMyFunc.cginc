// normlized value. input -> output
float Remap(float value, float2 input, float2 output){
	float inRange = input.y - input.x;
	float outRange = output.y - output.x;
	float value2rate = (value - input.x) / inRange;
	return (value2rate * outRange) + output.x;
}
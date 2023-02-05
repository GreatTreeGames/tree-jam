using UnityEngine;

namespace Scenes.simplest_possible_version.scripts
{
    public static class Extensions
    {
        public static Vector3 Multiply(this Vector3 input1, Vector3 input2)
        {
            return new Vector3(input1.x * input2.x, input1.y * input2.y, input1.z * input2.z);
        }
        
        public static Vector3 Divide(this Vector3 input1, Vector3 input2)
        {
            return new Vector3(input1.x / input2.x, input1.y / input2.y, input1.z / input2.z);
        }

        public static Vector3 ToVector3(this Vector2 input, float z = 0f)
        {
            return new Vector3(input.x, input.y, z);
        }
    }
}

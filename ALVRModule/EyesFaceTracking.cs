using System.Diagnostics;
using VRCFaceTracking;
using VRCFaceTracking.Core.Types;

namespace ALVRModule
{
    public class EyesFaceTracking : BaseFaceTracking
    {
        // Code taken from VRCFaceTracking-QuestProOpenXR
        private static void SetEyesQuatParams(float[] p)
        {
            Debug.Assert(p.Length == 8);

            var eye = UnifiedTracking.Data.Eye;

            var q_x = p[0];
            var q_y = p[1];
            var q_z = p[2];
            var q_w = p[3];

            double yaw = Math.Atan2(2.0 * (q_y * q_z + q_w * q_x), q_w * q_w - q_x * q_x - q_y * q_y + q_z * q_z);
            double pitch = Math.Asin(-2.0 * (q_x * q_z - q_w * q_y));

            double pitch_L = (180.0 / Math.PI) * pitch;
            double yaw_L = (180.0 / Math.PI) * yaw;

            q_x = p[4];
            q_y = p[5];
            q_z = p[6];
            q_w = p[7];
            yaw = Math.Atan2(2.0 * (q_y * q_z + q_w * q_x), q_w * q_w - q_x * q_x - q_y * q_y + q_z * q_z);
            pitch = Math.Asin(-2.0 * (q_x * q_z - q_w * q_y));

            double pitch_R = (180.0 / Math.PI) * pitch;
            double yaw_R = (180.0 / Math.PI) * yaw;

            var radianConst = 0.0174533f;

            var pitch_R_mod = (float)(Math.Abs(pitch_R) + 4f * Math.Pow(Math.Abs(pitch_R) / 30f, 30f));
            var pitch_L_mod = (float)(Math.Abs(pitch_L) + 4f * Math.Pow(Math.Abs(pitch_L) / 30f, 30f));
            var yaw_R_mod = (float)(Math.Abs(yaw_R) + 6f * Math.Pow(Math.Abs(yaw_R) / 27f, 18f));
            var yaw_L_mod = (float)(Math.Abs(yaw_L) + 6f * Math.Pow(Math.Abs(yaw_L) / 27f, 18f));

            eye.Right.Gaze = new Vector2(
                pitch_R < 0 ? pitch_R_mod * radianConst : -1 * pitch_R_mod * radianConst,
                yaw_R < 0 ? -1 * yaw_R_mod * radianConst : (float)yaw_R * radianConst);
            eye.Left.Gaze = new Vector2(
                pitch_L < 0 ? pitch_L_mod * radianConst : -1 * pitch_L_mod * radianConst,
                yaw_L < 0 ? -1 * yaw_L_mod * radianConst : (float)yaw_L * radianConst);

            eye.Left.PupilDiameter_MM = 5f;
            eye.Right.PupilDiameter_MM = 5f;

            eye._minDilation = 0;
            eye._maxDilation = 10;
        }

        private static void SetCombEyesQuatParams(float[] p)
        {
            Debug.Assert(p.Length == 4);

            var array = new float[8];
            p.CopyTo(array, 0);
            p.CopyTo(array, 4);

            SetEyesQuatParams(array);
        }

        public override bool ConsumePacket(byte[] packet, ref int cursor, string prefix)
        {
            switch (prefix)
            {
                case "EyesQuat":
                    SetEyesQuatParams(GetParams(packet, ref cursor, 8));
                    return true;
                case "CombQuat":
                    SetCombEyesQuatParams(GetParams(packet, ref cursor, 4));
                    return true;
                default:
                    return false;
            }
        }
    }
}

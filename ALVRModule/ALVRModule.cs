using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using VRCFaceTracking;
using VRCFaceTracking.Core.Params.Expressions;
using VRCFaceTracking.Core.Types;
using static VRCFaceTracking.Core.Params.Expressions.UnifiedExpressions;

namespace ALVRModule
{
    using static FaceFb;
    using static FacePico;

    public enum FaceFb
    {
        BrowLowererL = 0,
        BrowLowererR = 1,
        CheekPuffL = 2,
        CheekPuffR = 3,
        CheekRaiserL = 4,
        CheekRaiserR = 5,
        CheekSuckL = 6,
        CheekSuckR = 7,
        ChinRaiserB = 8,
        ChinRaiserT = 9,
        DimplerL = 10,
        DimplerR = 11,
        EyesClosedL = 12,
        EyesClosedR = 13,
        EyesLookDownL = 14,
        EyesLookDownR = 15,
        EyesLookLeftL = 16,
        EyesLookLeftR = 17,
        EyesLookRightL = 18,
        EyesLookRightR = 19,
        EyesLookUpL = 20,
        EyesLookUpR = 21,
        InnerBrowRaiserL = 22,
        InnerBrowRaiserR = 23,
        JawDrop = 24,
        JawSidewaysLeft = 25,
        JawSidewaysRight = 26,
        JawThrust = 27,
        LidTightenerL = 28,
        LidTightenerR = 29,
        LipCornerDepressorL = 30,
        LipCornerDepressorR = 31,
        LipCornerPullerL = 32,
        LipCornerPullerR = 33,
        LipFunnelerLB = 34,
        LipFunnelerLT = 35,
        LipFunnelerRB = 36,
        LipFunnelerRT = 37,
        LipPressorL = 38,
        LipPressorR = 39,
        LipPuckerL = 40,
        LipPuckerR = 41,
        LipStretcherL = 42,
        LipStretcherR = 43,
        LipSuckLB = 44,
        LipSuckLT = 45,
        LipSuckRB = 46,
        LipSuckRT = 47,
        LipTightenerL = 48,
        LipTightenerR = 49,
        LipsToward = 50,
        LowerLipDepressorL = 51,
        LowerLipDepressorR = 52,
        MouthLeft = 53,
        MouthRight = 54,
        NoseWrinklerL = 55,
        NoseWrinklerR = 56,
        OuterBrowRaiserL = 57,
        OuterBrowRaiserR = 58,
        UpperLidRaiserL = 59,
        UpperLidRaiserR = 60,
        UpperLipRaiserL = 61,
        UpperLipRaiserR = 62,
        TongueTipInterdental = 63,
        TongueTipAlveolar = 64,
        TongueFrontDorsalPalate = 65,
        TongueMidDorsalPalate = 66,
        TongueBackDorsalVelar = 67,
        TongueOutFb = 68,
        TongueRetreat = 69,
        Face1FbMax = 63,
        Face2FbMax = 70,
    }

    public enum FacePico
    {
        Pico_EyeLookDown_L = 0,
        Pico_NoseSneer_L = 1,
        Pico_EyeLookIn_L = 2,
        Pico_BrowInnerUp = 3,
        Pico_BrowDown_R = 4,
        Pico_MouthClose = 5,
        Pico_MouthLowerDown_R = 6,
        Pico_JawOpen = 7,
        Pico_MouthUpperUp_R = 8,
        Pico_MouthShrugUpper = 9,
        Pico_MouthFunnel = 10,
        Pico_EyeLookIn_R = 11,
        Pico_EyeLookDown_R = 12,
        Pico_NoseSneer_R = 13,
        Pico_MouthRollUpper = 14,
        Pico_JawRight = 15,
        Pico_BrowDown_L = 16,
        Pico_MouthShrugLower = 17,
        Pico_MouthRollLower = 18,
        Pico_MouthSmile_L = 19,
        Pico_MouthPress_L = 20,
        Pico_MouthSmile_R = 21,
        Pico_MouthPress_R = 22,
        Pico_MouthDimple_R = 23,
        Pico_MouthLeft = 24,
        Pico_JawForward = 25,
        Pico_EyeSquint_L = 26,
        Pico_MouthFrown_L = 27,
        Pico_EyeBlink_L = 28,
        Pico_CheekSquint_L = 29,
        Pico_BrowOuterUp_L = 30,
        Pico_EyeLookUp_L = 31,
        Pico_JawLeft = 32,
        Pico_MouthStretch_L = 33,
        Pico_MouthPucker = 34,
        Pico_EyeLookUp_R = 35,
        Pico_BrowOuterUp_R = 36,
        Pico_CheekSquint_R = 37,
        Pico_EyeBlink_R = 38,
        Pico_MouthUpperUp_L = 39,
        Pico_MouthFrown_R = 40,
        Pico_EyeSquint_R = 41,
        Pico_MouthStretch_R = 42,
        Pico_CheekPuff = 43,
        Pico_EyeLookOut_L = 44,
        Pico_EyeLookOut_R = 45,
        Pico_EyeWide_R = 46,
        Pico_EyeWide_L = 47,
        Pico_MouthRight = 48,
        Pico_MouthDimple_L = 49,
        Pico_MouthLowerDown_L = 50,
        Pico_TongueOut = 51,
        Pico_Viseme_PP = 52,
        Pico_Viseme_CH = 53,
        Pico_Viseme_o = 54,
        Pico_Viseme_O = 55,
        Pico_Viseme_I = 56,
        Pico_Viseme_u = 57,
        Pico_Viseme_RR = 58,
        Pico_Viseme_XX = 59,
        Pico_Viseme_aa = 60,
        Pico_Viseme_i = 61,
        Pico_Viseme_FF = 62,
        Pico_Viseme_U = 63,
        Pico_Viseme_TH = 64,
        Pico_Viseme_kk = 65,
        Pico_Viseme_SS = 66,
        Pico_Viseme_e = 67,
        Pico_Viseme_DD = 68,
        Pico_Viseme_E = 69,
        Pico_Viseme_nn = 70,
        Pico_Viseme_sil = 71,
        Pico_FaceMax = 72,
    };

    public class ALVRModule : ExtTrackingModule
    {
        const int PORT = 0xA1F7;

        readonly UdpClient socket = new(PORT);

        public override (bool SupportsEye, bool SupportsExpression) Supported => (true, true);

        public override (bool eyeSuccess, bool expressionSuccess) Initialize(bool eyeAvailable, bool expressionAvailable)
        {
            ModuleInformation.Name = "ALVR";

            var stream = GetType().Assembly.GetManifestResourceStream("ALVRModule.Assets.alvr.png");
            ModuleInformation.StaticImages = stream != null ? new List<Stream> { stream } : ModuleInformation.StaticImages;

            socket.Client.ReceiveTimeout = 100;

            return (true, true);
        }

        private static float[] GetParams(byte[] packet, ref int cursor, int param_count)
        {
            float[] data = new float[param_count];
            for (int i = 0; i < param_count; i++)
            {
                data[i] = BitConverter.ToSingle(packet, cursor + i * 4);
            }
            cursor += param_count * 4;

            return data;
        }

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

        static void SetParam(float[] data, FaceFb input, UnifiedExpressions outputType)
        {
            UnifiedTracking.Data.Shapes[(int)outputType].Weight = data[(int)input];
        }

        static void SetParam(float[] data, UnifiedExpressions outputType, FacePico input)
        {
            UnifiedTracking.Data.Shapes[(int)outputType].Weight = data[(int)input];
        }

        private static void SetFace1FbParams(float[] p)
        {
            // Debug.Assert(p.Length == (int)Face1FbMax);

            var eye = UnifiedTracking.Data.Eye;
            var expr = UnifiedTracking.Data.Shapes;

            eye.Left.Openness = 1.0f - (float)Math.Max(0, Math.Min(1, p[(int)EyesClosedL] + p[(int)EyesClosedL] * p[(int)LidTightenerL]));
            eye.Right.Openness = 1.0f - (float)Math.Max(0, Math.Min(1, p[(int)EyesClosedR] + p[(int)EyesClosedR] * p[(int)LidTightenerR]));


            // Eyelids
            SetParam(p, LidTightenerR, EyeSquintRight);
            SetParam(p, LidTightenerL, EyeSquintLeft);
            SetParam(p, UpperLidRaiserR, EyeWideRight);
            SetParam(p, UpperLidRaiserL, EyeWideLeft);

            // Eyebrows
            SetParam(p, BrowLowererR, BrowPinchRight);
            SetParam(p, BrowLowererL, BrowPinchLeft);
            SetParam(p, BrowLowererR, BrowLowererRight);
            SetParam(p, BrowLowererL, BrowLowererLeft);
            SetParam(p, InnerBrowRaiserR, BrowInnerUpRight);
            SetParam(p, InnerBrowRaiserL, BrowInnerUpLeft);
            SetParam(p, OuterBrowRaiserR, BrowOuterUpRight);
            SetParam(p, OuterBrowRaiserL, BrowOuterUpLeft);

            // Cheeks
            SetParam(p, CheekRaiserR, CheekSquintRight);
            SetParam(p, CheekRaiserL, CheekSquintLeft);
            SetParam(p, CheekPuffR, CheekPuffRight);
            SetParam(p, CheekPuffL, CheekPuffLeft);
            SetParam(p, CheekSuckR, CheekSuckRight);
            SetParam(p, CheekSuckL, CheekSuckLeft);

            // Jaw
            SetParam(p, JawDrop, JawOpen);
            SetParam(p, JawSidewaysRight, JawRight);
            SetParam(p, JawSidewaysLeft, JawLeft);
            SetParam(p, JawThrust, JawForward);
            SetParam(p, LipsToward, MouthClosed);

            //Lip push/pull
            expr[(int)LipSuckUpperRight].Weight =
                Math.Min(1f - (float)Math.Pow(p[(int)UpperLipRaiserR], 1f / 6f), p[(int)LipSuckRT]);
            expr[(int)LipSuckUpperLeft].Weight =
                Math.Min(1f - (float)Math.Pow(p[(int)UpperLipRaiserL], 1f / 6f), p[(int)LipSuckLT]);
            SetParam(p, LipSuckRB, LipSuckLowerRight);
            SetParam(p, LipSuckLB, LipSuckLowerLeft);
            SetParam(p, LipFunnelerRT, LipFunnelUpperRight);
            SetParam(p, LipFunnelerLT, LipFunnelUpperLeft);
            SetParam(p, LipFunnelerRB, LipFunnelLowerRight);
            SetParam(p, LipFunnelerLB, LipFunnelLowerLeft);
            SetParam(p, LipPuckerR, LipPuckerUpperRight);
            SetParam(p, LipPuckerL, LipPuckerUpperLeft);
            SetParam(p, LipPuckerR, LipPuckerLowerRight);
            SetParam(p, LipPuckerL, LipPuckerLowerLeft);

            // Upper lip raiser
            expr[(int)MouthUpperUpRight].Weight = Math.Max(0, p[(int)UpperLipRaiserR] - p[(int)NoseWrinklerR]);
            expr[(int)MouthUpperUpLeft].Weight = Math.Max(0, p[(int)UpperLipRaiserL] - p[(int)NoseWrinklerL]);
            expr[(int)MouthUpperDeepenRight].Weight = Math.Max(0, p[(int)UpperLipRaiserR] - p[(int)NoseWrinklerR]);
            expr[(int)MouthUpperDeepenLeft].Weight = Math.Max(0, p[(int)UpperLipRaiserL] - p[(int)NoseWrinklerL]);
            SetParam(p, NoseWrinklerR, NoseSneerRight);
            SetParam(p, NoseWrinklerL, NoseSneerLeft);

            // Lower lip depressor
            SetParam(p, LowerLipDepressorR, MouthLowerDownRight);
            SetParam(p, LowerLipDepressorL, MouthLowerDownLeft);

            // Mouth direction
            SetParam(p, MouthRight, MouthUpperRight);
            SetParam(p, MouthLeft, MouthUpperLeft);
            SetParam(p, MouthRight, MouthLowerRight);
            SetParam(p, MouthLeft, MouthLowerLeft);

            // Smile
            SetParam(p, LipCornerPullerR, MouthCornerPullRight);
            SetParam(p, LipCornerPullerL, MouthCornerPullLeft);
            SetParam(p, LipCornerPullerR, MouthCornerSlantRight);
            SetParam(p, LipCornerPullerL, MouthCornerSlantLeft);

            // Frown
            SetParam(p, LipCornerDepressorR, MouthFrownRight);
            SetParam(p, LipCornerDepressorL, MouthFrownLeft);
            SetParam(p, LipStretcherR, MouthStretchRight);
            SetParam(p, LipStretcherL, MouthStretchLeft);

            SetParam(p, DimplerL, MouthDimpleLeft);
            SetParam(p, DimplerR, MouthDimpleRight);

            SetParam(p, ChinRaiserT, MouthRaiserUpper);
            SetParam(p, ChinRaiserB, MouthRaiserLower);
            SetParam(p, LipPressorR, MouthPressRight);
            SetParam(p, LipPressorL, MouthPressLeft);
            SetParam(p, LipTightenerR, MouthTightenerRight);
            SetParam(p, LipTightenerL, MouthTightenerLeft);
        }

        private static void SetFace2FbParams(float[] p)
        {
            Debug.Assert(p.Length == (int)Face2FbMax);

            SetFace1FbParams(p);

            SetParam(p, TongueOutFb, TongueOut);
        }

        private static void SetFacePicoParams(float[] p)
        {
            var expr = UnifiedTracking.Data.Shapes;

            UnifiedTracking.Data.Eye.Right.Openness = 1.0f - Math.Clamp(p[(int)Pico_EyeBlink_R] + p[(int)Pico_EyeBlink_R] * p[(int)Pico_EyeSquint_R], 0.0f, 1.0f);
            UnifiedTracking.Data.Eye.Left .Openness = 1.0f - Math.Clamp(p[(int)Pico_EyeBlink_L] + p[(int)Pico_EyeBlink_L] * p[(int)Pico_EyeSquint_L], 0.0f, 1.0f);

            #region Eye Expressions

            SetParam(p, EyeSquintRight, Pico_EyeSquint_R);
            SetParam(p, EyeSquintLeft, Pico_EyeSquint_L);
            SetParam(p, EyeWideRight, Pico_EyeWide_R);
            SetParam(p, EyeWideLeft, Pico_EyeWide_L);

            #endregion

            #region Eyebrow Expressions

            SetParam(p, BrowPinchRight, Pico_BrowDown_R);
            SetParam(p, BrowPinchLeft, Pico_BrowDown_L);
            SetParam(p, BrowLowererRight, Pico_BrowDown_R);
            SetParam(p, BrowLowererLeft, Pico_BrowDown_L);
            SetParam(p, BrowInnerUpRight, Pico_BrowInnerUp);
            SetParam(p, BrowInnerUpLeft, Pico_BrowInnerUp);
            SetParam(p, BrowOuterUpRight, Pico_BrowOuterUp_R);
            SetParam(p, BrowOuterUpLeft, Pico_BrowOuterUp_L);

            #endregion

            #region Cheek Expressions

            SetParam(p, CheekSquintRight, Pico_CheekSquint_R);
            SetParam(p, CheekSquintLeft, Pico_CheekSquint_L);
            SetParam(p, CheekPuffRight, Pico_CheekPuff);
            SetParam(p, CheekPuffLeft, Pico_CheekPuff);

            #endregion

            #region Jaw Exclusive Expressions

            SetParam(p, JawOpen, Pico_JawOpen);
            SetParam(p, JawRight, Pico_JawRight);
            SetParam(p, JawLeft, Pico_JawLeft);
            SetParam(p, JawForward, Pico_JawForward);
            SetParam(p, MouthClosed, Pico_MouthClose);

            #endregion

            #region Lip Expressions

            SetParam(p, LipSuckUpperRight, Pico_MouthRollUpper);
            SetParam(p, LipSuckUpperLeft, Pico_MouthRollUpper);
            SetParam(p, LipSuckLowerRight, Pico_MouthRollLower);
            SetParam(p, LipSuckLowerLeft, Pico_MouthRollLower);

            SetParam(p, LipFunnelUpperRight, Pico_MouthFunnel);
            SetParam(p, LipFunnelUpperLeft, Pico_MouthFunnel);
            SetParam(p, LipFunnelLowerRight, Pico_MouthFunnel);
            SetParam(p, LipFunnelLowerLeft, Pico_MouthFunnel);

            SetParam(p, LipPuckerUpperRight, Pico_MouthPucker);
            SetParam(p, LipPuckerUpperLeft, Pico_MouthPucker);
            SetParam(p, LipPuckerLowerRight, Pico_MouthPucker);
            SetParam(p, LipPuckerLowerLeft, Pico_MouthPucker);

            expr[(int)MouthUpperUpRight]    .Weight = Math.Max(0, p[(int)Pico_MouthUpperUp_R] - p[(int)Pico_NoseSneer_R]);
            expr[(int)MouthUpperUpLeft]     .Weight = Math.Max(0, p[(int)Pico_MouthUpperUp_L] - p[(int)Pico_NoseSneer_L]);
            expr[(int)MouthUpperDeepenRight].Weight = Math.Max(0, p[(int)Pico_MouthUpperUp_R] - p[(int)Pico_NoseSneer_R]);
            expr[(int)MouthUpperDeepenLeft] .Weight = Math.Max(0, p[(int)Pico_MouthUpperUp_L] - p[(int)Pico_NoseSneer_L]);
            SetParam(p, NoseSneerRight, Pico_NoseSneer_R);
            SetParam(p, NoseSneerLeft, Pico_NoseSneer_L);

            SetParam(p, MouthLowerDownRight, Pico_MouthLowerDown_R);
            SetParam(p, MouthLowerDownLeft, Pico_MouthLowerDown_L);

            SetParam(p, MouthUpperRight, Pico_MouthRight);
            SetParam(p, MouthUpperLeft, Pico_MouthLeft);
            SetParam(p, MouthLowerRight, Pico_MouthRight);
            SetParam(p, MouthLowerLeft, Pico_MouthLeft);

            SetParam(p, MouthCornerPullRight, Pico_MouthSmile_R);
            SetParam(p, MouthCornerPullLeft, Pico_MouthSmile_L);
            SetParam(p, MouthCornerSlantRight, Pico_MouthSmile_R);
            SetParam(p, MouthCornerSlantLeft, Pico_MouthSmile_L);

            SetParam(p, MouthFrownRight, Pico_MouthFrown_R);
            SetParam(p, MouthFrownLeft, Pico_MouthFrown_L);
            SetParam(p, MouthStretchRight, Pico_MouthStretch_R);
            SetParam(p, MouthStretchLeft, Pico_MouthStretch_L);

            SetParam(p, MouthDimpleRight, Pico_MouthDimple_R);
            SetParam(p, MouthDimpleLeft, Pico_MouthDimple_L);

            SetParam(p, MouthRaiserUpper, Pico_MouthShrugUpper);
            SetParam(p, MouthRaiserLower, Pico_MouthShrugLower);
            SetParam(p, MouthPressRight, Pico_MouthPress_R);
            SetParam(p, MouthPressLeft, Pico_MouthPress_L);

            #endregion

            #region Tongue Expressions

            SetParam(p, TongueOut, Pico_TongueOut);

            #endregion
        }

        public override void Update()
        {
            byte[] packet;
            try
            {
                var peer = new IPEndPoint(IPAddress.Any, PORT);
                packet = socket.Receive(ref peer);
            }
            catch (Exception)
            {
                return;
            }

            int cursor = 0;
            while (cursor < packet.Length)
            {
                string str = Encoding.ASCII.GetString(packet[cursor..(cursor + 8)], 0, 8);
                cursor += 8;

                switch (str)
                {
                    case "EyesQuat":
                        SetEyesQuatParams(GetParams(packet, ref cursor, 8));
                        break;
                    case "CombQuat":
                        SetCombEyesQuatParams(GetParams(packet, ref cursor, 4));
                        break;
                    case "FaceFb\0\0":
                        SetFace1FbParams(GetParams(packet, ref cursor, (int)Face1FbMax));
                        break;
                    case "Face2Fb\0":
                        SetFace2FbParams(GetParams(packet, ref cursor, (int)Face2FbMax));
                        break;
                    case "FacePico":
                        SetFacePicoParams(GetParams(packet, ref cursor, (int)Pico_FaceMax));
                        break;
                    default:
                        Logger.LogError($"[ALVR Module] Unrecognized prefix: {str}");
                        break;
                }
            }
        }

        public override void Teardown()
        {
            socket.Close();
        }
    }
}

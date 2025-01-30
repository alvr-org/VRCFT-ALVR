using VRCFaceTracking;
using VRCFaceTracking.Core.Params.Expressions;
using static VRCFaceTracking.Core.Params.Expressions.UnifiedExpressions;

namespace ALVRModule
{
    using static FacePico;

    public enum FacePico
    {
        EyeLookDownL = 0,
        NoseSneerL = 1,
        EyeLookInL = 2,
        BrowInnerUp = 3,
        BrowDownR = 4,
        MouthClose = 5,
        MouthLowerDownR = 6,
        JawShapeOpen = 7,
        MouthUpperUpR = 8,
        MouthShrugUpper = 9,
        MouthFunnel = 10,
        EyeLookInR = 11,
        EyeLookDownR = 12,
        NoseSneerR = 13,
        MouthRollUpper = 14,
        JawShapeRight = 15,
        BrowDownL = 16,
        MouthShrugLower = 17,
        MouthRollLower = 18,
        MouthSmileL = 19,
        MouthPressL = 20,
        MouthSmileR = 21,
        MouthPressR = 22,
        MouthDimpleR = 23,
        MouthLeft = 24,
        JawShapeForward = 25,
        EyeSquintL = 26,
        MouthFrownL = 27,
        EyeBlinkL = 28,
        CheekSquintL = 29,
        BrowOuterUpL = 30,
        EyeLookUpL = 31,
        JawShapeLeft = 32,
        MouthStretchL = 33,
        MouthPucker = 34,
        EyeLookUpR = 35,
        BrowOuterUpR = 36,
        CheekSquintR = 37,
        EyeBlinkR = 38,
        MouthUpperUpL = 39,
        MouthFrownR = 40,
        EyeSquintR = 41,
        MouthStretchR = 42,
        CheekPuff = 43,
        EyeLookOutL = 44,
        EyeLookOutR = 45,
        EyeWideR = 46,
        EyeWideL = 47,
        MouthRight = 48,
        MouthDimpleL = 49,
        MouthLowerDownL = 50,
        TongueShapeOut = 51,
        VisemePP = 52,
        VisemeCH = 53,
        Visemeo = 54,
        VisemeO = 55,
        VisemeI = 56,
        Visemeu = 57,
        VisemeRR = 58,
        VisemeXX = 59,
        Visemeaa = 60,
        Visemei = 61,
        VisemeFF = 62,
        VisemeU = 63,
        VisemeTH = 64,
        Visemekk = 65,
        VisemeSS = 66,
        Visemee = 67,
        VisemeDD = 68,
        VisemeE = 69,
        Visemenn = 70,
        Visemesil = 71,
        FaceMax = 72,
    };

    public class PicoFaceTracking : BaseFaceTracking
    {
        private static void SetParam(float[] data, UnifiedExpressions outputType, FacePico input)
        {
            UnifiedTracking.Data.Shapes[(int)outputType].Weight = data[(int)input];
        }

        private static void SetFacePicoParams(float[] p)
        {
            var expr = UnifiedTracking.Data.Shapes;

            UnifiedTracking.Data.Eye.Right.Openness = 1.0f - Math.Clamp(p[(int)EyeBlinkR] + p[(int)EyeBlinkR] * p[(int)EyeSquintR], 0.0f, 1.0f);
            UnifiedTracking.Data.Eye.Left.Openness = 1.0f - Math.Clamp(p[(int)EyeBlinkL] + p[(int)EyeBlinkL] * p[(int)EyeSquintL], 0.0f, 1.0f);

            #region Eye Expressions

            SetParam(p, EyeSquintRight, EyeSquintR);
            SetParam(p, EyeSquintLeft, EyeSquintL);
            SetParam(p, EyeWideRight, EyeWideR);
            SetParam(p, EyeWideLeft, EyeWideL);

            #endregion

            #region Eyebrow Expressions

            SetParam(p, BrowPinchRight, BrowDownR);
            SetParam(p, BrowPinchLeft, BrowDownL);
            SetParam(p, BrowLowererRight, BrowDownR);
            SetParam(p, BrowLowererLeft, BrowDownL);
            SetParam(p, BrowInnerUpRight, BrowInnerUp);
            SetParam(p, BrowInnerUpLeft, BrowInnerUp);
            SetParam(p, BrowOuterUpRight, BrowOuterUpR);
            SetParam(p, BrowOuterUpLeft, BrowOuterUpL);

            #endregion

            #region Cheek Expressions

            SetParam(p, CheekSquintRight, CheekSquintR);
            SetParam(p, CheekSquintLeft, CheekSquintL);
            SetParam(p, CheekPuffRight, CheekPuff);
            SetParam(p, CheekPuffLeft, CheekPuff);

            #endregion

            #region Jaw Exclusive Expressions

            SetParam(p, JawOpen, JawShapeOpen);
            SetParam(p, JawRight, JawShapeRight);
            SetParam(p, JawLeft, JawShapeLeft);
            SetParam(p, JawForward, JawShapeForward);
            SetParam(p, MouthClosed, MouthClose);

            #endregion

            #region Lip Expressions

            SetParam(p, LipSuckUpperRight, MouthRollUpper);
            SetParam(p, LipSuckUpperLeft, MouthRollUpper);
            SetParam(p, LipSuckLowerRight, MouthRollLower);
            SetParam(p, LipSuckLowerLeft, MouthRollLower);

            SetParam(p, LipFunnelUpperRight, MouthFunnel);
            SetParam(p, LipFunnelUpperLeft, MouthFunnel);
            SetParam(p, LipFunnelLowerRight, MouthFunnel);
            SetParam(p, LipFunnelLowerLeft, MouthFunnel);

            SetParam(p, LipPuckerUpperRight, MouthPucker);
            SetParam(p, LipPuckerUpperLeft, MouthPucker);
            SetParam(p, LipPuckerLowerRight, MouthPucker);
            SetParam(p, LipPuckerLowerLeft, MouthPucker);

            expr[(int)MouthUpperUpRight].Weight = Math.Max(0, p[(int)MouthUpperUpR] - p[(int)NoseSneerR]);
            expr[(int)MouthUpperUpLeft].Weight = Math.Max(0, p[(int)MouthUpperUpL] - p[(int)NoseSneerL]);
            expr[(int)MouthUpperDeepenRight].Weight = Math.Max(0, p[(int)MouthUpperUpR] - p[(int)NoseSneerR]);
            expr[(int)MouthUpperDeepenLeft].Weight = Math.Max(0, p[(int)MouthUpperUpL] - p[(int)NoseSneerL]);
            SetParam(p, NoseSneerRight, NoseSneerR);
            SetParam(p, NoseSneerLeft, NoseSneerL);

            SetParam(p, MouthLowerDownRight, MouthLowerDownR);
            SetParam(p, MouthLowerDownLeft, MouthLowerDownL);

            SetParam(p, MouthUpperRight, MouthRight);
            SetParam(p, MouthUpperLeft, MouthLeft);
            SetParam(p, MouthLowerRight, MouthRight);
            SetParam(p, MouthLowerLeft, MouthLeft);

            SetParam(p, MouthCornerPullRight, MouthSmileR);
            SetParam(p, MouthCornerPullLeft, MouthSmileL);
            SetParam(p, MouthCornerSlantRight, MouthSmileR);
            SetParam(p, MouthCornerSlantLeft, MouthSmileL);

            SetParam(p, MouthFrownRight, MouthFrownR);
            SetParam(p, MouthFrownLeft, MouthFrownL);
            SetParam(p, MouthStretchRight, MouthStretchR);
            SetParam(p, MouthStretchLeft, MouthStretchL);

            SetParam(p, MouthDimpleRight, MouthDimpleR);
            SetParam(p, MouthDimpleLeft, MouthDimpleL);

            SetParam(p, MouthRaiserUpper, MouthShrugUpper);
            SetParam(p, MouthRaiserLower, MouthShrugLower);
            SetParam(p, MouthPressRight, MouthPressR);
            SetParam(p, MouthPressLeft, MouthPressL);

            #endregion

            #region Tongue Expressions

            SetParam(p, TongueOut, TongueShapeOut);

            #endregion
        }

        public override bool ConsumePacket(byte[] packet, ref int cursor, string prefix)
        {
            switch (prefix)
            {
                case "FacePico":
                    SetFacePicoParams(GetParams(packet, ref cursor, (int)FaceMax));
                    return true;
                default:
                    return false;
            }
        }
    }
}

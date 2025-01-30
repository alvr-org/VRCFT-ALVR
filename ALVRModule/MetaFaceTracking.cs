using System.Diagnostics;
using VRCFaceTracking;
using VRCFaceTracking.Core.Params.Expressions;
using static VRCFaceTracking.Core.Params.Expressions.UnifiedExpressions;

namespace ALVRModule
{
    using static FaceFb;

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

    public class MetaFaceTracking : BaseFaceTracking
    {
        private static void SetParam(float[] data, FaceFb input, UnifiedExpressions outputType)
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

        public override bool ConsumePacket(byte[] packet, ref int cursor, string prefix)
        {
            switch (prefix)
            {
                case "FaceFb\0\0":
                    SetFace1FbParams(GetParams(packet, ref cursor, (int)Face1FbMax));
                    return true;
                case "Face2Fb\0":
                    SetFace2FbParams(GetParams(packet, ref cursor, (int)Face2FbMax));
                    return true;
                default:
                    return false;
            }
        }
    }
}

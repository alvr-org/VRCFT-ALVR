using static VRCFaceTracking.Core.Params.Expressions.UnifiedExpressions;
using VRCFaceTracking.Core.Params.Data;

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

    public class FbFaceTracking
    {
        private static void SetFaceFbParams(FloatParams p, FloatWeightParams w, UnifiedEyeData eye)
        {
            eye.Right.Openness = 1.0f - Math.Clamp(p[EyesClosedR] + p[EyesClosedR] * p[LidTightenerR], 0.0f, 1.0f);
            eye.Left.Openness = 1.0f - Math.Clamp(p[EyesClosedL] + p[EyesClosedL] * p[LidTightenerL], 0.0f, 1.0f);

            #region Eyelids

            w[EyeSquintRight] = p[LidTightenerR];
            w[EyeSquintLeft] = p[LidTightenerL];
            w[EyeWideRight] = p[UpperLidRaiserR];
            w[EyeWideLeft] = p[UpperLidRaiserL];

            #endregion

            #region Eyebrows

            w[BrowPinchRight] = p[BrowLowererR];
            w[BrowPinchLeft] = p[BrowLowererL];
            w[BrowLowererRight] = p[BrowLowererR];
            w[BrowLowererLeft] = p[BrowLowererL];
            w[BrowInnerUpRight] = p[InnerBrowRaiserR];
            w[BrowInnerUpLeft] = p[InnerBrowRaiserL];
            w[BrowOuterUpRight] = p[OuterBrowRaiserR];
            w[BrowOuterUpLeft] = p[OuterBrowRaiserL];

            #endregion

            #region Cheeks

            w[CheekSquintRight] = p[CheekRaiserR];
            w[CheekSquintLeft] = p[CheekRaiserL];
            w[CheekPuffRight] = p[CheekPuffR];
            w[CheekPuffLeft] = p[CheekPuffL];
            w[CheekSuckRight] = p[CheekSuckR];
            w[CheekSuckLeft] = p[CheekSuckL];

            #endregion

            #region Jaw

            w[JawOpen] = p[JawDrop];
            w[JawRight] = p[JawSidewaysRight];
            w[JawLeft] = p[JawSidewaysLeft];
            w[JawForward] = p[JawThrust];
            w[MouthClosed] = p[LipsToward];

            #endregion

            #region Lip push/pull

            w[LipSuckUpperRight] = Math.Min(1f - (float)Math.Pow(p[UpperLipRaiserR], 1d / 6d), p[LipSuckRT]);
            w[LipSuckUpperLeft] = Math.Min(1f - (float)Math.Pow(p[UpperLipRaiserL], 1d / 6d), p[LipSuckLT]);
            w[LipSuckLowerRight] = p[LipSuckRB];
            w[LipSuckLowerLeft] = p[LipSuckLB];
            w[LipFunnelUpperRight] = p[LipFunnelerRT];
            w[LipFunnelUpperLeft] = p[LipFunnelerLT];
            w[LipFunnelLowerRight] = p[LipFunnelerRB];
            w[LipFunnelLowerLeft] = p[LipFunnelerLB];
            w[LipPuckerUpperRight] = p[LipPuckerR];
            w[LipPuckerUpperLeft] = p[LipPuckerL];
            w[LipPuckerLowerRight] = p[LipPuckerR];
            w[LipPuckerLowerLeft] = p[LipPuckerL];

            #endregion

            #region Upper lip raiser

            w[MouthUpperUpRight] = Math.Max(0, p[UpperLipRaiserR] - p[NoseWrinklerR]);
            w[MouthUpperUpLeft] = Math.Max(0, p[UpperLipRaiserL] - p[NoseWrinklerL]);
            w[MouthUpperDeepenRight] = Math.Max(0, p[UpperLipRaiserR] - p[NoseWrinklerR]);
            w[MouthUpperDeepenLeft] = Math.Max(0, p[UpperLipRaiserL] - p[NoseWrinklerL]);
            w[NoseSneerRight] = p[NoseWrinklerR];
            w[NoseSneerLeft] = p[NoseWrinklerL];

            #endregion

            #region Lower lip depressor

            w[MouthLowerDownRight] = p[LowerLipDepressorR];
            w[MouthLowerDownLeft] = p[LowerLipDepressorL];

            #endregion

            #region Mouth direction

            w[MouthUpperRight] = p[MouthRight];
            w[MouthUpperLeft] = p[MouthLeft];
            w[MouthLowerRight] = p[MouthRight];
            w[MouthLowerLeft] = p[MouthLeft];

            #endregion

            #region Smile

            w[MouthCornerPullRight] = p[LipCornerPullerR];
            w[MouthCornerPullLeft] = p[LipCornerPullerL];
            w[MouthCornerSlantRight] = p[LipCornerPullerR];
            w[MouthCornerSlantLeft] = p[LipCornerPullerL];

            #endregion

            #region Frown

            w[MouthFrownRight] = p[LipCornerDepressorR];
            w[MouthFrownLeft] = p[LipCornerDepressorL];
            w[MouthStretchRight] = p[LipStretcherR];
            w[MouthStretchLeft] = p[LipStretcherL];

            w[MouthDimpleLeft] = p[DimplerL];
            w[MouthDimpleRight] = p[DimplerR];

            w[MouthRaiserUpper] = p[ChinRaiserT];
            w[MouthRaiserLower] = p[ChinRaiserB];
            w[MouthPressRight] = p[LipPressorR];
            w[MouthPressLeft] = p[LipPressorL];
            w[MouthTightenerRight] = p[LipTightenerR];
            w[MouthTightenerLeft] = p[LipTightenerL];

            #endregion
        }

        public static void SetFace1FbParams(FloatParams p, FloatWeightParams w, UnifiedEyeData eye)
        {
            p.Read((int)Face1FbMax);

            SetFaceFbParams(p, w, eye);
        }

        public static void SetFace2FbParams(FloatParams p, FloatWeightParams w, UnifiedEyeData eye)
        {
            p.Read((int)Face2FbMax);

            SetFaceFbParams(p, w, eye);

            w[TongueOut] = p[TongueOutFb];
        }
    }
}

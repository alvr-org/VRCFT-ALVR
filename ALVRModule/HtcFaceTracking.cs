// Code inspired from https://github.com/VRCFaceTracking/SRanipalTrackingModule/blob/master/SRanipalExtTrackingModule/SRanipalTrackingInterface.cs

using static VRCFaceTracking.Core.Params.Expressions.UnifiedExpressions;
using U = VRCFaceTracking.Core.Params.Expressions.UnifiedExpressions;
using VRCFaceTracking.Core.Params.Data;


namespace ALVRModule
{
    using static EyesHtc;
    using static LipHtc;

    public enum EyesHtc
    {
        LeftBlink = 0,
        LeftWide = 1,
        RightBlink = 2,
        RightWide = 3,
        LeftSqueeze = 4,
        RightSqueeze = 5,
        LeftDown = 6,
        RightDown = 7,
        LeftOut = 8,
        RightIn = 9,
        LeftIn = 10,
        RightOut = 11,
        LeftUp = 12,
        RightUp = 13,
        EyesMax = 14,
    };

    public enum LipHtc
    {
        JawRight = 0,
        JawLeft = 1,
        JawForward = 2,
        JawOpen = 3,
        MouthApeShape = 4,
        MouthUpperRight = 5,
        MouthUpperLeft = 6,
        MouthLowerRight = 7,
        MouthLowerLeft = 8,
        MouthUpperOverturn = 9,
        MouthLowerOverturn = 10,
        MouthPout = 11,
        MouthSmileRight = 12,
        MouthSmileLeft = 13,
        MouthSadRight = 14,
        MouthSadLeft = 15,
        CheekPuffRight = 16,
        CheekPuffLeft = 17,
        CheekSuck = 18,
        MouthUpperUpright = 19,
        MouthUpperUpleft = 20,
        MouthLowerDownright = 21,
        MouthLowerDownleft = 22,
        MouthUpperInside = 23,
        MouthLowerInside = 24,
        MouthLowerOverlay = 25,
        TongueLongstep1 = 26,
        TongueLeft = 27,
        TongueRight = 28,
        TongueUp = 29,
        TongueDown = 30,
        TongueRoll = 31,
        TongueLongstep2 = 32,
        TongueUprightMorph = 33,
        TongueUpleftMorph = 34,
        TongueDownrightMorph = 35,
        TongueDownleftMorph = 36,
        LipMax = 37,
    }

    public class HtcFaceTracking
    {
        public static void SetEyesHtcParams(FloatParams p, FloatWeightParams w, UnifiedEyeData eye)
        {
            p.Read((int)EyesMax);

            eye.Right.Openness = 1.0f - Math.Clamp(p[RightBlink] + p[RightBlink] * p[RightSqueeze], 0.0f, 1.0f);
            eye.Left.Openness = 1.0f - Math.Clamp(p[LeftBlink] + p[LeftBlink] * p[LeftSqueeze], 0.0f, 1.0f);

            eye.Right.Gaze.x = p[RightOut] - p[RightIn];
            eye.Right.Gaze.y = p[RightUp] - p[RightDown];
            eye.Left.Gaze.x = -p[LeftOut] + p[LeftIn];
            eye.Left.Gaze.y = p[LeftUp] - p[LeftDown];

            eye.Left.PupilDiameter_MM = 5f;
            eye.Right.PupilDiameter_MM = 5f;

            eye._minDilation = 0;
            eye._maxDilation = 10;

            w[EyeWideLeft] = p[LeftWide];
            w[EyeWideRight] = p[RightWide];

            w[EyeSquintLeft] = p[LeftSqueeze];
            w[EyeSquintRight] = p[RightSqueeze];

            w[BrowInnerUpLeft] = p[LeftWide];
            w[BrowOuterUpLeft] = p[LeftWide];

            w[BrowInnerUpRight] = p[RightWide];
            w[BrowOuterUpRight] = p[RightWide];

            w[BrowPinchLeft] = p[LeftSqueeze];
            w[BrowLowererLeft] = p[LeftSqueeze];

            w[BrowPinchRight] = p[RightSqueeze];
            w[BrowLowererRight] = p[RightSqueeze];
        }

        public static void SetLipHtcParams(FloatParams p, FloatWeightParams w, UnifiedEyeData eye)
        {
            p.Read((int)LipMax);
            
            #region Direct Jaw

            w[U.JawOpen] = p[JawOpen] + p[MouthApeShape];
            w[U.JawLeft] = p[JawLeft];
            w[U.JawRight] = p[JawRight];
            w[U.JawForward] = p[JawForward];
            w[MouthClosed] = p[MouthApeShape];

            #endregion

            #region Direct Mouth and Lip

            w[MouthUpperUpRight] = p[MouthUpperRight] - p[MouthUpperOverturn];
            w[MouthUpperDeepenRight] = p[MouthUpperRight] - p[MouthUpperOverturn];
            w[MouthUpperUpLeft] = p[MouthUpperLeft] - p[MouthUpperOverturn];
            w[MouthUpperDeepenLeft] = p[MouthUpperLeft] - p[MouthUpperOverturn];

            w[MouthLowerDownLeft] = p[MouthLowerLeft] - p[MouthLowerOverturn];
            w[MouthLowerDownRight] = p[MouthLowerRight] - p[MouthLowerOverturn];

            w[LipPuckerUpperLeft] = p[MouthPout];
            w[LipPuckerLowerLeft] = p[MouthPout];
            w[LipPuckerUpperRight] = p[MouthPout];
            w[LipPuckerLowerRight] = p[MouthPout];

            w[LipFunnelUpperLeft] = p[MouthUpperOverturn];
            w[LipFunnelUpperRight] = p[MouthUpperOverturn];
            w[LipFunnelLowerLeft] = p[MouthUpperOverturn];
            w[LipFunnelLowerRight] = p[MouthUpperOverturn];

            w[LipSuckUpperLeft] = p[MouthUpperInside];
            w[LipSuckUpperRight] = p[MouthUpperInside];
            w[LipSuckLowerLeft] = p[MouthLowerInside];
            w[LipSuckLowerRight] = p[MouthLowerInside];

            w[U.MouthUpperLeft] = p[MouthUpperLeft];
            w[U.MouthUpperRight] = p[MouthUpperRight];
            w[U.MouthLowerLeft] = p[MouthLowerLeft];
            w[U.MouthLowerRight] = p[MouthLowerRight];

            w[MouthCornerPullLeft] = p[MouthSmileLeft];
            w[MouthCornerPullRight] = p[MouthSmileRight];
            w[MouthCornerSlantLeft] = p[MouthSmileLeft];
            w[MouthCornerSlantRight] = p[MouthSmileRight];
            w[MouthFrownLeft] = p[MouthSadLeft];
            w[MouthFrownRight] = p[MouthSadRight];

            w[MouthRaiserUpper] = p[MouthLowerOverlay] - p[MouthUpperInside];
            w[MouthRaiserLower] = p[MouthLowerOverlay];

            #endregion

            #region Direct Cheek

            w[U.CheekPuffLeft] = p[CheekPuffLeft];
            w[U.CheekPuffRight] = p[CheekPuffRight];

            w[CheekSuckLeft] = p[CheekSuck];
            w[CheekSuckRight] = p[CheekSuck];

            #endregion

            #region Direct Tongue

            w[TongueOut] = (p[TongueLongstep1] + p[TongueLongstep2]) / 2.0f;
            w[U.TongueUp] = p[TongueUp];
            w[U.TongueDown] = p[TongueDown];
            w[U.TongueLeft] = p[TongueLeft];
            w[U.TongueRight] = p[TongueRight];
            w[U.TongueRoll] = p[TongueRoll];

            #endregion

            #region Emulated Unified Mapping

            w[CheekSquintLeft] = p[MouthSmileLeft];
            w[CheekSquintRight] = p[MouthSmileRight];

            w[MouthDimpleLeft] = p[MouthSmileLeft];
            w[MouthDimpleRight] = p[MouthSmileRight];

            w[MouthStretchLeft] = p[MouthSadRight];
            w[MouthStretchRight] = p[MouthSadRight];

            #endregion
        }
    }
}

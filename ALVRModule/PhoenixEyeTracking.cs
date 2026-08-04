using VRCFaceTracking.Core.Params.Data;

namespace ALVRModule
{
    using static PhoenixEyeParams;
    enum PhoenixEyeParams
    {
        LeftEyeGazeVectorX,
        LeftEyeGazeVectorY,
        RightEyeGazeVectorX,
        RightEyeGazeVectorY,
        LeftEyeOpenness,
        RightEyeOpenness,
        LeftEyePupilDilation,
        RightEyePupilDilation
    }

    public class PhoenixEyeTracking
    {
        /// <summary>
        /// Below this threshold the pupil dilation will pause.
        /// </summary>
        const float EYE_PUPIL_DILATION_EYE_OPENNESS_THRESHOLD = 0.8f;
        public static void SetEyesPhoenixParams(FloatParams p, FloatWeightParams w, UnifiedEyeData eye)
        {
            p.Read(8);

            #region LEFT EYE
            eye.Left.Gaze.x = p[LeftEyeGazeVectorX];
            eye.Left.Gaze.y = p[LeftEyeGazeVectorY];
            eye.Left.Openness = p[LeftEyeOpenness];

            if (p[LeftEyePupilDilation] == 0)
                p[LeftEyePupilDilation] = 50f;

            if (p[LeftEyeOpenness] > EYE_PUPIL_DILATION_EYE_OPENNESS_THRESHOLD)
                eye.Left.PupilDiameter_MM = p[LeftEyePupilDilation] / 10;
            #endregion

            #region RIGHT EYE
            eye.Right.Gaze.x = p[RightEyeGazeVectorX];
            eye.Right.Gaze.y = p[RightEyeGazeVectorY];
            eye.Right.Openness = p[RightEyeOpenness];

            if (p[RightEyePupilDilation] == 0)
                p[RightEyePupilDilation] = 50f;

            if (p[RightEyeOpenness] > EYE_PUPIL_DILATION_EYE_OPENNESS_THRESHOLD)
                eye.Right.PupilDiameter_MM = p[RightEyePupilDilation] / 10;
            #endregion
        }
    }
}

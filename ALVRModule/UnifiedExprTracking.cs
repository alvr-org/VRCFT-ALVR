using VRCFaceTracking.Core.Params.Expressions;
using VRCFaceTracking.Core.Params.Data;

namespace ALVRModule
{
    public static class UnifiedExprTracking
    {
        public static readonly int ShapeCount = (int)UnifiedExpressions.Max;

        public const float Version = 1f;

        public enum EyeTail
        {
            Version = 0,
            LeftOpenness,
            RightOpenness,
            LeftPupilDiameter,
            RightPupilDiameter,
            MinDilation,
            MaxDilation,
            LeftGazePitch,
            LeftGazeYaw,
            RightGazePitch,
            RightGazeYaw,
            Count,
        }

        public static readonly int ParamCount = ShapeCount + (int)EyeTail.Count;

        public static void SetUnifiedExprParams(FloatParams p, FloatWeightParams w, UnifiedEyeData eye)
        {
            p.Read(ParamCount);

            float[] values = p.Params!;

            for (int i = 0; i < ShapeCount; i++)
                w[(UnifiedExpressions)i] = values[i];

            int t = ShapeCount;
            eye.Left.Openness = values[t + (int)EyeTail.LeftOpenness];
            eye.Right.Openness = values[t + (int)EyeTail.RightOpenness];

            eye.Left.PupilDiameter_MM = values[t + (int)EyeTail.LeftPupilDiameter];
            eye.Right.PupilDiameter_MM = values[t + (int)EyeTail.RightPupilDiameter];
            eye._minDilation = values[t + (int)EyeTail.MinDilation];
            eye._maxDilation = values[t + (int)EyeTail.MaxDilation];

            eye.Left.Gaze.x = values[t + (int)EyeTail.LeftGazePitch];
            eye.Left.Gaze.y = values[t + (int)EyeTail.LeftGazeYaw];
            eye.Right.Gaze.x = values[t + (int)EyeTail.RightGazePitch];
            eye.Right.Gaze.y = values[t + (int)EyeTail.RightGazeYaw];
        }
    }
}

using VRCFaceTracking;
using VRCFaceTracking.Core.Params.Expressions;

namespace ALVRModule
{
    public class FloatParams
    {
        private readonly MemoryStream Stream;

        public float[]? Params
        {
            get; private set;
        }

        public float this[Enum index]
        {
            get
            {
                return Params?[Convert.ToInt32(index)] ?? 0;
            }
        }

        public FloatParams(MemoryStream stream)
        {
            Stream = stream;
        }

        public void Read(int count)
        {
            Params = new float[count];

            byte[] buffer = new byte[4];
            for (int i = 0; i < count; i++)
            {
                Stream.ReadAtLeast(buffer, buffer.Length, false);
                Params[i] = BitConverter.ToSingle(buffer, 0);
            }
        }
    }

    public class FloatWeightParams
    {
        public static readonly FloatWeightParams Instance = new();

        public float this[UnifiedExpressions index]
        {
            set
            {
                UnifiedTracking.Data.Shapes[Convert.ToInt32(index)].Weight = value;
            }
        }

        private FloatWeightParams() { }
    }
}

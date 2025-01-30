namespace ALVRModule
{
    public abstract class BaseFaceTracking
    {
        protected static float[] GetParams(byte[] packet, ref int cursor, int param_count)
        {
            float[] data = new float[param_count];
            for (int i = 0; i < param_count; i++)
            {
                data[i] = BitConverter.ToSingle(packet, cursor + i * 4);
            }
            cursor += param_count * 4;

            return data;
        }

        public abstract bool ConsumePacket(byte[] packet, ref int cursor, string prefix);
    }
}

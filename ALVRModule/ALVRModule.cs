using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Logging;
using VRCFaceTracking;

namespace ALVRModule
{
    public class ALVRModule : ExtTrackingModule
    {
        const int PORT = 0xA1F7;
        const int PREFIX_SIZE = 8;

        readonly UdpClient socket = new(PORT);
        readonly BaseFaceTracking[] knownTrackings = { new EyesFaceTracking(), new MetaFaceTracking(), new PicoFaceTracking() };

        public override (bool SupportsEye, bool SupportsExpression) Supported => (true, true);

        public override (bool eyeSuccess, bool expressionSuccess) Initialize(bool eyeAvailable, bool expressionAvailable)
        {
            ModuleInformation.Name = "ALVR";

            var stream = GetType().Assembly.GetManifestResourceStream("ALVRModule.Assets.alvr.png");
            ModuleInformation.StaticImages = stream != null ? new List<Stream> { stream } : ModuleInformation.StaticImages;

            socket.Client.ReceiveTimeout = 100;

            return (true, true);
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
                string prefix = Encoding.ASCII.GetString(packet[cursor..(cursor + PREFIX_SIZE)], 0, PREFIX_SIZE);
                cursor += PREFIX_SIZE;

                bool consumed = false;

                foreach (var item in knownTrackings)
                {
                    consumed |= item.ConsumePacket(packet, ref cursor, prefix);

                    if (consumed)
                    {
                        break;
                    }
                }

                if (!consumed)
                {
                    Logger.LogError($"[ALVR Module] Unrecognized prefix: {prefix}");
                }
            }
        }

        public override void Teardown()
        {
            socket.Close();
        }
    }
}

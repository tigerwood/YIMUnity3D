using System;

namespace YouMe
{

    public class VoiceClient
    {

        static VoiceClient _ins;
        public static VoiceClient Instance{
            get{
                if(_ins==null){
                    _ins = new VoiceClient();
                }
                return _ins;
            }
        }

        public Action<IConnectEvent> ConnectEvent{set;get;}

        public IClient Initialize(string appKey, string secretKey, ServerZone zone)
        {
            throw new NotImplementedException();
        }

        public void Connect(string userID,string token=""){

        }




    }
}
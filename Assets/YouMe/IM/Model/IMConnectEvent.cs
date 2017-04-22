
using System;

namespace YouMe{
    public class IMConnectEvent : IConnectEvent
    {
        private ErrorCode _code;
        private ConnectEventType _type;
        private string _userID;

        public ErrorCode Code { 
            get{
                return _code;
            } 
        }

        public ConnectEventType EventType{ get { return _type; } }
        public string UserID{
            get{
                return _userID;
            } 
        }

        public IMConnectEvent(ErrorCode code,ConnectEventType type,string userID){
            _code = code;
            _type = type;
            _userID = userID;
        }

    }
}
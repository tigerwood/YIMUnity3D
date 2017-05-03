
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

    public class ConnectEvent{
        private ErrorCode _code;
        private string _userID;
        public string UserID{
            get{
                return _userID;
            } 
        }
        public ErrorCode Code { 
            get{
                return _code;
            } 
        }
         public ConnectEvent(ErrorCode code,string userID){
            _code = code;
            _userID = userID;
        }
    }

    public class LoginEvent:ConnectEvent{
        public LoginEvent(ErrorCode code,string userID):base(code,userID){
        }
    }

    public class LogoutEvent:ConnectEvent{
        public LogoutEvent(ErrorCode code,string userID):base(code,userID){
        }
    }

    public class KickOffEvent:ConnectEvent{
        public KickOffEvent(ErrorCode code,string userID):base(code,userID){
        }
    }
    
    public class DisconnectEvent:ConnectEvent{
        public DisconnectEvent(ErrorCode code,string userID):base(code,userID){
        }
    }
}
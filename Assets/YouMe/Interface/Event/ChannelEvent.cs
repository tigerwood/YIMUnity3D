namespace YouMe
{
    public enum ChannelEventType{
        JOIN_SUCCESS,
        LEAVE_SUCCESS,    
        JOIN_FAIL,
        LEAVE_FAIL,
    }
    public class ChannelEvent{

        ErrorCode _code;
        ChannelEventType _eventType;
        string _channelID;

        public ErrorCode code { get{
                return _code;
            } }
        public ChannelEventType EventType{ get{
                return _eventType;
            } }
        public string ChannelID{ get{
                return _channelID;
            } }

        public ChannelEvent(ErrorCode code,ChannelEventType eType,string channelID){
            _code = code;
            _eventType = eType;
            _channelID = channelID;
        }
    }
}
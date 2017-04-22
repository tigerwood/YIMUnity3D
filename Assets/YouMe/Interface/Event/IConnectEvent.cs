namespace YouMe
{
    public enum ConnectEventType{
        CONNECTED,
        DISCONNECTED,
        CONNECT_FAIL,
        KICKED,
        OFF_LINE //掉线
    }
    public interface IConnectEvent{
        ErrorCode Code { get; }
        ConnectEventType EventType{ get; }
        string UserID{ get; }
    }
}
namespace YouMe
{
    public enum ServerZone
    {
        China = 0,      // 中国
        Singapore = 1,  // 新加坡
        America = 2,        // 美国
        HongKong = 3,   // 香港
        Korea = 4,      // 韩国
        Australia = 5,  // 澳洲
        Deutschland = 6,    // 德国
        Brazil = 7,     // 巴西
        India = 8,      // 印度
        Japan = 9,      // 日本
        Ireland = 10,   // 爱尔兰
        ServerZone_Unknow = 9999
    };

    public enum LogLevel
    {
        NONE = 0,
        FATAL  = 1,
        ERROR = 10,
        WARNING = 20,
        INFO = 40,
        ALL = 50
    };

    public enum ConnectStatus
    {
        DISCONNECTED = 0,
        CONNECTED  = 1,
        CONNECTING = 2,
        RECONNECTING = 3
    };

    public enum ErrorCode{
        SUCCESS,
        NET_ERROR,
        SECRETKEY_ERROR,
        STATUS_ERROR,

        START_DOWNLOAD_FAIL
    }

    public enum MessageType
    {
        TEXT,
        AUDIO
    }
}
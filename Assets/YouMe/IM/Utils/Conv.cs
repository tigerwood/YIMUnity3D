using YIMEngine;

namespace YouMe{
    public static class Conv{
        public static ErrorCode ErrorCodeConvert(YIMEngine.ErrorCode errorcode){
            return (ErrorCode)errorcode;
        }
    }
}
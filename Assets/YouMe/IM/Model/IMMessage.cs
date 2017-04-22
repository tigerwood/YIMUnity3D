using System;
using YouMe;

public class IMMessage{
    public MessageType messageType;
}

public enum MessageDownloadStatus{
    NOTDOWNLOAD, DOWNLOADING, DOWNLOADED
}

public enum ChatType{
    Unknow = 0,
    PrivateChat = 1,
    RoomChat = 2,
}

public class AudioMessage:IMMessage{

    public string reciverID;
    public ChatType chatType;

    public MessageDownloadStatus status;
    public bool isRecorgnizeText;
    public string filePath;
    public string recognizedText;
    public string extraParam;

    public AudioMessage(string reciverID,ChatType chatType,string extraParam,bool isRecorgnizeText ){
        this.reciverID = reciverID;
        this.chatType = chatType;
        this.extraParam = extraParam;
        this.isRecorgnizeText = isRecorgnizeText;
    }

    public void PlayAudio(){}
    public void PlayAudioInQueue(){}

    public void Download(Action<ErrorCode,AudioMessage> msg){

    }
    public void Download(string targetPath, Action<ErrorCode,AudioMessage> msg){
        
    }

}

public class TextMessage:IMMessage{
    public string content;

    public TextMessage(string reciver,ChatType chatType,string content){
        this.content = content;
    }
}
using System;
using YouMe;

public class IMMessage{
    public MessageType messageType;
    public string reciverID;
    public ChatType chatType;
    public bool isSelfSend;
    public SendStatus sendStatus;
}

public enum MessageDownloadStatus{
    NOTDOWNLOAD, DOWNLOADING, DOWNLOADED
}

public enum ChatType{
    Unknow = 0,
    PrivateChat = 1,
    RoomChat = 2,
}

public enum SendStatus{
    NotStartSend = 0,
    Sending = 1,
    Sended = 2,
    Fail = 3,
}

public class AudioMessage:IMMessage{

    public MessageDownloadStatus downloadStatus;
    public bool isRecorgnizeText;
    public string filePath;
    public string recognizedText;
    public string extraParam;

    public AudioMessage(string reciverID,ChatType chatType,string extraParam,bool isRecorgnizeText,bool isSelfSend){
        this.messageType = MessageType.AUDIO;
        this.reciverID = reciverID;
        this.chatType = chatType;
        this.extraParam = extraParam;
        this.isRecorgnizeText = isRecorgnizeText;
        this.isSelfSend = isSelfSend;
        if(isSelfSend){
            sendStatus = SendStatus.NotStartSend;
        }else{
            sendStatus = SendStatus.Sended;
        }
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

    public TextMessage(string reciver,ChatType chatType,string content,bool isSelfSend){
         this.messageType = MessageType.TEXT;
        this.reciverID = reciver;
        this.chatType = chatType;
        this.content = content;
        this.isSelfSend = isSelfSend;
        if(isSelfSend){
            sendStatus = SendStatus.NotStartSend;
        }else{
            sendStatus = SendStatus.Sended;
        }
    }
}
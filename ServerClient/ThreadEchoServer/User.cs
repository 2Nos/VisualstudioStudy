using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

class User
{
    public Socket userSock;
    public byte[] sendBuffer;
    public byte[] receiveBuffer;

    public User()  //생성자
    {
        sendBuffer = new byte[128];
        receiveBuffer = new byte[128];
    }
    //새로운 사용자가 늘어날때마다 소켓이 할당이되고 버퍼가 초기화되어야하기에
    public User(Socket _sock)  //생성자
    {
        userSock = _sock;
        sendBuffer = new byte[128];
        receiveBuffer = new byte[128];
    }
    //User클래스에서는 어떠한 기능이 필요
    //버퍼를 초기화
    public void ClearSendBuffer()
    {
        Array.Clear(sendBuffer, 0, sendBuffer.Length);
    }
    public void ClearReceiveBuffer()
    {
        Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
    }
}


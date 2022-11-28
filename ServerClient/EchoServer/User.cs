using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
class User
{
    public Socket userSock;
    public byte[] sendBuffer;
    public byte[] receiveBuffer;
    public User()
    {
        sendBuffer = new byte[128];
        receiveBuffer = new byte[128];
    }
    public User(Socket _sock)
    {
        userSock = _sock;
        sendBuffer = new byte[128];
        receiveBuffer = new byte[128];
    }
    // User클래스에서는 어떠한 기능이 필요
    // 버퍼를 초기화
    public void ClearSendBuffer()
    {
        Array.Clear(sendBuffer, 0, sendBuffer.Length);
    }
    public void ClearReceiveBuffer()
    {
        Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
    }
}


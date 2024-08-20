using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public delegate void ServerEventHandler(Socket client);

    public partial class ServerForm : Form
    {
        private const string localhost = "127.0.0.1";
        private const int BACKLOG = 5;
        private readonly List<Socket> ClientList = new List<Socket>();

        private Socket Server = null;
        private Socket Client = null;

        private Thread ListenThread = null;     // 서버 개방 쓰레드
        private bool ListenThreadRun = false;   // 개방 쓰레드 동작 여부 변수
        private Thread ReceiveThread = null;    // 클라이언트 연결 쓰레드
        private bool ReceiveThreadRun = false;  // 연결 쓰레드 동작 여부 변수
        private NetInfo netInfo;                // IP, PORT 객체

        public int BufferSize = 1024;
        public string recvData = "";            // 수신 메시지 저장 변수
        public bool isOpend = false;

        public event ServerEventHandler ServerEvent;

        public ServerForm()
        {
            InitializeComponent();
        }

        public void Open()
        {
            try
            {
                if (Server != null)  // 이미 Open 했을때
                {
                    Server.Disconnect(false);
                    WriteRichTextbox("Server Already Open");
                    return;
                }

                Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress addr = IPAddress.Parse(localhost);
                IPEndPoint ipep = new IPEndPoint(addr, netInfo.Port);
                Server.Bind(ipep);
                Server.Listen(BACKLOG);
                isOpend = true;

                WriteRichTextbox("Server Open");
            }
            catch (SocketException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Listen()
        {
            while (true)
            {
                try
                {
                    if (Server != null)
                        Client = Server.Accept();

                    if (Client == null) continue;
                    if (!Client.Connected) continue;

                    ClearTextbox();

                    ClientList.Add(Client);
                    //this.Text = "Server CountClient : " + ClientList.Count;

                    if (ReceiveThread == null)
                    {
                        ReceiveThread = new Thread(Receive);
                        ReceiveThreadRun = true;
                        ReceiveThread.Start(Client);
                        // ReceiveThread.Start();
                    }
                    else
                    {
                        ReceiveThread.Abort();
                        ReceiveThread = null;
                        ReceiveThread = new Thread(Receive);
                        ReceiveThreadRun = true;
                        ReceiveThread.Start(Client);
                        // ReceiveThread.Start();
                    }
                }
                catch (NullReferenceException e)
                {
                    if (Server == null)
                        MessageBox.Show("Server is null", e.Message);
                    else if (Client == null)
                        MessageBox.Show("Client is null", e.Message);
                    break;
                }
                catch (ThreadAbortException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                if (Client != null)
                    MessageBox.Show("Connected");
                // break;
            }
        }

        public void Receive(object currentClient)
        {
            Socket client = (Socket)currentClient;
            byte[] recvBuf = new byte[BufferSize];
            int recvSize;

            while (true)
            {
                Thread.Sleep(1);

                try
                {
                    if (ClientList != null)
                    {
                        for (int i = 0; i < ClientList.Count; i++)
                        {
                            if (ClientList[i] != null)
                            {
                                if (ClientList[i].Connected)
                                    ClientList[i].NoDelay = true;
                                recvSize = ClientList[i].Receive(recvBuf);
                            }
                        }
                    }
                    else if (ClientList == null)
                    {
                        WriteRichTextbox("Server Closed");
                        break;
                    }

                    recvData = Encoding.Default.GetString(recvBuf, 0, recvBuf.Length);
                    WriteRichTextbox("Client : " + recvData);

                }
                catch (SocketException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (ThreadAbortException e)
                {
                    if (!ReceiveThreadRun)
                        MessageBox.Show("Server Closed", "Server");
                    else
                        MessageBox.Show(e.Message);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            if (ClientList != null)
            {
                ClientList.Remove(client);
                client.Close();
                client = null;
            }
        }

        public void WriteRichTextbox(string data)
        {
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText(data + "\r\n"); });
        }

        public void ClearTextbox()
        {
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.Text = ""; });
        }

        public void Send(string SendMsg)
        {
            try
            {
                if (ClientList != null)
                {
                    for (int i = 0; i < ClientList.Count; i++)
                    {
                        if (ClientList[i] != null)
                        {
                            if (ClientList[i].Connected)
                                ClientList[i].NoDelay = true;
                            ClientList[i].Send(Encoding.Default.GetBytes(SendMsg));
                        }
                    }
                }
            }
            catch (SocketException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Stop()
        {
            try
            {
                if (ListenThread != null)
                {
                    if (ListenThread.IsAlive)
                    {
                        ListenThreadRun = false;
                        if (!ListenThread.Join(500))
                        {
                            if (ListenThread.IsAlive)
                            {
                                ListenThread.Interrupt();
                                //ListenThread.Abort();
                            }
                        }
                    }
                    ListenThread = null;
                }
                if (Server != null)
                {
                    Server.Close();
                    Server = null;
                    WriteRichTextbox("Server Closed");
                    MessageBox.Show("Server Closed", "Server");
                }
                if (ClientList != null)
                {
                    foreach (Socket sock in ClientList)
                        if (sock != null) sock.Close();
                }
                isOpend = false;
                ReceiveThreadRun = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            InputNetInfoForm inputNet = new InputNetInfoForm();
            if (inputNet.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                netInfo = inputNet.ReturnNetInfo;

                ClearTextbox();

                try
                {
                    if (!isOpend)
                    {
                        Open();

                        ListenThread = new Thread(Listen);
                        ListenThread.IsBackground = true;
                        ListenThreadRun = true;
                        ListenThread.Start();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Send(txtInputMessage.Text);
            txtInputMessage.Clear();
        }
        private void txtInputMessageKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSend_Click(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ClearTextbox();
            if (Server == null)
            {
                WriteRichTextbox("Server is Not Open");
            }
            else
            {
                Stop();
                this.Text = "Server";
            }
        }
    }
}

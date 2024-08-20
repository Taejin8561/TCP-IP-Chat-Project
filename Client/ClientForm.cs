using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        private const string localhost = "127.0.0.1";

        private Socket Client = null;
        private Thread ClientThread = null;     // 클라이언트 연결쓰레드
        private bool ClientThreadRun = false;   // 쓰레드 동작 여부 변수
        private NetInfo netInfo;                 // IP, PORT 객체

        public int BufferSize = 1024;
        public string recvData = "";            // 수신 메시지 저장 변수
        public bool isConnected = false;
        public ClientForm()
        {
            InitializeComponent();
        }

        public void Connect()
        {
            try
            {
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (netInfo.Address == "localhost")
                {
                    netInfo.Address = localhost;
                }
                IPAddress addr = IPAddress.Parse(netInfo.Address);
                IPEndPoint ipep = new IPEndPoint(addr, netInfo.Port);
                Client.Connect(ipep);

                isConnected = true;
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

        public void Receive()
        {
            try
            {
                while (ClientThreadRun)
                {
                    byte[] recvBuf = new byte[BufferSize];
                    int recvSize = Client.Receive(recvBuf);

                    if (Client != null && Client.Connected)
                    {
                        recvData = Encoding.Default.GetString(recvBuf, 0, recvSize);
                        WriteRichTextbox("Server : " + recvData);
                    }
                    else
                    {
                        WriteRichTextbox("Disconnected");
                        break;
                    }
                }
            }
            catch (SocketException e)
            {
                if (!Client.Connected)
                    WriteRichTextbox("Disconnected");
                else
                    MessageBox.Show(e.Message);
            }
            catch (ThreadAbortException e)
            {
                if (!ClientThreadRun)
                    MessageBox.Show("Client Disconnect", "Client");
                else
                    MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
                if (Client != null && Client.Connected)
                    Client.Send(Encoding.Default.GetBytes(SendMsg));
                else
                    WriteRichTextbox("Start Server First");
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
            if (ClientThread != null)
            {
                if (ClientThread.IsAlive)
                {
                    ClientThreadRun = false;
                    if (!ClientThread.Join(500))
                    {
                        if (ClientThread.IsAlive)
                        {
                            ClientThread.Abort();
                        }
                    }
                }
                ClientThread = null;
            }

            if (Client != null)
            {
                if (Client.Connected)
                {
                    Client.Disconnect(false);
                    Client.Close();
                    Client = null;
                }
            }
            WriteRichTextbox("Disconnected");
            isConnected = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
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
                    if (!isConnected) Connect();
                    this.Text = "Client " + Client.RemoteEndPoint;

                    ClientThread = new Thread(Receive);
                    ClientThread.IsBackground = true;
                    ClientThreadRun = true;
                    ClientThread.Start();
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

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            ClearTextbox();
            if (Client == null)
            {
                WriteRichTextbox("Not Connected");
            }
            else
            {
                Stop();
                this.Text = "Client";
            }
        }
    }
}

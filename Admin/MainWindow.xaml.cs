using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Forms;

namespace Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        IAsyncResult cbResult;
        string localPath = "../../../Local/";
        string uploadfilename;
        //string serverUrl;
        public MainWindow()
        {
            InitializeComponent();
            disablebuttonsExceptLogin();
        }

        private void disableButtons()
        {
            button_update.IsEnabled = false;
            button_delete.IsEnabled = false;
            button_download.IsEnabled = false;
            button_browse.IsEnabled = false;
            button_upload.IsEnabled = false;
            button_login.IsEnabled = false;
            button_logoff.IsEnabled = false;
        }
        private void enableButtons()
        {
            button_update.IsEnabled = true;
            button_delete.IsEnabled = true;
            button_download.IsEnabled = true;
            button_browse.IsEnabled = true;
            button_upload.IsEnabled = true;
            button_login.IsEnabled = true;
            button_logoff.IsEnabled = true;
        }
        private void disablebuttonsExceptLogin()
        {
            button_update.IsEnabled = false;
            button_delete.IsEnabled = false;
            button_download.IsEnabled = false;
            button_browse.IsEnabled = false;
            button_upload.IsEnabled = false;
            button_login.IsEnabled = true;
            button_logoff.IsEnabled = false;
        }
        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();
            string ServerUrl = textbox_serverurl.Text;
            string UserName = textbox_username.Text;
            IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(this.passwordbox_password.SecurePassword);
            string Password = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);
            if (UserName == "" || Password == "")
            {
                System.Windows.MessageBox.Show("Username and Password can not be empty");
                disablebuttonsExceptLogin();
            }
            else
            {
                Action<string, string, string> action = this.Login;
                cbResult = action.BeginInvoke(ServerUrl, UserName, Password, null, null);
                enableButtons();
            }
        }
        private TestClient handle;
        
        //change delegate to bool
        void Login(string serverurl, string username, string password)
        {
            handle = new TestClient(serverurl);
            bool success = handle.login(username, password);
            string temp;
            if (success)
            {
                temp = "success";
            }
            else
                temp = "failed";
            //call back
            Dispatcher.Invoke(new Action<string>(this.replyLogin),
            System.Windows.Threading.DispatcherPriority.Background,
            new string[] { temp });
            //enableButtons();
        }
        void replyLogin(string success)
        {
            if (success == "success")
            {
                enableButtons();
                Getfiles();
                System.Windows.MessageBox.Show("You have loged in!");
            }
            else
            {
                System.Windows.MessageBox.Show("Wrong user name or password!");
            }
        }

        //////////////////////////////////////////////////////////////////
        //get files
        private void Getfiles()
        {
            string[] files = null;
            listbox_serverfiles.Items.Clear();
            for (int i = 0; i < 10; ++i)
            {
                Thread.Sleep(100);
                try
                {
                    files = handle.getAvailableFiles();
                    break;
                }
                catch
                {
                    continue;
                }
            }
            foreach (string file in files)
                listbox_serverfiles.Items.Add(file);
        }
        private void button_logoff_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();
            handle = null;
            listbox_serverfiles.Items.Clear();
            disablebuttonsExceptLogin();
        }

        //may has problem of thread
        private void button_update_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();
            Dispatcher.Invoke(new Action<string, string>(ReplyService),
                       System.Windows.Threading.DispatcherPriority.Background,
                       new string[] {"success", "Update" }); 
            enableButtons();
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();
            if (listbox_serverfiles.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Select a file to delete!");
            }
            else
            {
                string filename = System.IO.Path.GetFileName(listbox_serverfiles.SelectedItem.ToString());
                Action<string> action = this.DeleteFile;
                cbResult = action.BeginInvoke(filename, null, null);
            }
            enableButtons();
        }
        public void DeleteFile(string filename)
        {
            bool deletefile = handle.deleteFile(filename);
            string result;
            if (deletefile)
                result = "success";
            else
                result = "failed";
            //call back
            Dispatcher.Invoke(new Action<string, string>(ReplyService),
            System.Windows.Threading.DispatcherPriority.Background,
            new string[] { result, "Detele" });
        }
        private void ReplyService(string result, string service)
        {
            if (result == "success")
            {
                System.Windows.MessageBox.Show(service + " success!");
                Getfiles();
            }
            else
            {
                System.Windows.MessageBox.Show(service + " failed!");
            }
        }
        private void button_download_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();
            if (listbox_serverfiles.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Select a file to download!");
            }
            else
            {
                string filename = localPath + listbox_serverfiles.SelectedItem.ToString();
                Action<string> action = this.Download;
                cbResult = action.BeginInvoke(filename, null, null);
            }
            enableButtons();
        }
        void Download(string filename)
        {
            string result = handle.downLoadFile(filename);
            //call back
            Dispatcher.Invoke(new Action<string, string>(ReplyService),
            System.Windows.Threading.DispatcherPriority.Background,
            new string[] { result, "Download" });   
        }
        private void button_browse_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();
            textbox_filepathname.Text = null;
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                uploadfilename = dlg.FileName;
            }
            textbox_filepathname.Text = uploadfilename;
            enableButtons();
        }

        private void button_upload_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();

            if (uploadfilename == null)
            {
                System.Windows.Forms.MessageBox.Show("Choose a file");
            }
            else
            {
                Action<string> action = this.Upload;
                cbResult = action.BeginInvoke(uploadfilename, null, null);
            }
            textbox_filepathname.Text = null;
            uploadfilename = null;
            enableButtons();
        }
        void Upload(string filename)
        {
            string result = handle.upLoadFile(filename);
            //call back
            Dispatcher.Invoke(new Action<string, string>(ReplyService),
            System.Windows.Threading.DispatcherPriority.Background,
            new string[] { result, "Upload" });   
        }
       
    }
}

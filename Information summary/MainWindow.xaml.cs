using Microsoft.Win32;
using System;
using System.Collections;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using System.Windows.Threading;
using System.Diagnostics;

namespace Information_summary
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 存储添加的文件
        /// </summary>
        static ArrayList Number_file = new ArrayList();

        /// <summary>
        /// 一些无关紧要的事件过程
        /// </summary>
        #region

        private void 主窗体_DragEnter(object sender, DragEventArgs e)
        {
            拖放.Visibility = Visibility.Visible;
        }

        private void 主窗体_DragLeave(object sender, DragEventArgs e)
        {
            拖放.Visibility = Visibility.Collapsed;
        }

        private void 标题_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) { DragMove();}
        }

        private void 设置_Click(object sender, RoutedEventArgs e)
        {
            BeginStoryboard((Storyboard)FindResource("设置打开"));
        }

        private void 关闭算法_Click(object sender, RoutedEventArgs e)
        {
            BeginStoryboard((Storyboard)FindResource("设置关闭"));
        }

        private void 关闭窗体_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.前端显示 = (bool)前端显示.IsChecked;
            Properties.Settings.Default.文件名称 = (bool)名称.IsChecked;
            Properties.Settings.Default.文件大小 = (bool)大小.IsChecked;
            Properties.Settings.Default.创建日期 = (bool)创建.IsChecked;
            Properties.Settings.Default.修改日期 = (bool)修改.IsChecked;
            Properties.Settings.Default.版本信息 = (bool)版本.IsChecked;
            Properties.Settings.Default.文件类型 = (bool)类型.IsChecked;

            Properties.Settings.Default.MD5 = (bool)MD5.IsChecked;
            Properties.Settings.Default.CRC32 = (bool)CRC32.IsChecked;
            Properties.Settings.Default.RIPEMD160 = (bool)Ripemd.IsChecked;
            Properties.Settings.Default.SHA1 = (bool)SHA1.IsChecked;
            Properties.Settings.Default.SHA256 = (bool)SHA256.IsChecked;
            Properties.Settings.Default.SHA384 = (bool)SHA384.IsChecked;
            Properties.Settings.Default.SHA512 = (bool)SHA512.IsChecked;

            Properties.Settings.Default.Save();
            Environment.Exit(0);

        }

        private void 最小化_Click(object sender, RoutedEventArgs e)
        {
            主窗体.WindowState = WindowState.Minimized;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            主窗体.Topmost = (bool)前端显示.IsChecked;
        }

        private void 清除_Click(object sender, RoutedEventArgs e)
        {
            输出信息.Text = null;
            提示信息.Content = "清除成功!";
            BeginStoryboard((Storyboard)FindResource("提示打开"));
        }

        private void 复制_Click(object sender, RoutedEventArgs e)
        {
            if (输出信息.Text.Length > 5)
            {
                Clipboard.SetText(输出信息.Text);
                提示信息.Content = "复制成功!";
                BeginStoryboard((Storyboard)FindResource("提示打开"));
            }
            else
            {
                提示信息.Content = "木有信息!";
                BeginStoryboard((Storyboard)FindResource("提示打开"));
            }
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            //校验自身

            if (Checkx.Document_verification(Process.GetCurrentProcess().MainModule.FileName) != true)
            {
                MessageBox.Show("签名校验失败,程序可能被篡改,轻击确定以退出程序", "警告");
                Environment.Exit(0);
            }

            #region
            前端显示.IsChecked = Properties.Settings.Default.前端显示;
            名称.IsChecked = Properties.Settings.Default.文件名称;
            大小.IsChecked = Properties.Settings.Default.文件大小;
            创建.IsChecked = Properties.Settings.Default.创建日期;
            修改.IsChecked = Properties.Settings.Default.修改日期;
            版本.IsChecked = Properties.Settings.Default.版本信息;
            类型.IsChecked = Properties.Settings.Default.文件类型;

            MD5.IsChecked = Properties.Settings.Default.MD5;
            CRC32.IsChecked = Properties.Settings.Default.CRC32;
            Ripemd.IsChecked = Properties.Settings.Default.RIPEMD160;
            SHA1.IsChecked = Properties.Settings.Default.SHA1;
            SHA256.IsChecked = Properties.Settings.Default.SHA256;
            SHA384.IsChecked = Properties.Settings.Default.SHA384;
            SHA512.IsChecked = Properties.Settings.Default.SHA512;

            主窗体.Topmost = (bool)前端显示.IsChecked;
            #endregion
        }

        private void 添加_Click(object sender, RoutedEventArgs e)
        {
            Number_file.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog(this) == true)
            {
                foreach (string file in ofd.FileNames)
                {
                    Number_file.Add(file);
                }
            }
            calculate();
        }
        private void Assignment(string str)
        {
            Dispatcher.Invoke( new Action(delegate
            {
                输出信息.Text += str;
            }));
        }

        private void calculate()
        {
            bool[] st = new bool[14];
            #region
            st[0] = (bool)名称.IsChecked;
            st[1] = (bool)大小.IsChecked;
            st[2] = (bool)版本.IsChecked;
            st[3] = (bool)创建.IsChecked;
            st[4] = (bool)修改.IsChecked;
            st[5] = (bool)类型.IsChecked;
            st[6] = (bool)MD5.IsChecked;
            st[7] = (bool)CRC32.IsChecked;
            st[8] = (bool)SHA1.IsChecked;
            st[9] = (bool)SHA256.IsChecked;
            st[10] = (bool)SHA384.IsChecked;
            st[11] = (bool)SHA512.IsChecked;
            st[12] = (bool)Ripemd.IsChecked;
            #endregion
            bool whether = false;
            for (int i = 0; i < st.Length; i++)
            {
                if (st[i] == true)
                {
                    whether = true;
                }
            }
            if (whether == true)
            {
                加载条.IsIndeterminate = true;
                using (BackgroundWorker bw = new BackgroundWorker())
                {
                    bw.DoWork += new DoWorkEventHandler(bw_DoWork2);
                    bw.RunWorkerAsync(st);
                }
            }
            else
            {
                提示信息.Content = "请选择摘要项!";
                BeginStoryboard((Storyboard)FindResource("提示打开"));
            }
        }

        private void 保存_Click(object sender, RoutedEventArgs e)
        {
            if (输出信息.Text.Length > 5)
            {
                SaveFileDialog sf1 = new SaveFileDialog();
                sf1.Title = "保存导出文件";
                sf1.FileName = "信息摘要导出.txt";
                sf1.Filter = "txt文档|*.txt";
                if (sf1.ShowDialog(this) == true)
                {
                    if (File.Exists(sf1.FileName))
                    {
                        File.Delete(sf1.FileName);
                    }

                    StreamWriter sw = new StreamWriter(sf1.FileName, true, Encoding.UTF8);
                    sw.Write(输出信息.Text);
                    sw.Flush();
                    sw.Close();
                    提示信息.Content = "保存完成!";
                    BeginStoryboard((Storyboard)FindResource("提示打开"));
                }
            }
            else
            {
                提示信息.Content = "木有信息!";
                BeginStoryboard((Storyboard)FindResource("提示打开"));
            }

        }

        private void 拖放_Drop(object sender, DragEventArgs e)
        {
            拖放.Visibility = Visibility.Collapsed;
            Number_file.Clear();
            string[] filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            for (int i = 0; i < filePath.Length; i++)
            {
                if (File.Exists(filePath[i]))
                {
                    Number_file.Add(filePath[i]);
                }
            }
            calculate();
        }

        void bw_DoWork2(object sender, DoWorkEventArgs e)//后台
        {
            bool[] st = new bool[14];
            FileVersionInfo info;
            FileInfo fi;
            string str = "";
            for (int i = 0; i < Number_file.Count; i++)
            {
                if (((bool[])(e.Argument))[0])
                {
                    str  = "文件名称：       " + System.IO.Path.GetFileName(Number_file[i].ToString()) + "\n";
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[1])
                {
                    FileInfo fileInfo = null;
                    fileInfo = new FileInfo(Number_file[i].ToString());
                    str = "文件大小：       " + Math.Ceiling(fileInfo.Length / 1.0) + " 字节" + "\n";
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[2])
                {
                    info = FileVersionInfo.GetVersionInfo(Number_file[i].ToString());
                    if (info.FileVersion != null) { str = "文件版本：       " + info.FileVersion + "\n"; }
                    else { str = "文件版本：       " + "暂无信息.." + "\n"; }
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[3])
                {
                    fi = new FileInfo(Number_file[i].ToString());
                    str = "创建日期：       " + fi.CreationTime.ToString() + "\n";
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[4])
                {
                    fi = new FileInfo(Number_file[i].ToString());
                    str = "修改日期：       " + fi.LastWriteTime + "\n";
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[5])
                {
                    str = "文件类型：       " + System.IO.Path.GetExtension(Number_file[i].ToString()).Replace(".", "") + " 文件\n";
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[7])
                {
                    str = "CRC32值：      " + Digest_algorithm.CRC32.GetFileCRC32(Number_file[i].ToString()) + "\n";
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[6])
                {
                    str = "MD5值：         " + Digest_algorithm.MD5.GetMD5HashFromFile(Number_file[i].ToString()).ToUpper() + "\n";
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[12])
                {
                    str = "RIPEMD-160：" + Digest_algorithm.RIPEMD160.Hash_HMACRIPEMD160(Number_file[i].ToString()).ToUpper() + "\n";
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[8])
                {
                    str= "SHA1值：        " + Digest_algorithm.SHA1.Hash_SHA_1(Number_file[i].ToString()) + "\n";
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[9])
                {
                    str = "SHA256值：    " + Digest_algorithm.SHA256.Hash_SHA_256(Number_file[i].ToString()) + "\n";
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[10])
                {
                    str = "SHA384值：    " + Digest_algorithm.SHA384.Hash_SHA_384(Number_file[i].ToString()) + "\n";
                    Assignment(str);
                }

                if (((bool[])(e.Argument))[11])
                {
                    str = "SHA512值：    " + Digest_algorithm.SHA512.Hash_SHA_512(Number_file[i].ToString()) + "\n";
                    Assignment(str);
                }

                Dispatcher.Invoke(new Action(delegate
                {
                    输出信息.Text += "\n";
                }));
            }

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                加载条.IsIndeterminate = false;
            }));

            Digest_algorithm.Optimization.FlushMemory();
        }

        private void 版权_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/xingchuanzhen/Minecraft_Bypass_the_program");
        }

        private void 关于_Click(object sender, RoutedEventArgs e)
        {
            BeginStoryboard((Storyboard)FindResource("关于打开"));
        }

        private void 关闭关于_Click(object sender, RoutedEventArgs e)
        {
            BeginStoryboard((Storyboard)FindResource("关于关闭"));
        }

        private void 图标_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/xingchuanzhen/Information-summary");
        }
    }
}

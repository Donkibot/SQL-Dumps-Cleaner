using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        DirectoryInfo d;
        FileInfo[] Files;

        long[] fileSize;
        long overalSize = 0;
        string[] FileArray;
        string FilePath;

        public Form1()
        {
            InitializeComponent();
            FilePath = @"D:\Programs\Microsoft SQL\MSSQL15.MSSQLSERVER\MSSQL\Log\Polybase\dump";
            UpdateFileArray();
            
    }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateFileArray();
            ClearTextBox();
            WriteInTextBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteAllFiles();
            UpdateFileArray();
            ClearTextBox();
            WriteInTextBox();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", FilePath);
        }

        void ClearTextBox()
        {
            if (richTextBox1.Text != "")
            {
               richTextBox1.Clear();
            }
        }

        void WriteInTextBox()
        {
            foreach (string str in FileArray)
            {
                richTextBox1.Text += str + "\n";
            }
            string overalSizeInGb = string.Format("\nOveral size = {0:0.00} Gb\r\n", getOveralSize() / 1000.0);
            richTextBox1.Text += overalSizeInGb;
        }

        long getOveralSize()
        {
            overalSize = 0;
            foreach (long l in fileSize)
            {
                overalSize += l;
            }
            return overalSize;
        }


        void DeleteAllFiles()
        {
            foreach (FileInfo file in Files)
            {
                file.Delete();
            }
        }

        void UpdateFileArray()
        {
            DirectoryInfo d = new DirectoryInfo(FilePath);
            Files = d.GetFiles();
            FileArray = new string[Files.Length];
            fileSize = new long[Files.Length];

            int count = 0;
            foreach (FileInfo file in Files)
            {
                fileSize[count] = file.Length / (1024 * 1024);
                string temp = $"{file.Name} - {fileSize[count]}mb";
                FileArray[count] = temp;
                count++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                string path = folderBrowserDialog1.SelectedPath;
                folderTextBox.Text = path;
                FilePath = path;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            folderTextBox.Text = FilePath;
        }
    }

}

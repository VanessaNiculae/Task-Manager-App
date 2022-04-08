using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IntPtr hSnapshot = WinApiClass.CreateToolhelp32Snapshot(0x00000002, 0);

            WinApiClass.PROCESSENTRY32 entry = new WinApiClass.PROCESSENTRY32();

            entry.dwSize = (uint)Marshal.SizeOf(entry);

            WinApiClass.Process32First(hSnapshot, ref entry);

            dataGridView1.Rows.Add(entry.th32ProcessID, entry.szExeFile, entry.cntThreads);

            while (WinApiClass.Process32Next(hSnapshot, ref entry))
            {
                dataGridView1.Rows.Add(entry.th32ProcessID, entry.szExeFile, entry.cntThreads);
            }

            WinApiClass.GetLastError();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);
            IntPtr Id = WinApiClass.OpenProcess(0x0001, false, id);
            WinApiClass.TerminateProcess(Id, 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMLGUI.EventsLib;

namespace XMLGUI.Forms
{
    public partial class FilterProperties : Form
    {
        public event EventHandler<FilterChangeEventArgs> FilterChangeEvent;
        public FilterProperties()
        {
            InitializeComponent();
            comboBox.Items.Add("Клиент");
            comboBox.Items.Add("Тренер");
            comboBox.Items.Add("Вид занятия");
            comboBox.Items.Add("Дата начала");
            comboBox.SelectedItem = "Клиент";
        }

        private void OnBtnApplyClick(object sender, EventArgs e)
        {
            EventHandler<FilterChangeEventArgs> handler = FilterChangeEvent;
            handler?.Invoke(this, new FilterChangeEventArgs(this.paramTxtBox.Text, this.comboBox.SelectedItem.ToString()));
            //handler?.Invoke(this, new FilterChangeEventArgs());
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using XMLGUI.Forms;
using XMLGUI.EventsLib;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;

namespace XMLGUI
{
    public partial class XMLViewer : Form
    {
        XDocument doc;
        DataTable dt;
        string FileName;
        public XMLViewer()
        {
            InitializeComponent();
        }

        private void OnSetFilterClick(object sender, EventArgs e)
        {
            FilterProperties setFilterForm = new FilterProperties();
            setFilterForm.FilterChangeEvent += new EventHandler<FilterChangeEventArgs>(this.OnFilterChangeEvent);
            setFilterForm.Show();
        }

        public void OnFilterChangeEvent(object sender, FilterChangeEventArgs e)
        {
            //update this form, using information from e.Param
            ///for example:
            //tableView.Text += e.Param;
            switch (e.Enum)
            {
                case "Клиент":
                    bindingSource.Filter = "[last_name_user] LIKE'" + e.Param + "%'";
                    break;
                case "Тренер":
                    bindingSource.Filter = "[last_name_coach] LIKE'" + e.Param + "%'";
                    break;
                case "Вид занятия":
                    bindingSource.Filter = "[type_name] LIKE'" + e.Param + "%'";
                    break;
                case "Дата начала":
                    bindingSource.Filter = "[start_date] LIKE'" + e.Param + "%'";
                    break;
                default:
                    bindingSource.RemoveFilter();
                    break;
            }
            Console.WriteLine("{0}:{1}", e.Param,e.Enum);
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Title = ("Открыть...");
            OPF.Filter = "XML Document (*.xml) | *.xml| All Files (*.*)|*.*";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(OPF.FileName);
                FileName = OPF.FileName;
                doc = XDocument.Load(FileName);
                
                dt = new DataTable();
                //Add Columns:
                dt.Columns.Add("id", typeof(int));//The default Type is "int".
                dt.Columns.Add("last_name_user");
                dt.Columns.Add("type_id", typeof(int));
                dt.Columns.Add("type_name");
                dt.Columns.Add("last_name_coach");
                dt.Columns.Add("start_date", typeof(DateTime));
                dt.Columns.Add("num_minutes", typeof(int));
                dt.Columns.Add("Rate", typeof(int));
                
                foreach (XElement el in doc.Root.Elements())
                {
                    //Add Rows:
                    DataRow row = dt.NewRow();
                    foreach (XAttribute attr in el.Attributes())
                        row[attr.Name.ToString()] = attr.Value;
                    foreach (XElement element in el.Elements())
                        row[element.Name.ToString()] = element.Value;
                    dt.Rows.Add(row);
                }

                bindingSource.DataSource = dt;

                bindingNavigator.BindingSource = bindingSource;
                dataGridView.DataSource = bindingSource;
                //dataGridView.DataSource = q.ToList();

                bindingNavigatorDeleteItem.Enabled = true;
                bindingNavigatorAddNewItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                saveFileToolStripMenuItem.Enabled = true;
                filterToolStripMenuItem.Enabled = true;

                dataGridView.Columns[0].ReadOnly = true;
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            int maxId = doc.Root.Elements("user_account").Max(t => Int32.Parse(t.Attribute("id").Value));
            XElement user_account = new XElement("user_account",
                new XAttribute("id", ++maxId),
                new XElement("last_name_user", "Ульянова"),
                new XAttribute("type_id", "1"),
                new XElement("type_name", "Фитнес"),
                new XElement("last_name_coach", "Петрова"),
                new XElement("start_date", "20.07.2020 21:00:00"),
                new XElement("num_minutes", "60"),
                new XElement("rate", "10"));
            doc.Root.Add(user_account);
            DataRow row = dt.NewRow();
                row["id"] = user_account.Attribute("id").Value;
                row["last_name_user"] = user_account.Element("last_name_user").Value;
                row["type_id"] = user_account.Attribute("type_id").Value;
                row["type_name"] = user_account.Element("type_name").Value;
                row["last_name_coach"] = user_account.Element("last_name_coach").Value;
                row["start_date"] = user_account.Element("start_date").Value;
                row["num_minutes"] = user_account.Element("num_minutes").Value;
                row["rate"] = user_account.Element("rate").Value;
            dt.Rows.Add(row);
            //bindingSource.AddNew();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView.CurrentRow.Index+1;
            Console.WriteLine(rowIndex);
            IEnumerable<XElement> user_account = doc.Root.Descendants("user_account").Where(
                t => t.Attribute("id").Value == rowIndex.ToString());
            user_account.Remove();
            bindingSource.RemoveCurrent();
        }

        private void dataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            int maxId = doc.Root.Elements("user_account").Max(t => Int32.Parse(t.Attribute("id").Value));
            XElement user_account = new XElement("user_account",
                new XAttribute("id", ++maxId),
                new XElement("last_name_user", ""),
                new XAttribute("type_id", "0"),
                new XElement("type_name", ""),
                new XElement("last_name_coach", ""),
                new XElement("start_date", "01.01.01"),
                new XElement("num_minutes", "0"),
                new XElement("rate", "0"));
            doc.Root.Add(user_account);
            dataGridView.Rows[e.Row.Index-1].Cells[0].Value = maxId;
            Console.WriteLine("User add row({0})!", e.Row.Index);

        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            IEnumerable<XElement> user_account = doc.Root.Descendants("user_account").Where(
                t => t.Attribute("id").Value == e.RowIndex.ToString());
            foreach (XElement el in doc.Root.Elements())
            {
                if (el.Attribute("id").Value == dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())
                {
                    foreach (XAttribute attr in el.Attributes())
                        if (attr.Name == dataGridView.Columns[e.ColumnIndex].HeaderCell.Value.ToString())
                            attr.SetValue(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    foreach (XElement element in el.Elements())
                        if (element.Name == dataGridView.Columns[e.ColumnIndex].HeaderCell.Value.ToString())
                            element.SetValue(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                }
                }
            Console.WriteLine("CellValueChanged({0})! :{1}:{2}", e.RowIndex, dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, dataGridView.Columns[e.ColumnIndex].HeaderCell.Value.ToString());
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
        }

        private void dataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doc.Save(FileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Title = ("Сохранить как...");
            SFD.Filter = "XML Document (*.xml) | *.xml| All Files (*.*)|*.*";
            SFD.OverwritePrompt = true;
            if (SFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                doc.Save(SFD.FileName);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bindingSource.RemoveFilter();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

using System.IO;
using MessageBox = System.Windows.Forms.MessageBox;

namespace memo_v1
{
    public partial class Form1 : Form
    {
        int iTabIndex = 0;
        int i = 1;
        public Form1()
        {
            InitializeComponent();
        }

        String savePath = string.Empty;
        private void btn_TabAdd_Click(object sender, EventArgs e)
        {
            i++;
            string a = "Tabpage" + i;
            //this.tabControl1.TabPages.Add(a);
            RichTextBox rtb = new RichTextBox();
            rtb.Size = new Size(679, 354);
            rtb.Location = new Point(0, 0);
            rtb.Multiline = true;
            rtb.Font = new Font("新細明體", 16);
            rtb.Text += System.DateTime.Now.ToString("yyyy - MM - dd , HH:mm") + "\r\n";
            rtb.ScrollBars = RichTextBoxScrollBars.Both;

            Panel p = new Panel();
            p.Location = new Point(0, 0);
            p.Size = new Size(687, 380);
            p.BackColor = Color.White;
            TabPage tabPage = new TabPage();
            tabPage.Text = a;
            this.tabControl1.TabPages.Add(tabPage);
            tabPage.Controls.Add(rtb);
            this.tabControl1.SelectedTab = tabPage;
        }

        private void btn_TabDelete_Click(object sender, EventArgs e)
        {
            int a = this.tabControl1.SelectedIndex;
            int i = this.tabControl1.TabCount - 1;
            if (i < 1)
            {
                //MessageBox.Show("已經無法再刪除！");
            }
            else
            {
                //string a = "Tabpage" + i;
                this.tabControl1.TabPages.RemoveAt(a);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            iTabIndex = tabControl1.SelectedIndex;
        }
        //private void textBox_SelectAll(object sender, KeyEventArgs e)
        //{
        //    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
        //    {
        //        ((TextBox)sender).SelectAll();
        //        e.SuppressKeyPress = true;
        //    }
        //}

        private void lb_timer_Tick(object sender, EventArgs e)
        {
            lb_time.Text = (System.DateTime.Now.ToString("yyyy - MM - dd ,  HH:mm:ss")).ToString();
        }

        private void 儲存SToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (savePath == "")
                {
                    另存新檔toolStripButton_Click(sender, e);
                }
                else
                {
                        string tab = "";
                        string tabtext = "";
                        //string xmltab = "";
                        string xmltabtext = "";
                        RichTextBox rtbtext = new RichTextBox();
                        for (int itab = 0; itab < tabControl1.TabCount; itab++)
                        {
                            String a = "richTextBox" + itab;
                            tab = tabControl1.TabPages[itab].Text;
                            rtbtext = (RichTextBox)tabControl1.TabPages[itab].Controls[0];
                            tabtext = rtbtext.Text;
                            xmltabtext += tab + "|" + tabtext + ";";
                        }
                        //MessageBox.Show(tab);
                        //MessageBox.Show(tabtext);
                        //MessageBox.Show(xmltabtext);

                        StreamWriter sw = new StreamWriter(@savePath);
                        sw.Write(xmltabtext);
                        sw.Close();
                        tS_Status_lb.Text = "儲存成功！";
                    }
            }
            catch
            {
                Exception ex = new Exception();
                tS_Status_lb.Text = "儲存失敗！";
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //richTextBox1.Text = System.DateTime.Now.ToString("yyyy - MM - dd , HH:mm");
            richTextBox1.Text = System.DateTime.Now.ToString("yyyy - MM - dd , HH:mm") + "\r\n";
        }

        private void 開啟OToolStripButton_Click(object sender, EventArgs e)
        {

            openFileDialog1.AddExtension = true;
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                String line = "";
                String line_all = "";
                if (File.Exists(openFileDialog1.FileName))
                {
                    //讀取txt code
                }
                while ((line = sr.ReadToEnd()) != null)
                {
                    if (line != "")
                    {
                        Console.WriteLine(line.ToString());
                        //MessageBox.Show("line:" + line.ToString());
                        line_all += line;
                        //MessageBox.Show("line_all:" + line_all.ToString());
                    }
                    else
                    {
                        break;
                    }
                }

                List<string> tab = new List<string>();
                List<string> tabtext = new List<string>();
                string[] all = new string[100];
                string[] all_seprattab_text = new string[100];
                //line_all = line_all.Trim('\n');
                all = line_all.Split(';');
                for (int i = 0; i < all.GetUpperBound(0); i++)
                {
                    all_seprattab_text = all[i].Split('|');
                    tab.Add(all_seprattab_text[0]);
                    tabtext.Add(all_seprattab_text[1]);
                }

                tabControl1.TabPages.Clear();

                for (int i = 0; i < tab.Count; i++)
                {
                    try
                    {
                        //this.tabControl1.TabPages.Add(a);
                        RichTextBox rtb = new RichTextBox();
                        rtb.Size = new Size(679, 354);
                        rtb.Location = new Point(0, 0);
                        rtb.Multiline = true;
                        rtb.Font = new Font("新細明體", 16);
                        rtb.Text += tabtext[i];
                        //rtb.Text += System.DateTime.Now.ToString("yyyy - dd - MM , HH:mm") + "\r\n";
                        //rtb.ScrollBars = ScrollBars.Vertical;
                        //rtb.KeyDown += textBox_SelectAll;
                        rtb.ScrollBars = RichTextBoxScrollBars.Both;

                        Panel p = new Panel();
                        p.Location = new Point(0, 0);
                        p.Size = new Size(687, 380);
                        p.BackColor = Color.White;
                        TabPage tabPage = new TabPage();
                        tabPage.Text = tab[i];
                        this.tabControl1.TabPages.Add(tabPage);
                        tabPage.Controls.Add(rtb);
                        this.tabControl1.SelectedTab = tabPage;

                        tS_Status_lb.Text = "讀取成功！";
                        string savePath_new = string.Empty;
                        savePath_new = openFileDialog1.FileName;
                        savePath = savePath_new;
                    }
                    catch
                    {
                        tS_Status_lb.Text = "讀取失敗！";
                    }
                }
            }
        }

        private void 另存新檔toolStripButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.AddExtension= true;
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string tab = "";
                    string tabtext = "";
                    //string xmltab = "";
                    string xmltabtext = "";
                    RichTextBox rtbtext = new RichTextBox();
                    for (int itab = 0; itab < tabControl1.TabCount; itab++)
                    {
                        String a = "richTextBox" + itab;
                        tab = tabControl1.TabPages[itab].Text;
                        rtbtext = (RichTextBox)tabControl1.TabPages[itab].Controls[0];
                        tabtext = rtbtext.Text;
                        xmltabtext += tab + "|" + tabtext + ";";
                    }
                    StreamWriter SW = new StreamWriter(@saveFileDialog1.FileName);
                    savePath = saveFileDialog1.FileName;
                    SW.Write(xmltabtext);
                    SW.Flush();
                    SW.Close();
                    tS_Status_lb.Text = "儲存成功！";
                }
                catch
                {
                    tS_Status_lb.Text = "儲存失敗！";
                }
            }
        }

        private void tS_Status_lb_timer_Tick(object sender, EventArgs e)
        {
            tS_Status_lb.Text = "---------------";
        }
    }


    public class TabCancelEventArgs : CancelEventArgs
    {
        public readonly TabPage Tab;


        public TabCancelEventArgs(TabPage tab)
        {
            this.Tab = tab;
        }
    }
    public class TabEventArgs : EventArgs
    {
        public readonly TabPage Tab;

        public TabEventArgs(TabPage tab)
        {
            this.Tab = tab;
        }
    }
    public delegate void TabEventHandler(object sender, TabEventArgs e);
    public delegate void TabCancelEventHandler(object sender, TabCancelEventArgs e);

    public class AdvancedTabControl : TabControl
    {
        public AdvancedTabControl()
        {
            this.AllowClose = true;
            this.AllowSwap = true;
        }

        #region Tab Swapping and Tab Closing

        private TabPage predraggedTab, contextmenuTab;
        private Point dragStartPoint;
        private readonly ContextMenu tabCtxm = new ContextMenu();
        private bool allowClose, allowSwap;



        [DefaultValue(true)]
        [Category("Behavior")]
        [Description("Indicates wether tabs in the control can be manually closed by the user.")]
        public virtual bool AllowClose
        {
            get
            {
                return allowClose;
            }
            set
            {
                allowClose = value;
                rebuildContextMenu();
            }
        }

        [DefaultValue(true)]
        [Category("Behavior")]
        [Description("Indicates wether tabs in the control can be manually rearranged by the user. \nRequires AllowDrop to be enabled.")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual bool AllowSwap
        {
            get
            {
                return allowSwap;
            }
            set
            {
                allowSwap = value;

                if (!this.AllowDrop && value)
                    this.AllowDrop = true;
            }
        }

        [DefaultValue(true)]
        [RefreshProperties(RefreshProperties.All)]
        public override bool AllowDrop
        {
            get
            {
                return base.AllowDrop;
            }
            set
            {
                base.AllowDrop = value;

                if (!value && this.AllowSwap)
                    this.AllowSwap = false;
            }
        }



        protected override void OnMouseDown(MouseEventArgs e)
        {
            predraggedTab = getPointedTab(e.Location);
            dragStartPoint = e.Location;

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            predraggedTab = null;

            TabPage pointedTab = getPointedTab(e.Location);
            if (this.AllowClose && pointedTab != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextmenuTab = pointedTab;
                    tabCtxm.Show(this, e.Location);
                }

                if (e.Button == MouseButtons.Middle)
                {
                    contextmenuTab = pointedTab;
                    closeTab(this, e);
                }
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.AllowSwap && predraggedTab != null && e.Button == MouseButtons.Left && isDragGesture(dragStartPoint, e.Location))
                this.DoDragDrop(predraggedTab, DragDropEffects.Move);

            base.OnMouseMove(e);
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            TabPage draggedTab = (TabPage)drgevent.Data.GetData(typeof(TabPage));
            TabPage pointedTab = getPointedTab(this.PointToClient(new Point(drgevent.X, drgevent.Y)));

            if (draggedTab == predraggedTab && pointedTab != null)
            {
                drgevent.Effect = DragDropEffects.Move;

                if (pointedTab != draggedTab)
                    swapTabPages(draggedTab, pointedTab);
            }

            base.OnDragOver(drgevent);
        }



        private bool isDragGesture(Point start, Point current)
        {
            return Math.Abs(start.X - current.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(start.Y - current.Y) > SystemParameters.MinimumVerticalDragDistance;
        }

        private void rebuildContextMenu()
        {
            tabCtxm.MenuItems.Clear();

            if (this.AllowClose)
                tabCtxm.MenuItems.Add("Close", closeTab);
        }

        private void closeTab(object sender, EventArgs e)
        {
            this.OnTabClosing(new TabCancelEventArgs(contextmenuTab));
            contextmenuTab = null;
        }

        private void swapTabPages(TabPage src, TabPage dst)
        {
            int srci = this.TabPages.IndexOf(src);
            int dsti = this.TabPages.IndexOf(dst);

            this.TabPages[dsti] = src;
            this.TabPages[srci] = dst;

            if (this.SelectedIndex == srci)
                this.SelectedIndex = dsti;
            else if (this.SelectedIndex == dsti)
                this.SelectedIndex = srci;

            this.Refresh();
        }

        #endregion

        #region Custom Events

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event TabCancelEventHandler TabClosing;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event TabEventHandler TabClosed;

        // TODO: Add tabs swapped event.

        // TODO: Add tab mouseup, mousedown, click etc... events.

        protected virtual void OnTabClosing(TabCancelEventArgs e)
        {
            if (this.TabClosing != null)
                this.TabClosing(this, e);

            if (!e.Cancel)
            {
                this.TabPages.Remove(e.Tab);
                this.OnTabClosed(new TabEventArgs(e.Tab));
            }
        }

        protected virtual void OnTabClosed(TabEventArgs e)
        {
            if (this.TabClosed != null)
                this.TabClosed(this, e);
        }

        #endregion

        private TabPage getPointedTab(Point position)
        {
            for (int i = 0; i < this.TabPages.Count; i++)
                if (this.GetTabRect(i).Contains(position))
                    return this.TabPages[i];

            return null;
        }
    }
}

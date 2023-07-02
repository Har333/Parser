using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Parserr
{
    public partial class userControl2 : UserControl
    {
        public userControl2()
        {
            InitializeComponent();
        }
        public void AddCourse2(string courseName)
        {
            treeView1.Nodes.Add(courseName, courseName);
        }
        public void AddCourseSection(string key, string courseName)
        {
            TreeNode[] list = treeView1.Nodes.Find(key, true);
            list[0].Nodes.Add(courseName, courseName);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        public TreeNodeCollection nodeCollection() { return treeView1.Nodes; }
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }
            else if (e.Node.Nodes == null)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    if (node.Checked)
                    {
                        CheckChildNodes(node);
                    }
                }
            }
        }
        private void CheckChildNodes(TreeNode node)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                if (childNode.Checked)
                {
                    node.Checked = true;
                    break;
                }
            }

            if (node.Parent != null)
            {
                CheckChildNodes(node.Parent);
            }
        }
    }
}

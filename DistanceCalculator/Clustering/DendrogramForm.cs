using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DistanceCalculator.Clustering
{
    partial class DendrogramForm : Form
    {
        public DendrogramForm(Dendrogram dendrogram)
        {
            InitializeComponent();

            foreach (TreeNode node in GetTree(dendrogram).Nodes)
            {
                this.treeView1.Nodes.Add(node);
            }
        }

        private TreeNode GetTree(Dendrogram dendrogram)
        {
            if (dendrogram is LeafNode)
            {
                return new TreeNode(((LeafNode)dendrogram).Place.name);
            }
            else
            {
                TreeNode toRet = new TreeNode();
                toRet.Nodes.Add(GetTree(((InternalNode)dendrogram).Child1));
                toRet.Nodes.Add(GetTree(((InternalNode)dendrogram).Child2));
                toRet.Text = dendrogram.Name;
                return toRet;
            }
        }
    }
}

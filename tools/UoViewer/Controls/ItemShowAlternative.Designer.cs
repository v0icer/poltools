/***************************************************************************
 *
 * $Author: Turley
 * 
 * "THE BEER-WARE LICENSE"
 * As long as you retain this notice you can do whatever you want with 
 * this stuff. If we meet some day, and you think this stuff is worth it,
 * you can buy me a beer in return.
 *
 ***************************************************************************/

namespace Controls
{
    partial class ItemShowAlternative
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.DetailPictureBox = new System.Windows.Forms.PictureBox();
            this.DetailTextBox = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.namelabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.graphiclabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PreloadItems = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DetailPictureBox)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.DetailPictureBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.DetailTextBox);
            this.splitContainer2.Size = new System.Drawing.Size(155, 302);
            this.splitContainer2.SplitterDistance = 176;
            this.splitContainer2.TabIndex = 0;
            // 
            // DetailPictureBox
            // 
            this.DetailPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailPictureBox.Location = new System.Drawing.Point(0, 0);
            this.DetailPictureBox.Name = "DetailPictureBox";
            this.DetailPictureBox.Size = new System.Drawing.Size(155, 176);
            this.DetailPictureBox.TabIndex = 0;
            this.DetailPictureBox.TabStop = false;
            // 
            // DetailTextBox
            // 
            this.DetailTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailTextBox.Location = new System.Drawing.Point(0, 0);
            this.DetailTextBox.Name = "DetailTextBox";
            this.DetailTextBox.Size = new System.Drawing.Size(155, 122);
            this.DetailTextBox.TabIndex = 0;
            this.DetailTextBox.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.vScrollBar);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(619, 302);
            this.splitContainer1.SplitterDistance = 460;
            this.splitContainer1.TabIndex = 6;
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(443, 0);
            this.vScrollBar.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 302);
            this.vScrollBar.TabIndex = 0;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScroll);
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(460, 302);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseDoubleClick);
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
            this.pictureBox.SizeChanged += new System.EventHandler(this.OnResize);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "Search";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.OnSearchClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.namelabel,
            this.graphiclabel,
            this.toolStripStatusLabel1,
            this.PreloadItems,
            this.ProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 302);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(619, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // namelabel
            // 
            this.namelabel.AutoSize = false;
            this.namelabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.namelabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.namelabel.Name = "namelabel";
            this.namelabel.Size = new System.Drawing.Size(200, 17);
            this.namelabel.Text = "Name:";
            this.namelabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // graphiclabel
            // 
            this.graphiclabel.AutoSize = false;
            this.graphiclabel.Name = "graphiclabel";
            this.graphiclabel.Size = new System.Drawing.Size(150, 17);
            this.graphiclabel.Text = "Graphic:";
            this.graphiclabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PreloadItems
            // 
            this.PreloadItems.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.PreloadItems.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.PreloadItems.Name = "PreloadItems";
            this.PreloadItems.Size = new System.Drawing.Size(77, 17);
            this.PreloadItems.Text = "Preload Items";
            this.PreloadItems.Click += new System.EventHandler(this.OnClickPreload);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // ItemShowAlternative
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ItemShowAlternative";
            this.Size = new System.Drawing.Size(619, 324);
            this.Load += new System.EventHandler(this.OnLoad);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DetailPictureBox)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox DetailPictureBox;
        private System.Windows.Forms.RichTextBox DetailTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel namelabel;
        private System.Windows.Forms.ToolStripStatusLabel graphiclabel;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripStatusLabel PreloadItems;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;

    }
}
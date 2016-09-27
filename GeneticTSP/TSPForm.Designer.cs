namespace GeneticTSP
{
    partial class TSPForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_DrawSurface = new System.Windows.Forms.PictureBox();
            this.m_gen_box = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_DrawSurface)).BeginInit();
            this.SuspendLayout();
            // 
            // m_DrawSurface
            // 
            this.m_DrawSurface.Location = new System.Drawing.Point(66, 59);
            this.m_DrawSurface.Name = "m_DrawSurface";
            this.m_DrawSurface.Size = new System.Drawing.Size(704, 468);
            this.m_DrawSurface.TabIndex = 0;
            this.m_DrawSurface.TabStop = false;
            // 
            // m_gen_box
            // 
            this.m_gen_box.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.m_gen_box.Location = new System.Drawing.Point(23, 24);
            this.m_gen_box.Name = "m_gen_box";
            this.m_gen_box.Size = new System.Drawing.Size(117, 20);
            this.m_gen_box.TabIndex = 1;
            this.m_gen_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TSPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 556);
            this.Controls.Add(this.m_gen_box);
            this.Controls.Add(this.m_DrawSurface);
            this.Name = "TSPForm";
            this.Text = "GeneticTSP";
            ((System.ComponentModel.ISupportInitialize)(this.m_DrawSurface)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_DrawSurface;
        private System.Windows.Forms.TextBox m_gen_box;
    }
}


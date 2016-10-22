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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.crossoverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.permutationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderBasedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.positionBasedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mutationTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exchangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrambleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displacementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displacedInversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sigmaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boltzmannToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selecitonMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roulletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tournamentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sUSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elitismToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_gen_label = new System.Windows.Forms.Label();
            this.m_boltz_temp_lbl = new System.Windows.Forms.Label();
            this.m_boltz_text = new System.Windows.Forms.TextBox();
            this.m_message_lbl = new System.Windows.Forms.Label();
            this.m_message_text = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_DrawSurface)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_DrawSurface
            // 
            this.m_DrawSurface.Location = new System.Drawing.Point(23, 50);
            this.m_DrawSurface.Name = "m_DrawSurface";
            this.m_DrawSurface.Size = new System.Drawing.Size(1849, 899);
            this.m_DrawSurface.TabIndex = 0;
            this.m_DrawSurface.TabStop = false;
            // 
            // m_gen_box
            // 
            this.m_gen_box.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.m_gen_box.Location = new System.Drawing.Point(88, 24);
            this.m_gen_box.Name = "m_gen_box";
            this.m_gen_box.Size = new System.Drawing.Size(64, 20);
            this.m_gen_box.TabIndex = 1;
            this.m_gen_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crossoverToolStripMenuItem,
            this.mutationTypeToolStripMenuItem,
            this.scaleTypeToolStripMenuItem,
            this.selecitonMethodToolStripMenuItem,
            this.elitismToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1884, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // crossoverToolStripMenuItem
            // 
            this.crossoverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.permutationToolStripMenuItem,
            this.orderBasedToolStripMenuItem,
            this.positionBasedToolStripMenuItem});
            this.crossoverToolStripMenuItem.Name = "crossoverToolStripMenuItem";
            this.crossoverToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.crossoverToolStripMenuItem.Text = "Crossover Type";
            // 
            // permutationToolStripMenuItem
            // 
            this.permutationToolStripMenuItem.Name = "permutationToolStripMenuItem";
            this.permutationToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.permutationToolStripMenuItem.Text = "Permutation";
            this.permutationToolStripMenuItem.Click += new System.EventHandler(this.permutationToolStripMenuItem_Click);
            // 
            // orderBasedToolStripMenuItem
            // 
            this.orderBasedToolStripMenuItem.Checked = true;
            this.orderBasedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.orderBasedToolStripMenuItem.Name = "orderBasedToolStripMenuItem";
            this.orderBasedToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.orderBasedToolStripMenuItem.Text = "Order Based";
            this.orderBasedToolStripMenuItem.Click += new System.EventHandler(this.orderBasedToolStripMenuItem_Click);
            // 
            // positionBasedToolStripMenuItem
            // 
            this.positionBasedToolStripMenuItem.Name = "positionBasedToolStripMenuItem";
            this.positionBasedToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.positionBasedToolStripMenuItem.Text = "Position Based";
            this.positionBasedToolStripMenuItem.Click += new System.EventHandler(this.positionBasedToolStripMenuItem_Click);
            // 
            // mutationTypeToolStripMenuItem
            // 
            this.mutationTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exchangeToolStripMenuItem,
            this.scrambleToolStripMenuItem,
            this.insertionToolStripMenuItem,
            this.displacementToolStripMenuItem,
            this.inversionToolStripMenuItem,
            this.displacedInversionToolStripMenuItem});
            this.mutationTypeToolStripMenuItem.Name = "mutationTypeToolStripMenuItem";
            this.mutationTypeToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.mutationTypeToolStripMenuItem.Text = "Mutation Type";
            // 
            // exchangeToolStripMenuItem
            // 
            this.exchangeToolStripMenuItem.Name = "exchangeToolStripMenuItem";
            this.exchangeToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.exchangeToolStripMenuItem.Text = "Exchange";
            this.exchangeToolStripMenuItem.Click += new System.EventHandler(this.exchangeToolStripMenuItem_Click);
            // 
            // scrambleToolStripMenuItem
            // 
            this.scrambleToolStripMenuItem.Name = "scrambleToolStripMenuItem";
            this.scrambleToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.scrambleToolStripMenuItem.Text = "Scramble";
            this.scrambleToolStripMenuItem.Click += new System.EventHandler(this.scrambleToolStripMenuItem_Click);
            // 
            // insertionToolStripMenuItem
            // 
            this.insertionToolStripMenuItem.Checked = true;
            this.insertionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.insertionToolStripMenuItem.Name = "insertionToolStripMenuItem";
            this.insertionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.insertionToolStripMenuItem.Text = "Insertion";
            this.insertionToolStripMenuItem.Click += new System.EventHandler(this.insertionToolStripMenuItem_Click);
            // 
            // displacementToolStripMenuItem
            // 
            this.displacementToolStripMenuItem.Name = "displacementToolStripMenuItem";
            this.displacementToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.displacementToolStripMenuItem.Text = "Displacement";
            this.displacementToolStripMenuItem.Click += new System.EventHandler(this.displacementToolStripMenuItem_Click);
            // 
            // inversionToolStripMenuItem
            // 
            this.inversionToolStripMenuItem.Name = "inversionToolStripMenuItem";
            this.inversionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.inversionToolStripMenuItem.Text = "Inversion";
            this.inversionToolStripMenuItem.Click += new System.EventHandler(this.inversionToolStripMenuItem_Click);
            // 
            // displacedInversionToolStripMenuItem
            // 
            this.displacedInversionToolStripMenuItem.Name = "displacedInversionToolStripMenuItem";
            this.displacedInversionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.displacedInversionToolStripMenuItem.Text = "DisplacedInversion";
            this.displacedInversionToolStripMenuItem.Click += new System.EventHandler(this.displacedInversionToolStripMenuItem_Click);
            // 
            // scaleTypeToolStripMenuItem
            // 
            this.scaleTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.rankToolStripMenuItem,
            this.sigmaToolStripMenuItem,
            this.boltzmannToolStripMenuItem});
            this.scaleTypeToolStripMenuItem.Name = "scaleTypeToolStripMenuItem";
            this.scaleTypeToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.scaleTypeToolStripMenuItem.Text = "Scale Type";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noScaleToolStripMenuItem_Click);
            // 
            // rankToolStripMenuItem
            // 
            this.rankToolStripMenuItem.Name = "rankToolStripMenuItem";
            this.rankToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.rankToolStripMenuItem.Text = "Rank";
            this.rankToolStripMenuItem.Click += new System.EventHandler(this.rankToolStripMenuItem_Click);
            // 
            // sigmaToolStripMenuItem
            // 
            this.sigmaToolStripMenuItem.Name = "sigmaToolStripMenuItem";
            this.sigmaToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.sigmaToolStripMenuItem.Text = "Sigma";
            this.sigmaToolStripMenuItem.Click += new System.EventHandler(this.sigmaToolStripMenuItem_Click);
            // 
            // boltzmannToolStripMenuItem
            // 
            this.boltzmannToolStripMenuItem.Checked = true;
            this.boltzmannToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.boltzmannToolStripMenuItem.Name = "boltzmannToolStripMenuItem";
            this.boltzmannToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.boltzmannToolStripMenuItem.Text = "Boltzmann";
            this.boltzmannToolStripMenuItem.Click += new System.EventHandler(this.boltzmannToolStripMenuItem_Click);
            // 
            // selecitonMethodToolStripMenuItem
            // 
            this.selecitonMethodToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roulletteToolStripMenuItem,
            this.tournamentToolStripMenuItem,
            this.sUSToolStripMenuItem});
            this.selecitonMethodToolStripMenuItem.Name = "selecitonMethodToolStripMenuItem";
            this.selecitonMethodToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.selecitonMethodToolStripMenuItem.Text = "Selection Method";
            // 
            // roulletteToolStripMenuItem
            // 
            this.roulletteToolStripMenuItem.Name = "roulletteToolStripMenuItem";
            this.roulletteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.roulletteToolStripMenuItem.Text = "Roullette";
            this.roulletteToolStripMenuItem.Click += new System.EventHandler(this.roulletteToolStripMenuItem_Click);
            // 
            // tournamentToolStripMenuItem
            // 
            this.tournamentToolStripMenuItem.Checked = true;
            this.tournamentToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tournamentToolStripMenuItem.Name = "tournamentToolStripMenuItem";
            this.tournamentToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.tournamentToolStripMenuItem.Text = "Tournament";
            this.tournamentToolStripMenuItem.Click += new System.EventHandler(this.tournamentToolStripMenuItem_Click);
            // 
            // sUSToolStripMenuItem
            // 
            this.sUSToolStripMenuItem.Name = "sUSToolStripMenuItem";
            this.sUSToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.sUSToolStripMenuItem.Text = "SUS";
            this.sUSToolStripMenuItem.Click += new System.EventHandler(this.SUSToolStripMenuItem_Click);
            // 
            // elitismToolStripMenuItem
            // 
            this.elitismToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem,
            this.offToolStripMenuItem});
            this.elitismToolStripMenuItem.Name = "elitismToolStripMenuItem";
            this.elitismToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.elitismToolStripMenuItem.Text = "Elitism";
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Checked = true;
            this.onToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.onToolStripMenuItem.Text = "On";
            this.onToolStripMenuItem.Click += new System.EventHandler(this.elitismOnToolStripMenuItem_Click);
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.elitismOffToolStripMenuItem_Click);
            // 
            // m_gen_label
            // 
            this.m_gen_label.AutoSize = true;
            this.m_gen_label.Location = new System.Drawing.Point(20, 27);
            this.m_gen_label.Name = "m_gen_label";
            this.m_gen_label.Size = new System.Drawing.Size(62, 13);
            this.m_gen_label.TabIndex = 3;
            this.m_gen_label.Text = "Generation:";
            // 
            // m_boltz_temp_lbl
            // 
            this.m_boltz_temp_lbl.AutoSize = true;
            this.m_boltz_temp_lbl.Location = new System.Drawing.Point(158, 27);
            this.m_boltz_temp_lbl.Name = "m_boltz_temp_lbl";
            this.m_boltz_temp_lbl.Size = new System.Drawing.Size(67, 13);
            this.m_boltz_temp_lbl.TabIndex = 4;
            this.m_boltz_temp_lbl.Text = "Temperaute:";
            // 
            // m_boltz_text
            // 
            this.m_boltz_text.Location = new System.Drawing.Point(231, 24);
            this.m_boltz_text.Name = "m_boltz_text";
            this.m_boltz_text.Size = new System.Drawing.Size(64, 20);
            this.m_boltz_text.TabIndex = 5;
            this.m_boltz_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_message_lbl
            // 
            this.m_message_lbl.AutoSize = true;
            this.m_message_lbl.Location = new System.Drawing.Point(507, 27);
            this.m_message_lbl.Name = "m_message_lbl";
            this.m_message_lbl.Size = new System.Drawing.Size(53, 13);
            this.m_message_lbl.TabIndex = 6;
            this.m_message_lbl.Text = "Message:";
            // 
            // m_message_text
            // 
            this.m_message_text.Location = new System.Drawing.Point(566, 24);
            this.m_message_text.Name = "m_message_text";
            this.m_message_text.Size = new System.Drawing.Size(358, 20);
            this.m_message_text.TabIndex = 7;
            // 
            // TSPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1884, 961);
            this.Controls.Add(this.m_message_text);
            this.Controls.Add(this.m_message_lbl);
            this.Controls.Add(this.m_boltz_text);
            this.Controls.Add(this.m_boltz_temp_lbl);
            this.Controls.Add(this.m_gen_label);
            this.Controls.Add(this.m_gen_box);
            this.Controls.Add(this.m_DrawSurface);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TSPForm";
            this.Text = "GeneticTSP";
            ((System.ComponentModel.ISupportInitialize)(this.m_DrawSurface)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_DrawSurface;
        private System.Windows.Forms.TextBox m_gen_box;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem crossoverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem permutationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderBasedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem positionBasedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mutationTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scaleTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selecitonMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elitismToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exchangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scrambleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displacementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sigmaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boltzmannToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roulletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tournamentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sUSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displacedInversionToolStripMenuItem;
        private System.Windows.Forms.Label m_gen_label;
        private System.Windows.Forms.Label m_boltz_temp_lbl;
        private System.Windows.Forms.TextBox m_boltz_text;
        private System.Windows.Forms.Label m_message_lbl;
        private System.Windows.Forms.TextBox m_message_text;
    }
}


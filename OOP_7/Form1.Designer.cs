namespace OOP_7
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            id = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            textBox1 = new TextBox();
            comboBox2 = new ComboBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            button2 = new Button();
            button3 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            groupBox1 = new GroupBox();
            splitter1 = new Splitter();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(497, 48);
            button1.Name = "button1";
            button1.Size = new Size(325, 29);
            button1.TabIndex = 0;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(175, 49);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { id, Column1, Column2, Column3, Column4, Column5, Column6 });
            dataGridView1.Location = new Point(14, 15);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(925, 371);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // id
            // 
            id.HeaderText = "id";
            id.MinimumWidth = 6;
            id.Name = "id";
            id.ReadOnly = true;
            id.Width = 125;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column1.HeaderText = "Имя студента";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.HeaderText = "№ Зачетки";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 125;
            // 
            // Column3
            // 
            Column3.HeaderText = "№ Групппы";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 125;
            // 
            // Column4
            // 
            Column4.HeaderText = "Институт";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 125;
            // 
            // Column5
            // 
            Column5.HeaderText = "Направление";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 125;
            // 
            // Column6
            // 
            Column6.HeaderText = "Дата поступления";
            Column6.MinimumWidth = 6;
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Width = 125;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(16, 49);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(151, 27);
            textBox1.TabIndex = 3;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(175, 116);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 4;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(16, 116);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(151, 27);
            textBox2.TabIndex = 5;
            textBox2.KeyPress += textBox2_KeyPress;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(334, 49);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(156, 27);
            textBox3.TabIndex = 6;
            textBox3.KeyPress += textBox3_KeyPress;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(334, 116);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(156, 27);
            dateTimePicker1.TabIndex = 7;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // button2
            // 
            button2.Location = new Point(497, 117);
            button2.Name = "button2";
            button2.Size = new Size(325, 29);
            button2.TabIndex = 8;
            button2.Text = "Удалить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(497, 83);
            button3.Name = "button3";
            button3.Size = new Size(325, 29);
            button3.TabIndex = 9;
            button3.Text = "Изменить";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 25);
            label1.Name = "label1";
            label1.Size = new Size(102, 20);
            label1.TabIndex = 10;
            label1.Text = "Имя студента";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 92);
            label2.Name = "label2";
            label2.Size = new Size(114, 20);
            label2.TabIndex = 11;
            label2.Text = "Номер зачетки";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(334, 25);
            label3.Name = "label3";
            label3.Size = new Size(112, 20);
            label3.TabIndex = 12;
            label3.Text = "Номер группы";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(334, 92);
            label4.Name = "label4";
            label4.Size = new Size(94, 20);
            label4.TabIndex = 13;
            label4.Text = "Дата записи";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(175, 25);
            label5.Name = "label5";
            label5.Size = new Size(70, 20);
            label5.TabIndex = 14;
            label5.Text = "Институт";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(175, 92);
            label6.Name = "label6";
            label6.Size = new Size(104, 20);
            label6.TabIndex = 15;
            label6.Text = "Направление";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom;
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Location = new Point(58, 392);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.MaximumSize = new Size(835, 183);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(835, 183);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            // 
            // splitter1
            // 
            splitter1.Location = new Point(0, 0);
            splitter1.Margin = new Padding(3, 4, 3, 4);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 575);
            splitter1.TabIndex = 17;
            splitter1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 575);
            Controls.Add(splitter1);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            MinimumSize = new Size(965, 611);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private ComboBox comboBox1;
        private DataGridView dataGridView1;
        private TextBox textBox1;
        private ComboBox comboBox2;
        private TextBox textBox2;
        private TextBox textBox3;
        private DateTimePicker dateTimePicker1;
        private Button button2;
        private Button button3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private GroupBox groupBox1;
        private Splitter splitter1;
    }
}

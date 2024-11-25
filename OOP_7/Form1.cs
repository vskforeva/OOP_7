using System.Text.RegularExpressions;

namespace OOP_7
{
    public partial class Form1 : Form
    {
        private JsonDataHandler jsonDataHandler = new JsonDataHandler();
        public Form1()
        {
            InitializeComponent();
        }
        public class WeekDay
        {
            private int _id;
            private string _dayName;
            private string _specName;
            public int Id { get => _id; set => _id = value; }
            public string DayName { get => _dayName; set => _dayName = value; }
            public string SpecName { get => _specName; set => _specName = value; }
        }
        private void initWeekDays()
        {
            List<WeekDay> weekDays = new List<WeekDay> {
                        new WeekDay{ Id = 1, DayName = "�����" },
                        new WeekDay{ Id = 2, DayName = "���" },
                        new WeekDay{ Id = 3, DayName = "���" },
                        new WeekDay{ Id = 4, DayName = "���" },
                        new WeekDay{ Id = 5, DayName = "���" },
                        new WeekDay{ Id = 6, DayName = "���" },
                        new WeekDay{ Id = 7, DayName = "���" },
            };
            comboBox1.DataSource = weekDays;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "DayName";
        }
        private void initSpecDays()
        {
            List<WeekDay> specDays = new List<WeekDay>
            {
                        new WeekDay{ Id = 1, SpecName = "���������� �����������" },
                        new WeekDay{ Id = 2, SpecName = "�����������" },
                        new WeekDay{ Id = 3, SpecName = "���������� � ������������ �����" },

            };
            comboBox2.DataSource = specDays;
            comboBox2.ValueMember = "Id";
            comboBox2.DisplayMember = "SpecName";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initWeekDays();
            initSpecDays();

            LoadData();
            dataGridView1.Columns[0].Visible = false; // 0 - ������ ������� ID

        }
        private void LoadData()
        {
            var rowsData = jsonDataHandler.LoadData();

            foreach (var row in rowsData)
            {
                dataGridView1.Rows.Add(
                    row.Id,
                    row.TextField1,
                    row.TextField2,
                    row.TextField3,
                    row.DayName,
                    row.SpecName,
                    row.DateValue);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ���������, ��������� �� ����������� ����
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("����������, ��������� ��� ��������� ����.");
                return;
            }

            // �������� ����� ������ �������
            if (textBox2.Text.Length < 8)
            {
                MessageBox.Show("������� ����� ������� ���������");
                return; // ���������� ���������� ������, ���� ����� ������ 8
            }

            // �������� ������� ��� textBox3
            string currentText = textBox3.Text;
            if (!string.IsNullOrWhiteSpace(currentText))
            {
                Regex regex = new Regex(@"^\d{3,}[�-��-�]?-?[�-��-�]{2,}$");
                if (!regex.IsMatch(currentText))
                {
                    MessageBox.Show("������: ������� ������ �� �������, ��������: 100�-���; 200-��");
                    return; // ���������� ���������� ������, ���� ������ �� ��������
                }
            }
            else
            {
                MessageBox.Show("������: ���� �� ������ ���� ������.");
                return; // ���������� ���������� ������, ���� ���� ������
            }

            // �������� �� ������������ ������ ������� (��������������, ��� ��� column2)
            string studentRecordNumber = textBox2.Text; // ������������, ��� ����� ������� �������� � textBox2
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow && row.Cells[2].Value != null && row.Cells[2].Value.ToString() == studentRecordNumber)
                {
                    MessageBox.Show("������� � ����� ������� ������� ��� ����������!");
                    return;
                }
            }

            int newId = GetNextId(); // ��������� ����� ������ � DataGridView
            dataGridView1.Rows.Add(
                newId,
                textBox1.Text,
                studentRecordNumber,
                currentText,
                ((WeekDay)comboBox1.SelectedItem).DayName.ToString(),
                ((WeekDay)comboBox2.SelectedItem).SpecName.ToString(),
                dateTimePicker1.Value // ��������� �������� �� DateTimePicker � 6-� �������
            );

            SaveData(); // ��������� ������ � JSON ����� ����������
        }

        private int GetNextId()
        {
            int maxId = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int id))
                {
                    if (id > maxId)
                    {
                        maxId = id;
                    }
                }
            }
            // Find the first missing ID
            for (int i = 1; i <= maxId + 1; i++)
            {
                bool idExists = false;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == i.ToString())
                    {
                        idExists = true;
                        break;
                    }
                }

                if (!idExists)
                {
                    return i; // Return the first missing ID
                }
            }

            return maxId + 1; // If no missing ID found, return the next available ID
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // ���������, ������� �� ������
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // ������� ���������� ������
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow) // ���������, ��� ��� �� ����� ������
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                }

                // ��������� ID ����� �������� (�����������)
                UpdateIds();
                SaveData();
            }
            else
            {
                MessageBox.Show("����������, �������� ������ ��� ��������.");
            }
        }

        private void UpdateIds()
        {
            int currentId = 1;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    row.Cells[0].Value = currentId++;
                }
            }
        }
        private void SaveData()
        {
            var rowsData = new List<DataRow>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    rowsData.Add(new DataRow
                    {
                        Id = Convert.ToInt32(row.Cells[0].Value),
                        TextField1 = row.Cells[1].Value?.ToString(),
                        TextField2 = row.Cells[2].Value?.ToString(),
                        TextField3 = row.Cells[3].Value?.ToString(),
                        DayName = row.Cells[4].Value?.ToString(),
                        SpecName = row.Cells[5].Value?.ToString(),
                        DateValue = Convert.ToDateTime(row.Cells[6].Value)
                    });
                }
            }

            jsonDataHandler.SaveData(rowsData); // ��������� ������ ����� JsonDataHandler
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // ���������, ��� ������ ������ ������ -1 � �� �������� ����� �������
            if (e.RowIndex >= 0 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                // �������� ��������� ������
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // ��������� ��������� ���� ������� �� ��������� ������
                textBox1.Text = selectedRow.Cells[1].Value?.ToString() ?? ""; // �������� 1 �� ������ ������ �������
                textBox2.Text = selectedRow.Cells[2].Value?.ToString() ?? ""; // �������� 2 �� ������ ������ �������
                textBox3.Text = selectedRow.Cells[3].Value?.ToString() ?? ""; // �������� 3 �� ������ ������ �������

                // �������� �������� ��� � ������������� �� �����
                string dayName = selectedRow.Cells[4].Value?.ToString() ?? ""; // �������� 4 �� ������ ������ ������� ��� DayName
                string specName = selectedRow.Cells[5].Value?.ToString() ?? ""; // �������� 5 �� ������ ������ ������� ��� SpecName

                // ������� ��������������� ID ��� ComboBox1 (����)
                var weekDays = (List<WeekDay>)comboBox1.DataSource;
                var day = weekDays.FirstOrDefault(w => w.DayName == dayName);
                if (day != null)
                {
                    comboBox1.SelectedValue = day.Id; // ������������� ID ���
                }
                else
                {
                    MessageBox.Show("���� �� ������: " + dayName);
                }

                // ������� ��������������� ID ��� ComboBox2 (�������������)
                var specDays = (List<WeekDay>)comboBox2.DataSource;
                var spec = specDays.FirstOrDefault(s => s.SpecName == specName);
                if (spec != null)
                {
                    comboBox2.SelectedValue = spec.Id; // ������������� ID �������������
                }
                else
                {
                    MessageBox.Show("������������� �� �������: " + specName);
                }

                // ������������� ���� � DateTimePicker �� ������ 6
                if (selectedRow.Cells[6].Value is DateTime dateValue) // �������� 6 �� ������ ������ ������� ��� ����
                {
                    dateTimePicker1.Value = dateValue; // ������������� �������� � DateTimePicker
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // ���������, ������� �� ������
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // �������� ���������� ������
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // ��������� �������� � ��������� ������
                selectedRow.Cells[1].Value = textBox1.Text; // ��������� �������� �� textBox1
                selectedRow.Cells[2].Value = textBox2.Text; // ��������� �������� �� textBox2
                selectedRow.Cells[3].Value = textBox3.Text; // ��������� �������� �� textBox3

                // ������������� �������� ��� � ������������� �� �����������
                selectedRow.Cells[4].Value = ((WeekDay)comboBox1.SelectedItem).DayName; // ��������� �������� ��� DayName
                selectedRow.Cells[5].Value = ((WeekDay)comboBox2.SelectedItem).SpecName; // ��������� �������� ��� SpecName

                // ������������� ���� �� DateTimePicker
                selectedRow.Cells[6].Value = dateTimePicker1.Value; // ��������� �������� ��� ����
                SaveData();
            }
            else
            {
                MessageBox.Show("����������, �������� ������ ��� ���������.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is WeekDay selectedDay)
            {
                UpdateSpecComboBox(selectedDay.Id);
                UpdateSelectedRow();
            }
        }
        private void UpdateSpecComboBox(int dayId)
        {
            List<WeekDay> specDays = new List<WeekDay>();

            // ���������� ��������� ������������� �� ������ ���������� ���
            switch (dayId)
            {
                case 1: // �����
                    specDays.Add(new WeekDay { Id = 1, SpecName = "���������� �����������" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "�����������" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "���������� � ������������ �����" });
                    break;
                case 2: // ���
                    specDays.Add(new WeekDay { Id = 4, SpecName = "�������" });
                    specDays.Add(new WeekDay { Id = 5, SpecName = "������� ����" });
                    specDays.Add(new WeekDay { Id = 6, SpecName = "����������" });
                    break;
                case 3: //���
                    specDays.Add(new WeekDay { Id = 1, SpecName = "��������" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "�����" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "��������" });
                    break;
                case 4://���
                    specDays.Add(new WeekDay { Id = 1, SpecName = "���������� ����" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "�������� ����" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "���� ����" });
                    break;
                case 5: //���
                    specDays.Add(new WeekDay { Id = 1, SpecName = "�����" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "������� ������" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "���������" });
                    break;
                case 6: //���
                    specDays.Add(new WeekDay { Id = 1, SpecName = "���������" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "��������" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "������" });
                    break;
                case 7: //���
                    specDays.Add(new WeekDay { Id = 1, SpecName = "����������" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "����������" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "���������" });
                    break;
                // �������� �������������� ������ ��� ������ �������� dayId
                default:
                    break;
            }

            // ��������� ComboBox2
            comboBox2.DataSource = specDays;
            comboBox2.ValueMember = "Id";
            comboBox2.DisplayMember = "SpecName";
        }
        private void UpdateSelectedRow()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // ��������� �������� � ��������� ������
                selectedRow.Cells[1].Value = textBox1.Text; // ��������� �������� �� textBox1
                selectedRow.Cells[2].Value = textBox2.Text; // ��������� �������� �� textBox2
                selectedRow.Cells[3].Value = textBox3.Text; // ��������� �������� �� textBox3

                selectedRow.Cells[4].Value = ((WeekDay)comboBox1.SelectedItem).DayName; // ��������� �������� ��� DayName
                selectedRow.Cells[5].Value = ((WeekDay)comboBox2.SelectedItem).SpecName; // ��������� �������� ��� SpecName
                selectedRow.Cells[6].Value = dateTimePicker1.Value; // ��������� �������� ��� ����

                SaveData(); // ��������� ������ � JSON ����� ���������
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            UpdateSelectedRow();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedRow();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            UpdateSelectedRow();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
            }
            else if (textBox2.Text.Length >= 8 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
            }
            UpdateSelectedRow();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
        
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                string currentText = textBox3.Text;
                if (!string.IsNullOrWhiteSpace(currentText))
                {
                    Regex regex = new Regex(@"^\d{3,}[�-��-�]?-?[�-��-�]{2,}$");
                    if (!regex.IsMatch(currentText))
                    {
                        MessageBox.Show("������: ������� ������ �� �������, ��������: 100�-���; 200-��");
                        return; 
                    }
                    UpdateSelectedRow();
                }
                else
                {
                    MessageBox.Show("������: ���� �� ������ ���� ������.");
                }
            }

        }
    }
}

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
                        new WeekDay{ Id = 1, DayName = "ИТНИТ" },
                        new WeekDay{ Id = 2, DayName = "ИГН" },
                        new WeekDay{ Id = 3, DayName = "ИЕН" },
                        new WeekDay{ Id = 4, DayName = "ИИЯ" },
                        new WeekDay{ Id = 5, DayName = "ИИП" },
                        new WeekDay{ Id = 6, DayName = "ИКИ" },
                        new WeekDay{ Id = 7, DayName = "ИПП" },
            };
            comboBox1.DataSource = weekDays;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "DayName";
        }
        private void initSpecDays()
        {
            List<WeekDay> specDays = new List<WeekDay>
            {
                        new WeekDay{ Id = 1, SpecName = "Прикладная информатика" },
                        new WeekDay{ Id = 2, SpecName = "Радиофизика" },
                        new WeekDay{ Id = 3, SpecName = "Математика и компьютерные науки" },

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
            dataGridView1.Columns[0].Visible = false; // 0 - индекс столбца ID

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
            // Проверяем, заполнены ли необходимые поля
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все текстовые поля.");
                return;
            }

            // Проверка длины номера зачетки
            if (textBox2.Text.Length < 8)
            {
                MessageBox.Show("Введите номер зачетки полностью");
                return; // Прекращаем выполнение метода, если длина меньше 8
            }

            // Проверка формата для textBox3
            string currentText = textBox3.Text;
            if (!string.IsNullOrWhiteSpace(currentText))
            {
                Regex regex = new Regex(@"^\d{3,}[а-яА-Я]?-?[а-яА-Я]{2,}$");
                if (!regex.IsMatch(currentText))
                {
                    MessageBox.Show("Ошибка: Введите данные по шаблону, например: 100б-ПИо; 200-ЯЗ");
                    return; // Прекращаем выполнение метода, если формат не подходит
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Поле не должно быть пустым.");
                return; // Прекращаем выполнение метода, если поле пустое
            }

            // Проверка на уникальность номера зачетки (предполагается, что это column2)
            string studentRecordNumber = textBox2.Text; // Предполагаем, что номер зачетки вводится в textBox2
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow && row.Cells[2].Value != null && row.Cells[2].Value.ToString() == studentRecordNumber)
                {
                    MessageBox.Show("Студент с таким номером зачетки уже существует!");
                    return;
                }
            }

            int newId = GetNextId(); // Добавляем новую строку в DataGridView
            dataGridView1.Rows.Add(
                newId,
                textBox1.Text,
                studentRecordNumber,
                currentText,
                ((WeekDay)comboBox1.SelectedItem).DayName.ToString(),
                ((WeekDay)comboBox2.SelectedItem).SpecName.ToString(),
                dateTimePicker1.Value // Добавляем значение из DateTimePicker в 6-й столбик
            );

            SaveData(); // Сохраняем данные в JSON после добавления
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
            // Проверяем, выбрана ли строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Удаляем выделенные строки
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow) // Проверяем, что это не новая строка
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                }

                // Обновляем ID после удаления (опционально)
                UpdateIds();
                SaveData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления.");
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

            jsonDataHandler.SaveData(rowsData); // Сохраняем данные через JsonDataHandler
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверяем, что индекс строки больше -1 и не является новой строкой
            if (e.RowIndex >= 0 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Заполняем текстовые поля данными из выбранной строки
                textBox1.Text = selectedRow.Cells[1].Value?.ToString() ?? ""; // Замените 1 на индекс вашего столбца
                textBox2.Text = selectedRow.Cells[2].Value?.ToString() ?? ""; // Замените 2 на индекс вашего столбца
                textBox3.Text = selectedRow.Cells[3].Value?.ToString() ?? ""; // Замените 3 на индекс вашего столбца

                // Получаем названия дня и специальности из ячеек
                string dayName = selectedRow.Cells[4].Value?.ToString() ?? ""; // Замените 4 на индекс вашего столбца для DayName
                string specName = selectedRow.Cells[5].Value?.ToString() ?? ""; // Замените 5 на индекс вашего столбца для SpecName

                // Находим соответствующий ID для ComboBox1 (день)
                var weekDays = (List<WeekDay>)comboBox1.DataSource;
                var day = weekDays.FirstOrDefault(w => w.DayName == dayName);
                if (day != null)
                {
                    comboBox1.SelectedValue = day.Id; // Устанавливаем ID дня
                }
                else
                {
                    MessageBox.Show("День не найден: " + dayName);
                }

                // Находим соответствующий ID для ComboBox2 (специальность)
                var specDays = (List<WeekDay>)comboBox2.DataSource;
                var spec = specDays.FirstOrDefault(s => s.SpecName == specName);
                if (spec != null)
                {
                    comboBox2.SelectedValue = spec.Id; // Устанавливаем ID специальности
                }
                else
                {
                    MessageBox.Show("Специальность не найдена: " + specName);
                }

                // Устанавливаем дату в DateTimePicker из ячейки 6
                if (selectedRow.Cells[6].Value is DateTime dateValue) // Замените 6 на индекс вашего столбца для даты
                {
                    dateTimePicker1.Value = dateValue; // Устанавливаем значение в DateTimePicker
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем выделенную строку
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Обновляем значения в выбранной строке
                selectedRow.Cells[1].Value = textBox1.Text; // Обновляем значение из textBox1
                selectedRow.Cells[2].Value = textBox2.Text; // Обновляем значение из textBox2
                selectedRow.Cells[3].Value = textBox3.Text; // Обновляем значение из textBox3

                // Устанавливаем названия дня и специальности из комбобоксов
                selectedRow.Cells[4].Value = ((WeekDay)comboBox1.SelectedItem).DayName; // Обновляем значение для DayName
                selectedRow.Cells[5].Value = ((WeekDay)comboBox2.SelectedItem).SpecName; // Обновляем значение для SpecName

                // Устанавливаем дату из DateTimePicker
                selectedRow.Cells[6].Value = dateTimePicker1.Value; // Обновляем значение для даты
                SaveData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для изменения.");
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

            // Определяем доступные специальности на основе выбранного дня
            switch (dayId)
            {
                case 1: // ИТНИТ
                    specDays.Add(new WeekDay { Id = 1, SpecName = "Прикладная информатика" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "Радиофизика" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "Математика и компьютерные науки" });
                    break;
                case 2: // ИГН
                    specDays.Add(new WeekDay { Id = 4, SpecName = "История" });
                    specDays.Add(new WeekDay { Id = 5, SpecName = "Русский язык" });
                    specDays.Add(new WeekDay { Id = 6, SpecName = "Литература" });
                    break;
                case 3: //ИЕН
                    specDays.Add(new WeekDay { Id = 1, SpecName = "Ботаника" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "Химия" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "Биология" });
                    break;
                case 4://ИИЯ
                    specDays.Add(new WeekDay { Id = 1, SpecName = "Английский язык" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "Немецкий язык" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "Коми язык" });
                    break;
                case 5: //ИИП
                    specDays.Add(new WeekDay { Id = 1, SpecName = "Право" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "История России" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "Экономика" });
                    break;
                case 6: //ИКИ
                    specDays.Add(new WeekDay { Id = 1, SpecName = "Рисование" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "Живопись" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "Дизайн" });
                    break;
                case 7: //ИПП
                    specDays.Add(new WeekDay { Id = 1, SpecName = "Педагогика" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "Психология" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "География" });
                    break;
                // Добавьте дополнительные случаи для других значений dayId
                default:
                    break;
            }

            // Обновляем ComboBox2
            comboBox2.DataSource = specDays;
            comboBox2.ValueMember = "Id";
            comboBox2.DisplayMember = "SpecName";
        }
        private void UpdateSelectedRow()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Обновляем значения в выбранной строке
                selectedRow.Cells[1].Value = textBox1.Text; // Обновляем значение из textBox1
                selectedRow.Cells[2].Value = textBox2.Text; // Обновляем значение из textBox2
                selectedRow.Cells[3].Value = textBox3.Text; // Обновляем значение из textBox3

                selectedRow.Cells[4].Value = ((WeekDay)comboBox1.SelectedItem).DayName; // Обновляем значение для DayName
                selectedRow.Cells[5].Value = ((WeekDay)comboBox2.SelectedItem).SpecName; // Обновляем значение для SpecName
                selectedRow.Cells[6].Value = dateTimePicker1.Value; // Обновляем значение для даты

                SaveData(); // Сохраняем данные в JSON после изменения
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
                    Regex regex = new Regex(@"^\d{3,}[а-яА-Я]?-?[а-яА-Я]{2,}$");
                    if (!regex.IsMatch(currentText))
                    {
                        MessageBox.Show("Ошибка: Введите данные по шаблону, например: 100б-ПИо; 200-ЯЗ");
                        return; 
                    }
                    UpdateSelectedRow();
                }
                else
                {
                    MessageBox.Show("Ошибка: Поле не должно быть пустым.");
                }
            }

        }
    }
}

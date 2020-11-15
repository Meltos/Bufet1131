using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Бюро_находок_и_забытых_вещей
{
    public partial class CityForm : Form
    {
        Paginator<CityDB, City> paginator;
        Viewer<City> viewer;
        CityDB dB;
        CountryDB CountryDB;
        List<string> Tags;
        public CityForm()
        {
            InitializeComponent();
            CountryDB = new CountryDB();
            viewer = new Viewer<City>();
            
            viewer.ViewerControl = listView1;
            var prop = listView1.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            prop.SetValue(listView1, true, null);
            List<string> tags = new List<string>();
            for (int i = 0; i < listView1.Columns.Count; i++)
                tags.Add(listView1.Columns[i].Tag.ToString());
            Tags = tags;
            comboBox1.DataSource = null;
            comboBox1.DataSource = CountryDB.GetList();
            comboBox1.DisplayMember = "NameCountry";
            // подписываемся на событие изменения выводимых записей
            paginator.ShowRowsChanges += Paginator_ShowRowsChanges;
            // подписываемся на изменение кол-ва страниц
            paginator.CountChanged += Paginator_CountChanged;
            // подписываемся на изменение текущего индекса
            paginator.CurrentIndexChanged += Paginator_CurrentIndexChanged;
            // вызываем обновление всех данных и событий
            // за счет того, что данный метод вызывается ПОСЛЕ создания пагинатора интерфейс успевает подписаться на события пагинатора и нормально отобразить все данные
            
            dB.Save();
        }

        private void Paginator_CurrentIndexChanged(object sender, EventArgs e)
        {
            hScrollBar1.Value = paginator.CurrentIndex;
        }

        private void Paginator_CountChanged(object sender, EventArgs e)
        {
            hScrollBar1.Maximum = paginator.MaxIndex;
            hScrollBar1.Value = paginator.CurrentIndex;
        }

        private void Paginator_ShowRowsChanges(object sender, EventArgs e)
        {
            ShowData((Country)comboBox1.SelectedItem);
        }

        private void ShowData(Country rows)
        {
            viewer.UniversalView(Tags, rows.Cities);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            paginator.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            paginator.End();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
                paginator.Right();
            else if (e.NewValue < e.OldValue)
                paginator.Left();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Country country = (Country)comboBox1.SelectedItem;
            dB = new CityDB(CountryDB, country);
            // создаем экземпляр пагинатора для отображения 10 записей на странице. Число 10 можно сделать переменной и вынести в настройки
            paginator = new Paginator<CityDB, City>(dB, 10);
            // для отображения данных в листвью я сделал отдельный класс
            // в нем кэшируются строки
            if (country.Cities.Count == 0)
            {
                dB.Save();
                return;
            }
            viewer.UniversalView(Tags, country.Cities);
            dB.Save();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCityForm addCityForm = new AddCityForm(dB.Add(), CountryDB);
            addCityForm.ShowDialog();
            dB.Save();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;
            City city = (City)listView1.SelectedItems[0].Tag;
            AddCityForm addCityForm = new AddCityForm(city, CountryDB);
            addCityForm.ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;
            City city = (City)listView1.SelectedItems[0].Tag;
            if (MessageBox.Show("Точно удалить город?", "Предупреждение!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dB.Remove(city);
            }
        }
    }
}

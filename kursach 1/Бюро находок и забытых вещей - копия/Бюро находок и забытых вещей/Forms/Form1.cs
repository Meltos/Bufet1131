using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Бюро_находок_и_забытых_вещей
{
    public partial class Form1 : Form
    {
        Paginator<AdvertisementDB, Advertisement> paginator;
        Viewer<Advertisement> viewer;
        public Form1()
        {
            InitializeComponent();

            var db = new AdvertisementDB();
            // создаем экземпляр пагинатора для отображения 10 записей на странице. Число 10 можно сделать переменной и вынести в настройки
            paginator = new Paginator<AdvertisementDB, Advertisement>(db, 10);
            // для отображения данных в листвью я сделал отдельный класс
            // в нем кэшируются строки
            viewer = new Viewer<Advertisement>();
            // подписываемся на событие изменения выводимых записей
            paginator.ShowRowsChanges += Paginator_ShowRowsChanges;
            // подписываемся на изменение кол-ва страниц
            paginator.CountChanged += Paginator_CountChanged;
            // подписываемся на изменение текущего индекса
            paginator.CurrentIndexChanged += Paginator_CurrentIndexChanged;

            // вызываем обновление всех данных и событий
            // за счет того, что данный метод вызывается ПОСЛЕ создания пагинатора интерфейс успевает подписаться на события пагинатора и нормально отобразить все данные
            db.Save();
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
            ShowAdvertisementData(paginator.ShowRows);
        }

        private void ShowAdvertisementData(List<Advertisement> rows)
        {
            
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
                paginator.Right();
            else if (e.NewValue < e.OldValue)
                paginator.Left();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            paginator.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            paginator.End();
        }

        private void добавитьСтрануToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CountryForm countryForm = new CountryForm();
            countryForm.ShowDialog();
        }

        private void добавитьГородToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CityForm cityForm = new CityForm();
            cityForm.ShowDialog();
        }

        private void добавитьКатегориюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            categoryForm.ShowDialog();
        }

        private void добавитьПодкатегориюToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

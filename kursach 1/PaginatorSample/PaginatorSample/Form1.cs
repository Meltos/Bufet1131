using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaginatorSample
{
    public partial class Form1 : Form
    {
        Paginator<DataBase, Human> paginator;
        ListViewViewer viewer;
        public Form1()
        {
            InitializeComponent();

            var db = new DataBase();
            // создаем экземпляр пагинатора для отображения 10 записей на странице. Число 10 можно сделать переменной и вынести в настройки
            paginator = new Paginator<DataBase, Human>(db, 10);
            // для отображения данных в листвью я сделал отдельный класс
            // в нем кэшируются строки
            viewer = new ListViewViewer(listView1, 2, 10);
            // подписываемся на событие изменения выводимых записей
            paginator.ShowRowsChanges += Paginator_ShowRowsChanges;
            // подписываемся на изменение кол-ва страниц
            paginator.CountChanged += Paginator_CountChanged;
            // подписываемся на изменение текущего индекса
            paginator.CurrentIndexChanged += Paginator_CurrentIndexChanged;

            // вызываем обновление всех данных и событий
            // за счет того, что данный метод вызывается ПОСЛЕ создания пагинатора интерфейс успевает подписаться на события пагинатора и нормально отобразить все данные
            db.GetTestData();
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

        void ShowHumanData(List<Human> rows)
        {
            viewer.ViewData(rows.Select(s=>(IViewRow)s));
        }

        private void Paginator_ShowRowsChanges(object sender, EventArgs e)
        {
            ShowHumanData(paginator.ShowRows);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            paginator.Start();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            paginator.End();
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
                paginator.Right();
            else if (e.NewValue < e.OldValue)
                paginator.Left();
        }
    }
}

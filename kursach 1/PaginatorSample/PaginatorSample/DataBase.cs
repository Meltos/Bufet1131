using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginatorSample
{
    public class DataBase : IDB<Human>
    {
        // коллекция с данными. В принципе, вместо нее может быть ссылка на открытый файл, в котором хранятся записи
        List<Human> humans = new List<Human>();
        // количество записей в бд
        public int Count { get { return humans.Count; } }
        // событие изменения кол-ва записей в бд, следует вызывать при добавлении и удалении записей
        public event EventHandler CountChanged;

        // тестовый набор записей. В вашей программе записи должны читаться из файла
        public void GetTestData()
        {
            for (int i = 0; i < 105; i++)
                humans.Add(new Human { FName = $"Test name {i}", LName = $"Test lastname {i}" });
            CountChanged?.Invoke(this, new EventArgs());
        }

        // метод получения ограниченного кол-ва записей для отображения на странице
        public List<Human> GetData(int start, int count)
        {
            if (Count > start + count)
                return humans.GetRange(start, count);
            else if (Count > start)
                return humans.GetRange(start, Count - start);
            else
                return new List<Human>();
        }
    }
}

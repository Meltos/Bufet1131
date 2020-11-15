using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginatorSample
{
    // интерфейс бд, используемый для пагинатора
    // можно использовать для разных бд
    public interface IDB<T>
    {
        int Count { get; }
        event EventHandler CountChanged;
        List<T> GetData(int start, int count);
    }
}

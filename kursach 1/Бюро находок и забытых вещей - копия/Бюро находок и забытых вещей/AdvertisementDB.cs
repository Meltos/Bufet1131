using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Бюро_находок_и_забытых_вещей
{
    public class AdvertisementDB : IDB<Advertisement>
    {
        List<Advertisement> advertisements = new List<Advertisement>();
        string fileName = "advertisementsdb.bin";

        public int Count { get { return advertisements.Count; } }

        public event EventHandler CountChanged;

        public AdvertisementDB()
        {
            if (!File.Exists(fileName))
                return;
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                advertisements = (List<Advertisement>)bf.Deserialize(fs);
            }
        }

        public Advertisement Add()
        {
            Advertisement advertisement = new Advertisement();
            advertisements.Add(advertisement);
            Save();
            return advertisement;
        }

        public void Edit(Advertisement edit, string name)
        {
            
        }

        public List<Advertisement> GetData(int start, int count)
        {
            if (Count > start + count)
                return advertisements.GetRange(start, count);
            else if (Count > start)
                return advertisements.GetRange(start, Count - start);
            else
                return new List<Advertisement>();
        }

        public List<Advertisement> GetList()
        {
            return advertisements;
        }

        public void Remove(Advertisement delete)
        {
            advertisements.Remove(delete);
            Save();
        }

        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                bf.Serialize(fs, advertisements);
            }
            CountChanged?.Invoke(this, new EventArgs());
        }
    }
}

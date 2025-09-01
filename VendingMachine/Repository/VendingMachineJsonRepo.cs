using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VendingMachine.Repository
{
    class VendingMachineJsonRepo : VendingMachineCollectionRepo
    {
        private readonly string _filePath = "products.json";

        public VendingMachineJsonRepo()
        {
            
            LoadFileOrWriteSeed();
        }

        private void LoadFileOrWriteSeed()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        List<Product> loaded = JsonSerializer.Deserialize<List<Product>>(json);
                        if (loaded != null)
                        {
                            
                            _shelf = loaded;
                            return;
                        }
                    }
                }
            }
            catch
            {
               
            }

            
            SaveFile();
        }

        private void SaveFile()
        {
            JsonSerializerOptions opts = new JsonSerializerOptions();
            opts.WriteIndented = true;
            string json = JsonSerializer.Serialize(_shelf, opts);
            File.WriteAllText(_filePath, json);
        }

        // Autosave ved tilføj
        public override void Add(Product product)
        {
            base.Add(product);
            SaveFile();
        }

        // Autosave ved slet
        public override void Delete(Product product)
        {
            base.Delete(product);
            SaveFile();
        }

       
        public void DeleteAllById(int id)
        {
            
            List<Product> snapshot = new List<Product>(_shelf);
            int i = 0;
            while (i < snapshot.Count)
            {
                if (snapshot[i].ID == id)
                {
                    _shelf.Remove(snapshot[i]);
                }
                i = i + 1;
            }
            SaveFile();
        }

        public void ClearAll()
        {
            _shelf.Clear();
            SaveFile();
        }
    }

}

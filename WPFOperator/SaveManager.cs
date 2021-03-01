using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WPFOperator.Models;

namespace WPFOperator
{
    class SaveManager
    {
        public LauncherKeyObject LoadKey()
        {
            LauncherKeyObject key = new LauncherKeyObject();
            try
            {
                using (Stream stream = new FileStream("keyfile.kf", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    key = (LauncherKeyObject)bf.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                System.Environment.Exit(0);
            }
            return key;
        }

        public void SaveEmployers(ObservableCollection<EmployerObject> employers)
        {
            try
            {
                using (Stream stream = new FileStream("emps.data", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, employers);
                }
            }
            catch (Exception e)
            {

            }
        }


        public ObservableCollection<EmployerObject> LoadEmployers()
        {
            ObservableCollection<EmployerObject> employers = new ObservableCollection<EmployerObject>();
            try
            {
                using (Stream stream = new FileStream("emps.data", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    employers = (ObservableCollection<EmployerObject>)bf.Deserialize(stream);
                }
            }
            catch (Exception e)
            {

            }
            
            return employers;
        }

        public List<EmployerObject> LoadEmployersArchive()
        {
            List<EmployerObject> employersArchive = new List<EmployerObject>();
            try
            {
                using (Stream stream = new FileStream("arch_employers.data", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    employersArchive = (List<EmployerObject>)bf.Deserialize(stream);
                }
            }
            catch (Exception e)
            {

            }

            return employersArchive;
        }

        public void SaveEmployersArchive(List<EmployerObject> employersArchive)
        {
            try
            {
                using (Stream stream = new FileStream("arch_employers.data", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, employersArchive);
                }
            }
            catch (Exception e)
            {

            }
        }

        public ObservableCollection<CardObject> LoadCardsArchive()
        {
            ObservableCollection<CardObject> cardsArchive = new ObservableCollection<CardObject>();
            try
            {
                using (Stream stream = new FileStream("arch_cards.data", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    cardsArchive = (ObservableCollection<CardObject>)bf.Deserialize(stream);
                }
            }
            catch (Exception e)
            {

            }

            return cardsArchive;
        }

        public void SaveCardsArchive(ObservableCollection<CardObject> cardArchive)
        {
            try
            {
                using (Stream stream = new FileStream("arch_cards.data", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, cardArchive);
                }
            }
            catch (Exception e)
            {

            }
        }

        public void SaveCardTypes(Dictionary<int, string> types)
        {
            try
            {
                using (Stream stream = new FileStream("types.data", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, types);
                }
            }
            catch (Exception e)
            {

            }
        }

        public Dictionary<int, string> LoadTypes()
        {
            Dictionary<int, string> types = new Dictionary<int, string>();
            try
            {
                using (Stream stream = new FileStream("types.data", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    types = (Dictionary<int, string>)bf.Deserialize(stream);
                }
            }
            catch (Exception e)
            {

            }
            
                
            return types;
        }
    }
}

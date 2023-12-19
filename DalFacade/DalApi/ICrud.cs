using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : class
    {
        int Create(T item); //Creates new entity object in DAL
        T? Read(int id); //Reads entity object by its ID 
        //The method will receive a pointer to a boolean function, a delegate of type Func, which will act on one of the members of the list of type T and return the first object in the list on which the function returns True.
        T? Read(Func<T, bool> filter);
        IEnumerable<T?> ReadAll(Func<T, bool>? filter = null);  // Reads all entity objects
        void Update(T item); //Updates entity object
        void Delete(int id); //Deletes an object by its Id
        void Reset();//Clear all the DB
    }

}

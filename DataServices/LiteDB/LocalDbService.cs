using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using DataServices.Interfaces;
using LiteDB;

namespace DataServices.Services
{
    public class LocalDbService : ILocalDbService
    {
        private readonly LiteDatabase Local;

        public LocalDbService(string fileName)
        {
            Local = new LiteDatabase($"filename={GetFilePath(fileName)};mode=Exclusive");
            var collections = Local.GetCollectionNames();
            foreach (var collectionName in collections)
            {
                Console.WriteLine($"*** COLLECTION: {collectionName}");
            }
        }

        private string GetFilePath(string fileName) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

        //private string GetFilePath(string fileName)
        //{
        //    return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
        //}

        public int Count<T>()
        {
            var collection = Local.GetCollection<T>();
            return collection.Count();
        }

        public int Count<T>(Expression<Func<T, bool>> query)
        {
            var collection = Local.GetCollection<T>();
            return collection.Count(query);
        }

        public IEnumerable<T> All<T>()
        {
            var collection = Local.GetCollection<T>();
            return collection.FindAll().Skip(0).Take(10);
        }

        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> query)
        {
            var collection = Local.GetCollection<T>();
            return collection.Find(query);
        }

        public void Insert<T>(T item)
        {
            var collection = Local.GetCollection<T>();
            var newId = collection.Insert(item);
        }

        public void Upsert<T>(T item)
        {
            var collection = Local.GetCollection<T>();
            collection.Upsert(item);
        }

        public void Update<T>(T item)
        {
            var collection = Local.GetCollection<T>();
            collection.Update(item);
        }

        public void Delete<T>(Expression<Func<T, bool>> query)
        {
            var collection = Local.GetCollection<T>();
            collection.DeleteMany(query);
        }

        public void Delete<T>(Guid id)
        {
            var collection = Local.GetCollection<T>();
            collection.Delete(id);
        }


        ~LocalDbService()
        {
            Local.Dispose();
        }
    }
}


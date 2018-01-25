using SQLite;
using StatisticsApp.Entities;
using StatisticsApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace StatisticsApp.Services
{
    public class DisclaimerRead
    {
        private SQLiteConnection _connection;

        public DisclaimerRead()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<DisclaimerEntity>();
        }

        public IEnumerable<DisclaimerEntity> GetIsRead() => (from tbl in _connection.Table<DisclaimerEntity>() select tbl).ToList();

        public void AddIsRead(bool isRead)
        {
            var Read = new DisclaimerEntity
            {
                IsRead = isRead
            };

            _connection.Insert(Read);
        }
    }
}

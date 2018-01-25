﻿using SQLite;
using StatisticsApp.Entities;
using StatisticsApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace StatisticsApp.Services
{
    public class FavToggle
    {
        private SQLiteConnection _connection;

        public FavToggle()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<AddToFav>();
        }

        public IEnumerable<AddToFav> GetFavourites() => (from tbl in _connection.Table<AddToFav>() select tbl).ToList();

        public void AddFav(string survey, bool isFav, string icon)
        {
            var favSurvey = new AddToFav
            {
                SurveId = survey,
                IsFavourite = isFav,
                Icon = icon
            };

            _connection.Insert(favSurvey);
        }
        public void UpdateFav(string survey, bool isFav, string icon)
        {
            var favSurvey = _connection.Table<AddToFav>().FirstOrDefault(i => i.SurveId == survey);

            favSurvey.IsFavourite = isFav;
            favSurvey.Icon = icon;

            _connection.Update(favSurvey);
        }

        public void AddAllSurveys(List<AddToFav> surveysToAdd)
        {
            var items = GetFavourites().Select(x => x.SurveId).ToList();
            var newItems = surveysToAdd.Select(x => x.SurveId).ToList();
            var addNew = newItems.Except(items).ToList();

            var itemsToAdd = new List<AddToFav>();

            if (addNew.Count() > 0)
            {
                foreach (var item in addNew)
                {
                    itemsToAdd.Add(surveysToAdd.FirstOrDefault(x => x.SurveId == item));
                }

                _connection.InsertAll(itemsToAdd);
            }
        }
    }
}

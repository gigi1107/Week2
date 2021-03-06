﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using week2.Models;
using week2.ViewModels;

namespace week2.Services
{
    public sealed class MockDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MockDataStore _instance;

        public static MockDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MockDataStore();
                }
                return _instance;
            }
        }

        private List<Item> _itemDataset = new List<Item>();
        private List<Character> _characterDataset = new List<Character>();
        //private List<Monster> _monsterDataset = new List<Monster>();
        //private List<Score> _scoreDataset = new List<Score>();

        private MockDataStore()
        {
            InitilizeSeedData();
        }

        private void InitilizeSeedData()
        {


            // Implement

            // Load Items.
            _itemDataset.Add(new Item("Fresh Carrot", "+health"));

            _itemDataset.Add(new Item("Wet Grass", "+ 20% health"));

            _itemDataset.Add(new Item("Magical Dew", "+2 pts wisdom"));

            // Implement Characters

            _characterDataset.Add(new Character 
            { Name = "Hazel", Age = 3, Description = "Leader of the Rabbits" });

            _characterDataset.Add(new Character
            { Name = "Fiver", Age = 1, Description = "Can forsee the future" } );

            _characterDataset.Add(new Character
            {
                Name = "Clover",
                Age = 2,
                Description = "Rescued Clover from a hutch"

            });

            // Implement Monsters

            // Implement Scores
        }

        private void CreateTables()
        {
            // Do nothing...
        }

        // Delete the Datbase Tables by dropping them
        public void DeleteTables()
        {
            _characterDataset.Clear();
            _itemDataset.Clear();
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            // Implement Monsters

            // Implement Characters 
            CharactersViewModel.Instance.SetNeedsRefresh(true);

            // Implement Scores
        }

        public void InitializeDatabaseNewTables()
        {
            DeleteTables();

            // make them again
            CreateTables();

            // Populate them
            InitilizeSeedData();

            // Tell View Models they need to refresh
            NotifyViewModelsOfDataChange();
        }

        #region Item
        // Item
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Item(data.Id);
            if (oldData == null)
            {
                _itemDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Item(data);
            if (UpdateResult)
            {
                await AddAsync_Item(data);
                return true;
            }

            return false;
        }

        public async Task<bool> AddAsync_Item(Item data)
        {
            _itemDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _itemDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            return await Task.FromResult(_itemDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            return await Task.FromResult(_itemDataset);
        }

        #endregion Item

       
        // Character
        public async Task<bool> AddAsync_Character(Character data)
        {
            // Check to see if the item exist
            var oldData = await GetAsync_Character(data.Id);
            if (oldData == null)
            {
                _characterDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Character(data);
            if (UpdateResult)
            {
                await AddAsync_Character(data);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _characterDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Character> GetAsync_Character(string id)
        {
            return await Task.FromResult(_characterDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            return await Task.FromResult(_characterDataset);
        }
    }
}


        //#endregion Character

        //#region Monster
        ////Monster
        //public async Task<bool> AddAsync_Monster(Monster data)
        //{
        //    // Implement
        //    return false;
        //}

        //public async Task<bool> UpdateAsync_Monster(Monster data)
        //{
        //    // Implement
        //    return false;
        //}

        //public async Task<bool> DeleteAsync_Monster(Monster data)
        //{
        //    // Implement
        //    return false;
        //}

        //public async Task<Monster> GetAsync_Monster(string id)
        //{
        //    // Implement
        //    return null;
        //}

        //public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        //{
        //    // Implement
        //    return null;
        //}

        //#endregion Monster

        //#region Score
        //// Score
        //public async Task<bool> AddAsync_Score(Score data)
        //{
        //    // Implement
        //    return false;
        //}

        //public async Task<bool> UpdateAsync_Score(Score data)
        //{
        //    // Implement
        //    return false;
        //}

        //public async Task<bool> DeleteAsync_Score(Score data)
        //{
        //    // Implement
        //    return false;
        //}

        //public async Task<Score> GetAsync_Score(string id)
        //{
        //    // Implement
        //    return null;
        //}

        //public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        //{
        //    // Implement
        //    return null;
        //}
        //#endregion Score
    
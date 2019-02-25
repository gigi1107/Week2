﻿using System;

using NUnit.Framework;

using UnitTests.Models.Default;
using Xamarin.Forms.Mocks;

using System.Linq;

using Crawl.Models;
using Crawl.ViewModels;
using Crawl.Services;
using Crawl.Views;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace UnitTests.ViewModels
{
    [TestFixture]
    class ItemsViewModelTests
    {
        #region ItemsViewModelBasics
        [Test]
        public void ViewModel_ItemsViewModel_Instantiate_Should_Pass()
        {

            MockForms.Init();

            var Actual = new ItemsViewModel();

            // Validate the controller can stand up and has a Title
            Assert.AreEqual("Item List", Actual.Title, TestContext.CurrentContext.Test.Name);
        }

        #endregion ItemsViewModelBasics

        #region DataOperations
        [Test]
        public async Task ViewModel_ItemsViewModel_AddData_Should_Pass()
        {

            MockForms.Init();

            // Get State of the DataStore, and set to run on the Mock
            var myDataStoreEnum = MasterDataStore.GetDataStoreMockFlag();
            MasterDataStore.ToggleDataStore(DataStoreEnum.Mock);

            var myViewModel = new ItemsViewModel();
            var myData = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);
            var myReturn = await myViewModel.AddAsync(myData);

            var Actual = await myViewModel.GetAsync(myData.Id);
            var Expected = myData;

            // Return state
            MasterDataStore.ToggleDataStore(myDataStoreEnum);

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected.Id, Actual.Id, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public async Task ViewModel_ItemsViewModel_DeleteData_Should_Pass()
        {

            MockForms.Init();

            // Get State of the DataStore, and set to run on the Mock
            var myDataStoreEnum = MasterDataStore.GetDataStoreMockFlag();
            MasterDataStore.ToggleDataStore(DataStoreEnum.Mock);

            var myViewModel = new ItemsViewModel();
            var myData = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);
            await myViewModel.AddAsync(myData);

            var myReturn = await myViewModel.DeleteAsync(myData);

            var Actual = await myViewModel.GetAsync(myData.Id);
            Object Expected = null;

            // Return state
            MasterDataStore.ToggleDataStore(myDataStoreEnum);

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public async Task ViewModel_ItemsViewModel_UpdateData_Should_Pass()
        {

            MockForms.Init();

            // Get State of the DataStore, and set to run on the Mock
            var myDataStoreEnum = MasterDataStore.GetDataStoreMockFlag();
            MasterDataStore.ToggleDataStore(DataStoreEnum.Mock);

            var myViewModel = new ItemsViewModel();
            var myData = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);
            await myViewModel.AddAsync(myData);

            var value = "new";

            myData.Name = value;
            var myReturn = myViewModel.UpdateAsync(myData);

            var Actual = await myViewModel.GetAsync(myData.Id);
            string Expected = value;

            // Return state
            MasterDataStore.ToggleDataStore(myDataStoreEnum);

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual.Name, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public async Task ViewModel_ItemsViewModel_UpdateData_Bogus_Should_Skip()
        {

            MockForms.Init();

            // Get State of the DataStore, and set to run on the Mock
            var myDataStoreEnum = MasterDataStore.GetDataStoreMockFlag();
            MasterDataStore.ToggleDataStore(DataStoreEnum.Mock);

            var myViewModel = new ItemsViewModel();
            myViewModel.ForceDataRefresh();

            var myData = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);

            // Make the ID bogus...
            var value = "new";
            myData.Id = value;

            var myReturn = await myViewModel.UpdateAsync(myData);

            var Actual = myReturn;
            bool Expected = false;

            // Return state
            MasterDataStore.ToggleDataStore(myDataStoreEnum);

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public async Task ViewModel_ItemsViewModel_InsertUpdateAsync_New_Data_Should_Pass()
        {

            MockForms.Init();

            // Get State of the DataStore, and set to run on the Mock
            var myDataStoreEnum = MasterDataStore.GetDataStoreMockFlag();
            MasterDataStore.ToggleDataStore(DataStoreEnum.Mock);

            var myViewModel = new ItemsViewModel();

            // New Item
            var myData = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);

            var myReturn = await myViewModel.InsertUpdateAsync(myData);

            var Actual = await myViewModel.GetAsync(myData.Id);
            var Expected = myData.Name;

            // Return state
            MasterDataStore.ToggleDataStore(myDataStoreEnum);

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual.Name, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public async Task ViewModel_ItemsViewModel_InsertUpdateAsync_Update_Data_Should_Pass()
        {

            MockForms.Init();

            // Get State of the DataStore, and set to run on the Mock
            var myDataStoreEnum = MasterDataStore.GetDataStoreMockFlag();
            MasterDataStore.ToggleDataStore(DataStoreEnum.Mock);

            var myViewModel = new ItemsViewModel();
            var myData = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);
            await myViewModel.AddAsync(myData);

            // Update existing
            var Value = "updated";
            myData.Name = Value;
            var myReturn = await myViewModel.InsertUpdateAsync(myData);

            var Actual = await myViewModel.GetAsync(myData.Id);
            string Expected = Value;

            // Return state
            MasterDataStore.ToggleDataStore(myDataStoreEnum);

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual.Name, TestContext.CurrentContext.Test.Name);
        }


        #endregion DataOperations

        #region MessageCenter
        [Test]
        public async Task ViewModel_MessageCenter_ItemsViewModel_MessageCenter_AddData_Should_Pass()
        {

            MockForms.Init();

            // Get State of the DataStore, and set to run on the Mock
            var myDataStoreEnum = MasterDataStore.GetDataStoreMockFlag();
            MasterDataStore.ToggleDataStore(DataStoreEnum.Mock);

            var myViewModel = new ItemsViewModel();
            var myData = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);

            var myPage = new ItemNewPage();
            MessagingCenter.Send(myPage, "AddData", myData);

            var Actual = await myViewModel.GetAsync(myData.Id);
            var Expected = myData;

            // Return state
            MasterDataStore.ToggleDataStore(myDataStoreEnum);

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected.Id, Actual.Id, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public async Task ViewModel_MessageCenter_ItemsViewModel_MessageCenter_DeleteData_Should_Pass()
        {

            MockForms.Init();

            // Get State of the DataStore, and set to run on the Mock
            var myDataStoreEnum = MasterDataStore.GetDataStoreMockFlag();
            MasterDataStore.ToggleDataStore(DataStoreEnum.Mock);

            var myViewModel = new ItemsViewModel();
            var myData = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);
            await myViewModel.AddAsync(myData);

            var myPage = new ItemDeletePage(new ItemDetailViewModel(new Item()));
            MessagingCenter.Send(myPage, "DeleteData", myData);

            var Actual = await myViewModel.GetAsync(myData.Id);
            Object Expected = null;

            // Return state
            MasterDataStore.ToggleDataStore(myDataStoreEnum);

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public async Task ViewModel_MessageCenter_ItemsViewModel_MessageCenter_UpdateData_Should_Pass()
        {

            MockForms.Init();

            // Get State of the DataStore, and set to run on the Mock
            var myDataStoreEnum = MasterDataStore.GetDataStoreMockFlag();
            MasterDataStore.ToggleDataStore(DataStoreEnum.Mock);

            var myViewModel = new ItemsViewModel();
            var myData = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);
            await myViewModel.AddAsync(myData);

            var value = "new";

            myData.Name = value;

            var myPage = new ItemEditPage(new ItemDetailViewModel(new Item()));
            MessagingCenter.Send(myPage, "EditData", myData);

            var Actual = await myViewModel.GetAsync(myData.Id);
            string Expected = value;

            // Return state
            MasterDataStore.ToggleDataStore(myDataStoreEnum);

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual.Name, TestContext.CurrentContext.Test.Name);
        }

        #endregion MessageCenter

        #region LoadRefesh
        [Test]
        public void ViewModel_ItemsViewModel_SetNeedsRefresh_False_Should_Be_False()
        {

            MockForms.Init();

            var myData = new ItemsViewModel();

            myData.SetNeedsRefresh(false);

            var Actual = myData.NeedsRefresh();
            var Expected = false;

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ViewModel_ItemsViewModel_SetNeedsRefresh_True_Should_Be_True()
        {

            MockForms.Init();

            var myData = new ItemsViewModel();

            myData.SetNeedsRefresh(true);

            var Actual = myData.NeedsRefresh();
            var Expected = true;

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ViewModel_ItemsViewModel_DataSet_Should_Be_Valid()
        {

            MockForms.Init();

            var myData = new ItemsViewModel();
            var myLoad = myData.LoadDataCommand;

            var Actual = myData.Dataset;
            var Expected = myData.Dataset;

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ViewModel_ItemsViewModel_LoadDataCommand_Should_Pass()
        {

            MockForms.Init();

            var myData = new ItemsViewModel();
            myData.ForceDataRefresh();

            var Actual = myData.Dataset.Count();
            var Expected = myData.DataStore.GetAllAsync_Item().GetAwaiter().GetResult().Count();

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ViewModel_ItemsViewModel_LoadDataCommand_With_IsBusy_Should_Skip()
        {

            MockForms.Init();

            var myData = new ItemsViewModel();
            var myIsBusy = myData.IsBusy;
            myData.ForceDataRefresh();

            myData.Dataset.Clear();

            myData.IsBusy = true;

            var canExecute = myData.LoadDataCommand.CanExecute(null);
            myData.LoadDataCommand.Execute(null);

            var Actual = myData.Dataset.Count();
            var Expected = 0;

            // set it back...
            myData.IsBusy = myIsBusy;
            myData.ForceDataRefresh();

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ViewModel_ItemsViewModel_LoadCommand_With_Bogus_DataSource_Should_Throw_Skip()
        {

            MockForms.Init();

            var myData = new ItemsViewModel();
            var myIsBusy = myData.IsBusy;

            // Make the data store null, this will fire the Exception, which then skips...
            myData.DataStore = null;
            myData.ForceDataRefresh();

            var Actual = myData.Dataset.Count();
            var Expected = 0;

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        #endregion LoadRefesh

        #region ChooseRandomItemString
        [Test]
        public void ViewModel_ItemsViewModel_ChooseRandomItemString_With_Unknown_Location_Should_Skip()
        {
            MockForms.Init();
            var myData = new ItemsViewModel();

            // Load Data
            myData.ForceDataRefresh();

            var Actual = myData.ChooseRandomItemString(ItemLocationEnum.Unknown,AttributeEnum.Unknown);
            string Expected = null;

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ViewModel_ItemsViewModel_ChooseRandomItemString_With_No_Items_Data_Should_Skip()
        {

            MockForms.Init();

            var myData = new ItemsViewModel();

            // Load Data
            myData.ForceDataRefresh();
            myData.Dataset.Clear();

            var Actual = myData.ChooseRandomItemString(ItemLocationEnum.Feet, AttributeEnum.Unknown);
            string Expected = null;

            //Reset data
            myData.ForceDataRefresh();

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ViewModel_ItemsViewModel_ChooseRandomItemString_With_Unknown_Attribute_Should_Return_Item()
        {
            MockForms.Init();

            var myData = new ItemsViewModel();

            // Load Data
            myData.ForceDataRefresh();
            myData.Dataset.Clear();

            // Make an item for the feet
            var myItem = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);
            myData.AddAsync(myItem).GetAwaiter().GetResult();

            // Ask for Any Item for the feet, don't care about the attribute
            var Actual = myData.ChooseRandomItemString(ItemLocationEnum.Feet, AttributeEnum.Unknown);
            string Expected = myItem.Guid;

            // Load Data
            myData.ForceDataRefresh();

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ViewModel_ItemsViewModel_ChooseRandomItemString_With_Known_Attribute_Should_Return_Item()
        {
            MockForms.Init();

            var myData = new ItemsViewModel();

            // Load Data
            myData.ForceDataRefresh();
            myData.Dataset.Clear();

            // Make an item for the feet
            var myItem = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);
            myData.AddAsync(myItem).GetAwaiter().GetResult();

            // Add item for speed
            myItem = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Speed);
            myData.AddAsync(myItem).GetAwaiter().GetResult();

            // Ask for Any Item for the feet, and speed
            var Actual = myData.ChooseRandomItemString(ItemLocationEnum.Feet, AttributeEnum.Speed);
            string Expected = myItem.Guid;

            // Load Data
            myData.ForceDataRefresh();

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ViewModel_ItemsViewModel_ChooseRandomItemString_With_No_Match_Should_Return_Null()
        {
            MockForms.Init();

            var myData = new ItemsViewModel();

            // Load Data
            myData.ForceDataRefresh();
            myData.Dataset.Clear();

            // Make an item for the feet
            var myItem = DefaultModels.ItemDefault(ItemLocationEnum.Feet, AttributeEnum.Attack);
            myData.AddAsync(myItem).GetAwaiter().GetResult();

            // Ask for Any Item for the head
            var Actual = myData.ChooseRandomItemString(ItemLocationEnum.Head, AttributeEnum.Unknown);
            string Expected = null;

            // Load Data
            myData.ForceDataRefresh();

            // Validate the controller can stand up and has a Title
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }
        #endregion ChooseRandomItemString
    }
}




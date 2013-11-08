﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using DataCache;
using LocalTypes;
using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Mail;

namespace DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for PersonAccessTest and is intended
    ///to contain all PersonAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PersonAccessTest
    {

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            for (int i = 2; i <= 15; i++)
            {
                if (!Cache.CacheData.t_people.Any(person => person.C_id == i))
                {
                    var newPerson = t_people.Createt_people(i);
                    newPerson.address = "12;"+ i + i +";main st;anywhere;anystate;usa;12345";
                    newPerson.email = i + "@something.somewhere";
                    newPerson.family_name = "Doe";
                    newPerson.given_name = "Jack/Jane";
                    Cache.CacheData.t_people.AddObject(newPerson);
                } 
            }
            
            #region Account info
            for (int newAccountIndex = 200; newAccountIndex <= 210; newAccountIndex++)
            {
                if (!Cache.CacheData.t_accounts.Any(acc => acc.C_id == newAccountIndex))
                {
                    int accountOwner = 2;
                    var newAccount = t_accounts.Createt_accounts(newAccountIndex, accountOwner);
                    newAccount.last_month_paid = DateTime.Today;
                    newAccount.monthly_total = 0;
                    Cache.CacheData.t_accounts.AddObject(newAccount);
                    accountOwner++; 
                }
            }
            for (int newDonationIndex = 1010; newDonationIndex <= 1015; newDonationIndex++)
            {
                if (!Cache.CacheData.t_donations.Any(donation => donation.C_id == newDonationIndex))
                {
                    var newDonation = t_donations.Createt_donations(
                                newDonationIndex, 200, "reason:" + newDonationIndex, 12.5, DateTime.Today, false);
                    Cache.CacheData.t_donations.AddObject(newDonation);
                }
            }
            for (int newDonationIndex = 1016; newDonationIndex <= 1020; newDonationIndex++)
            {
                if (!Cache.CacheData.t_donations.Any(donation => donation.C_id == newDonationIndex))
                {
                    var newDonation = t_donations.Createt_donations(
                               newDonationIndex, 200, "reason:" + newDonationIndex, 12.5, DateTime.Today, true);
                    newDonation.date_paid = DateTime.Today;
                    Cache.CacheData.t_donations.AddObject(newDonation);
                }
            } 
            #endregion

            #region Number info
            if (!Cache.CacheData.t_phone_types.Any(numberType => numberType.C_id == 1))
            {
                Cache.CacheData.t_phone_types.AddObject(t_phone_types.Createt_phone_types(1, "phonetype:1"));
            }
            for (int newPhoneNumberIndex = 100; newPhoneNumberIndex <= 110; newPhoneNumberIndex++)
            {
                if (!Cache.CacheData.t_phone_numbers.Any(number => number.C_id == newPhoneNumberIndex))
                {
                    var newPhoneNumber = t_phone_numbers.Createt_phone_numbers(
                                2, "phone number:" + newPhoneNumberIndex.ToString(), 1, newPhoneNumberIndex);
                    Cache.CacheData.t_phone_numbers.AddObject(newPhoneNumber); 
                }
            }
            #endregion
            
            #region Yahrtzieht info
            for (int i = 200; i < 211; i++)
            {
                if (!Cache.CacheData.t_yahrtziehts.Any(yahr => yahr.C_id == i))
                {
                    var newYahrtzieht = t_yahrtziehts.Createt_yahrtziehts(i, 2, DateTime.Today, "ploni ben almoni");
                    newYahrtzieht.relation = "they where not";
                    Cache.CacheData.t_yahrtziehts.AddObject(newYahrtzieht); 
                }
            }
            #endregion

            Cache.CacheData.SaveChanges();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            var donations = (from donate in Cache.CacheData.t_donations select donate).ToList<t_donations>();
            var accounts = (from account in Cache.CacheData.t_accounts select account).ToList<t_accounts>();
            var phoneNumbers = (from number in Cache.CacheData.t_phone_numbers select number).ToList<t_phone_numbers>();
            var phoneTypes = (from type in Cache.CacheData.t_phone_types select type).ToList<t_phone_types>();
            var yahrtziehts = (from yahr in Cache.CacheData.t_yahrtziehts select yahr).ToList<t_yahrtziehts>();
            var peoples = (from person in Cache.CacheData.t_people select person).ToList<t_people>();
            for (int i = 0; i < donations.Count; i++)
            {
                Cache.CacheData.t_donations.DeleteObject(donations[i]);
            }
            for (int i = 0; i < accounts.Count; i++)
            {
                Cache.CacheData.t_accounts.DeleteObject(accounts[i]);
            }
            for (int i = 0; i < phoneNumbers.Count; i++)
            {
                Cache.CacheData.t_phone_numbers.DeleteObject(phoneNumbers[i]);
            }
            for (int i = 0; i < phoneTypes.Count; i++)
            {
                Cache.CacheData.t_phone_types.DeleteObject(phoneTypes[i]);
            }
            for (int i = 0; i < yahrtziehts.Count; i++)
            {
                Cache.CacheData.t_yahrtziehts.DeleteObject(yahrtziehts[i]);
            }
            for (int i = 0; i < peoples.Count; i++)
            {
                Cache.CacheData.t_people.DeleteObject(peoples[i]);
            }
            Cache.CacheData.SaveChanges();
        }
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        #region Add Tests

        /// <summary>
        ///A test for AddMultipleNewPersons
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewPersonsTest()
        {
            List<Person> newPersonList = null; // TODO: Initialize to an appropriate value
            PersonAccess.AddMultipleNewPersons(newPersonList);
        }

        /// <summary>
        ///A test for AddNewPerson
        ///</summary>
        [TestMethod()]
        public void AddNewPersonTest()
        {
            Person newPerson = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PersonAccess.AddNewPerson(newPerson);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Convert Tests

        /// <summary>
        ///A test for ConvertMultipleDbPersonsToLocalType
        ///</summary>
        [TestMethod()]
        public void ConvertMultipleDbPersonsToLocalTypeTest()
        {
            List<t_people> dbTypePersonList = null; // TODO: Initialize to an appropriate value
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.ConvertMultipleDbPersonsToLocalType(dbTypePersonList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertMultipleLocalPersonsToDbType
        ///</summary>
        [TestMethod()]
        public void ConvertMultipleLocalPersonsToDbTypeTest()
        {
            List<Person> localTypePersonList = null; // TODO: Initialize to an appropriate value
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess.ConvertMultipleLocalPersonsToDbType(localTypePersonList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleDbPersonToLocalType
        ///</summary>
        [TestMethod()]
        public void ConvertSingleDbPersonToLocalTypeTest()
        {
            t_people dbTypePerson = null; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.ConvertSingleDbPersonToLocalType(dbTypePerson);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleLocalPersonToDbType
        ///</summary>
        [TestMethod()]
        public void ConvertSingleLocalPersonToDbTypeTest()
        {
            Person localTypePerson = null; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess.ConvertSingleLocalPersonToDbType(localTypePerson);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Delete Tests

        /// <summary>
        ///A test for DeleteMultiplePersons
        ///</summary>
        [TestMethod()]
        public void DeleteMultiplePersonsTest()
        {
            List<Person> deletedPersonList = null; // TODO: Initialize to an appropriate value
            PersonAccess.DeleteMultiplePersons(deletedPersonList);
        }

        /// <summary>
        ///A test for DeleteSinglePerson
        ///</summary>
        [TestMethod()]
        public void DeleteSinglePersonTest()
        {
            Person deletedPerson = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PersonAccess.DeleteSinglePerson(deletedPerson);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DeleteSinglePerson
        ///</summary>
        [TestMethod()]
        public void DeleteSingleNonExistintPersonTest()
        {
            Person deletedPerson = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PersonAccess.DeleteSinglePerson(deletedPerson);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Get Tests

        /// <summary>
        ///A test for GetAllPeople
        ///</summary>
        [TestMethod()]
        public void GetAllPeopleTest()
        {
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.GetAllPeople();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByAccount
        ///</summary>
        [TestMethod()]
        public void GetByAccountTest()
        {
            Account accountSearchedBy = null; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetByAccount(accountSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByAccount
        ///</summary>
        [TestMethod()]
        public void GetByNonExsitentAccountTest()
        {
            Account accountSearchedBy = null; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetByAccount(accountSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByAddress
        ///</summary>
        [TestMethod()]
        public void GetByAddressTest()
        {
            StreetAddress addressSearchedBy = null; // TODO: Initialize to an appropriate value
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.GetByAddress(addressSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByAddress
        ///</summary>
        [TestMethod()]
        public void GetByNonExsitentAddressTest()
        {
            StreetAddress addressSearchedBy = null; // TODO: Initialize to an appropriate value
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.GetByAddress(addressSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByEmail
        ///</summary>
        [TestMethod()]
        public void GetByEmailTest()
        {
            MailAddress email = null; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByEmail
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentEmailTest()
        {
            MailAddress email = null; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetById
        ///</summary>
        [TestMethod()]
        public void GetByIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetById
        ///</summary>
        [TestMethod()]
        public void GetByNonExsitentIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByName
        ///</summary>
        [TestMethod()]
        public void GetByNameTest()
        {
            string fullName = string.Empty; // TODO: Initialize to an appropriate value
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.GetByName(fullName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByName
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentNameTest()
        {
            string fullName = string.Empty; // TODO: Initialize to an appropriate value
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.GetByName(fullName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPhoneNumber
        ///</summary>
        [TestMethod()]
        public void GetByPhoneNumberTest()
        {
            PhoneNumber numberSearchedBy = null; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetByPhoneNumber(numberSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPhoneNumber
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentPhoneNumberTest()
        {
            PhoneNumber numberSearchedBy = null; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetByPhoneNumber(numberSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByYahrtzieht
        ///</summary>
        [TestMethod()]
        public void GetByYahrtziehtTest()
        {
            Yahrtzieht yahrtziehtSearchedBy = null; // TODO: Initialize to an appropriate value
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.GetByYahrtzieht(yahrtziehtSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByYahrtzieht
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentYahrtziehtTest()
        {
            Yahrtzieht yahrtziehtSearchedBy = null; // TODO: Initialize to an appropriate value
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.GetByYahrtzieht(yahrtziehtSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Lookup Tests

        /// <summary>
        ///A test for LookupAllPeople
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllPeopleTest()
        {
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess_Accessor.LookupAllPeople();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByAccount
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByAccountTest()
        {
            int accountId = 0; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupByAccount(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByNoNExsistentAccount
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentAccountTest()
        {
            int accountId = 0; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupByAccount(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByAddress
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByAddressTest()
        {
            string address = string.Empty; // TODO: Initialize to an appropriate value
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess_Accessor.LookupByAddress(address);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByAddress
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentAddressTest()
        {
            string address = string.Empty; // TODO: Initialize to an appropriate value
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess_Accessor.LookupByAddress(address);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByEmail
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByEmailTest()
        {
            string email = string.Empty; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByEmail
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentEmailTest()
        {
            string email = string.Empty; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNameTest()
        {
            string fullName = string.Empty; // TODO: Initialize to an appropriate value
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess_Accessor.LookupByName(fullName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentNameTest()
        {
            string fullName = string.Empty; // TODO: Initialize to an appropriate value
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess_Accessor.LookupByName(fullName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPhoneNumber
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByPhoneNumberTest()
        {
            string numberSearchedBy = string.Empty; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupByPhoneNumber(numberSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPhoneNumber
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentPhoneNumberTest()
        {
            string numberSearchedBy = string.Empty; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupByPhoneNumber(numberSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByYahrtzieht
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByYahrtziehtTest()
        {
            string nameOfDeceased = string.Empty; // TODO: Initialize to an appropriate value
            string relationToDeceased = string.Empty; // TODO: Initialize to an appropriate value
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess_Accessor.LookupByYahrtzieht(nameOfDeceased, relationToDeceased);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByYahrtzieht
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentYahrtziehtTest()
        {
            string nameOfDeceased = string.Empty; // TODO: Initialize to an appropriate value
            string relationToDeceased = string.Empty; // TODO: Initialize to an appropriate value
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess_Accessor.LookupByYahrtzieht(nameOfDeceased, relationToDeceased);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Update Tests

        /// <summary>
        ///A test for UpdateMultiplePersons
        ///</summary>
        [TestMethod()]
        public void UpdateMultiplePersonsTest()
        {
            List<Person> updatedPersonList = null; // TODO: Initialize to an appropriate value
            PersonAccess.UpdateMultiplePersons(updatedPersonList);
        }

        /// <summary>
        ///A test for UpdateSinglePerson
        ///</summary>
        [TestMethod()]
        public void UpdateSinglePersonTest()
        {
            Person updatedPerson = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PersonAccess.UpdateSinglePerson(updatedPerson);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpdateSinglePerson
        ///</summary>
        [TestMethod()]
        public void UpdateSingleNoNExsistentPersonTest()
        {
            Person updatedPerson = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PersonAccess.UpdateSinglePerson(updatedPerson);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Upsert Tests

        /// <summary>
        ///A test for UpsertSinglePerson
        ///</summary>
        [TestMethod()]
        public void UpsertAddSinglePersonTest()
        {
            Person upsertedPerson = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PersonAccess.UpsertSinglePerson(upsertedPerson);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpsertSinglePerson
        ///</summary>
        [TestMethod()]
        public void UpsertUpdateSinglePersonTest()
        {
            Person upsertedPerson = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PersonAccess.UpsertSinglePerson(upsertedPerson);
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
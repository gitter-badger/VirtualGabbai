﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using DataTypes;

namespace DataAccess
{
    public static class PhoneNumberAccess
    {
        #region Read Methods

        #region Local type return

        public static PhoneNumber GetPhoneNumberByType(PhoneType searchedType)
        {
            return null;
            //return this.ConvertSingleDbPhoneNumberToLocalType(this.LookupPhoneTypeByTypeName(typeName));
        }

        public static PhoneNumber GetPhoneNumberById(int id)
        {
            return null;
            //return this.ConvertSingleDbPhoneNumberToLocalType(this.LookupPhoneNumberById(id));
        }

        public static List<PhoneNumber> GetAllPhoneNumbers(int personId)
        {
            return null;
            //return this.ConvertMultipleDbPhoneNumbersToLocalType(this.LookupAllPhoneTypes());
        }

        public static PhoneNumber GetSpecificPhoneNumber(string phoneNumber)
        {
            return null;
        }

        #endregion

        #region Db type return

        private static t_phone_numbers LookupPhoneNumberByType(PhoneType searchedType)
        {
            return null;
        }

        private static t_phone_numbers LookupSpecificPhoneNumber(string phoneNumber)
        {
            return null;
            //return (from CurrPhoneType in Cache.CacheData.t_phone_types
            //        where CurrPhoneType.type_name == typeName
            //        select CurrPhoneType).First();
        }

        private static List<t_phone_numbers> LookupAllPhoneNumbers(int personId)
        {
            return null;
            //return (from CurrPhoneType in Cache.CacheData.t_phone_numbers
            //        select CurrPhoneType).ToList<t_phone_numbers>();
        }

        private static t_phone_numbers LookupPhoneNumberById(int id)
        {
            return null;
            //return (from CurrPhoneType in Cache.CacheData.t_phone_numbers
            //        where CurrPhoneType.C_id == id
            //        select CurrPhoneType).First();
        }

        #endregion

        #endregion

        #region Write

        #region Create

        public static void AddNewPhoneNumber(PhoneNumber newPhoneNumber)
        {
            //t_phone_types phonrTypeToAdd = this.ConvertSingleLocalPhoneNumberToDbType(newPhoneNumber);
            //Cache.CacheData.t_phone_types.AddObject(phonrTypeToAdd);
            //Cache.CacheData.SaveChanges();
        }

        public static void AddMultipleNewPhoneTypes(List<PhoneNumber> newPhoneNumberList)
        {
            foreach (PhoneNumber newPhoneNumber in newPhoneNumberList)
            {
                PhoneNumberAccess.AddNewPhoneNumber(newPhoneNumber);
            }
        }

        #endregion

        #region Update

        public static void UpdateSinglePhoneNumber(PhoneNumber updatedPhoneNumber)
        {
            //t_phone_types phoneTypeUpdating = this.LookupPhoneTypById(updatedPhoneNumber._Id);
            //phoneTypeUpdating = this.ConvertSingleLocalPhoneNumberToDbType(updatedPhoneNumber);
            //Cache.CacheData.t_phone_types.ApplyCurrentValues(phoneTypeUpdating);
            //Cache.CacheData.SaveChanges();
        }

        public static void UpdateMultiplePhoneNumbers(List<PhoneNumber> updatedPhoneNumberList)
        {
            foreach (PhoneNumber updatedPhoneNumber in updatedPhoneNumberList)
            {
                PhoneNumberAccess.UpdateSinglePhoneNumber(updatedPhoneNumber);
            }
        }

        #endregion

        #region Delete

        public static void DeleteSinglePhoneNumber(PhoneNumber deletedPhoneNumber)
        {
            //t_phone_types phoneTypeDeleting =
            //    Cache.CacheData.t_phone_types.First(phoneType => phoneType.C_id == deletedPhoneNumber._Id);
            //Cache.CacheData.t_phone_types.DeleteObject(phoneTypeDeleting);
            //Cache.CacheData.SaveChanges();
        }

        public static void DeleteMultiplePhoneNumbers(List<PhoneNumber> deletedPhoneNumberList)
        {
            foreach (PhoneNumber deletedPhoneNumber in deletedPhoneNumberList)
            {
                PhoneNumberAccess.DeleteSinglePhoneNumber(deletedPhoneNumber);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private static List<t_phone_numbers> ConvertMultipleLocalPhoneNumbersToDbType(List<PhoneNumber> localTypePhoneNumberList)
        {
            return null;
            //List<t_phone_numbers> dbTypePhoneNumberList = new List<t_phone_numbers>();

            //foreach (PhoneNumber CurrPhoneNumber in localTypePhoneNumberList)
            //{
            //    dbTypePhoneNumberList.Add(this.ConvertSingleLocalPhoneNumberToDbType(CurrPhoneNumber));
            //}

            //return dbTypePhoneNumberList;
        }

        private static t_phone_numbers ConvertSingleLocalPhoneNumberToDbType(PhoneNumber localTypePhoneNumber)
        {
            return null;
            //return t_phone_numbers.Createt_phone_types(localTypePhoneType._Id, localTypePhoneType.PhoneTypeName);
        }

        private static List<PhoneNumber> ConvertMultipleDbPhoneNumbersToLocalType(List<t_phone_numbers> dbTypePhoneNumberList)
        {
            return null;
            //List<PhoneNumber> localTypePhoneTypeList = new List<PhoneNumber>();

            //foreach (t_phone_numbers CurrPhoneNumber in dbTypePhoneNumberList)
            //{
            //    localTypePhoneTypeList.Add(this.ConvertSingleDbPhoneNumberToLocalType(CurrPhoneNumber));
            //}

            //return localTypePhoneTypeList;
        }

        private static PhoneNumber ConvertSingleDbPhoneNumberToLocalType(t_phone_numbers dbTypePhoneNumber)
        {
            return null;
            //return new PhoneType(dbTypePhoneType.C_id, dbTypePhoneType.type_name);
        }

        #endregion
    }
}

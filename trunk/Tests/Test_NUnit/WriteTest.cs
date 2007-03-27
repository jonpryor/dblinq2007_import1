﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Query;
using System.Xml.XLinq;
using System.Data.DLinq;
using NUnit.Framework;
#if ORACLE
using ClientCodeOra;
using xint = System.Int32;
#elif POSTGRES
using Client2.user;
using xint = System.Int32;
#else
using MySql.Data.MySqlClient;
using Client2.user;
using xint = System.UInt32;
#endif

namespace Test_NUnit
{
    [TestFixture]
    public class WriteTest
    {
#if ORACLE
        const string connStr = "server=localhost;user id=system; password=linq2";
#else
        const string connStr = "server=localhost;user id=LinqUser; password=linq2; database=LinqTestDB";
#endif
        
        //MySqlConnection _conn;
        //public MySqlConnection Conn 
        //{ 
        //    get 
        //    { 
        //        if(_conn==null){ _conn=new MySqlConnection(connStr); _conn.Open(); }
        //        return _conn;
        //    }
        //}

        #region Tests 'E' test live object cache
        [Test]
        public void E1_LiveObjectsAreUnqiue()
        {
            //grab an object twice, make sure we get the same object each time
            LinqTestDB db = new LinqTestDB(connStr);
            var q = from p in db.Products select p;
            Product pen1 = q.First();
            Product pen2 = q.First();
            string uniqueStr = "Unique"+Environment.TickCount;
            pen1.QuantityPerUnit = uniqueStr;
            bool isSameObject1 = pen2.QuantityPerUnit==uniqueStr;
            Assert.IsTrue(isSameObject1,"Expected pen1 and pen2 to be the same live object, but their fields are different");
            object oPen1 = pen1;
            object oPen2 = pen2;
            bool isSameObject2 = oPen1==oPen2;
            Assert.IsTrue(isSameObject2,"Expected pen1 and pen2 to be the same live object, but their fields are different");
        }
        #endregion


        #region Tests 'G' do insertion
        private xint insertProduct_priv()
        {
            LinqTestDB db = new LinqTestDB(connStr);
            Product newProd = new Product();
            newProd.CategoryID = 1;
            newProd.ProductName = "Temp."+Environment.TickCount;
            newProd.QuantityPerUnit = "33 1/2";
            db.Products.Add(newProd);
            db.SubmitChanges();
            Assert.Greater(newProd.ProductID,0,"After insertion, ProductID should be non-zero");
            Assert.IsFalse(newProd.IsModified,"After insertion, Product.IsModified should be false");
            return newProd.ProductID; //this test cab be used from delete tests
        }

        [Test]
        public void G1_InsertProduct()
        {
            insertProduct_priv();
        }

        [Test]
        public void G2_DeleteTest()
        {
            xint insertedID = insertProduct_priv();
            Assert.Greater(insertedID,0,"DeleteTest cannot operate if row was not inserted");
            
            LinqTestDB db = new LinqTestDB(connStr);
            var q = from p in db.Products where p.ProductID==insertedID select p;
            List<Product> insertedProducts = q.ToList();
            foreach(Product insertedProd in insertedProducts)
            {
                db.Products.Remove(insertedProd);
            }
            db.SubmitChanges();

            int numLeft = (from p in db.Products where p.ProductID==insertedID select p).Count();
            Assert.AreEqual(numLeft,0, "After deletion, expected count of Products with ID="+insertedID+" to be zero, instead got "+numLeft);
        }

        #endregion

    }
}

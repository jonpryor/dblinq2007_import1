#region HEADER
using System;
using System.Collections.Generic;
using System.Text;
using System.Query;
#endregion

namespace Test_NUnit
{
#region HEADER
    /// <summary>
    /// when a problem crops up in NUnit, you can convert the project from DLL into EXE, 
    /// and debug into the offending method.
    /// </summary>
#endregion
    class Program2
    {
        static void Main()
        {
            //new ReadTest().C3_SelectPenIdName();
            new ReadTest_GroupBy().G01_SimpleGroup();
            //new ReadTest_GroupBy().G04_OrderSumByCustomerID();
            //new ReadTest_Complex().F1_ProductCount();
            //new ReadTest().D10_Products_LetterP_Desc();
            //new ReadTest().D7_OrdersFromLondon_Alt();
            //new WriteTest().G2_DeleteTest();
            //new WriteTest().G1_InsertProduct();
        }
    }
    //class Column { public string table_name; }
    //class Table { public string table_name; }
}

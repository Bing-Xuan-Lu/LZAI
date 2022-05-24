using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LZAI.Service.Service;
using LZAI.Model.QueryFillter;
using LZAI.Service.LZGPS;
using Newtonsoft.Json;

namespace LZAIMonitor.Tests.Service
{
    /// <summary>
    /// TestLZGPSService 的摘要說明
    /// </summary>
    [TestClass]
    public class TestLZGPSService
    {
        public TestLZGPSService()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///取得或設定提供目前測試回合
        ///相關資訊與功能的測試內容。
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

        #region 其他測試屬性
        //
        // 您可以使用下列其他屬性撰寫測試: 
        //
        // 執行該類別中第一項測試前，使用 ClassInitialize 執行程式碼
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在類別中的所有測試執行後，使用 ClassCleanup 執行程式碼
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在執行每一項測試之前，先使用 TestInitialize 執行程式碼 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在執行每一項測試之後，使用 TestCleanup 執行程式碼
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGetCarRealTime()
        {
            //
            // TODO:  在此加入測試邏輯
            //
            string msg = "";
            LZGPSDataServiceSoapClient soapClient = new LZGPSDataServiceSoapClient();
            var response = soapClient.GetRealTimeGPS("", "PsTcom", out msg);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(response));

        }

        [TestMethod]
        public void TestGetCarHistory()
        {
            //
            // TODO:  在此加入測試邏輯
            //
            string msg = "";
            LZGPSDataServiceSoapClient soapClient = new LZGPSDataServiceSoapClient();
            var response = soapClient.GetCarGPS("0002-NW", "2022-04-27", "00:00", "23:59", "", "PsTcom", out msg);
            var obj = JsonConvert.DeserializeObject("{}");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(response));

        }

        [TestMethod]
        public void TestIns_LZCarsList()
        {
            //
            // TODO:  在此加入測試邏輯
            //
            string msg = "";
            LZGPSDataServiceSoapClient soapClient = new LZGPSDataServiceSoapClient();
            var response = soapClient.Ins_LZCarsList("01-TP", "", "PsTcom", out msg);
            Assert.IsTrue(response);

        }

        [TestMethod]
        public void TestDel_LZCarsList()
        {
            //
            // TODO:  在此加入測試邏輯
            //
            string msg = "";
            LZGPSDataServiceSoapClient soapClient = new LZGPSDataServiceSoapClient();
            var response = soapClient.Ins_LZCarsList("01-TP", "", "PsTcom", out msg);
            Assert.IsTrue(response);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Google.Cloud.Firestore.V1;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using MVCServer.DataAccess;
using System.Data.Entity;
using MVCServer.Models;
using MVCServer;


namespace MVCServer.Controllers
{
    public class HomeController : Controller
    {
        
        
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ViewResult> About()
        {
            var studentsList = new List<Mail>();
            string filepath = HttpContext.Server.MapPath("~/connect/creds.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            string projectId = "emailparser-cb4bb";
            FirestoreDb firestoreDb = FirestoreDb.Create(projectId);

            
            

            
            Query allAddressQuery = firestoreDb.Collection("emaildata").Select("address");
            Query allClientsQuery = firestoreDb.Collection("emaildata").Select("client");
            Query allPhonesQuery = firestoreDb.Collection("emaildata").Select("contact");
            QuerySnapshot allAddressQuerySnapshot = await allAddressQuery.GetSnapshotAsync();
            QuerySnapshot allClientsQuerySnapshot = await allClientsQuery.GetSnapshotAsync();
            QuerySnapshot allPhonesQuerySnapshot = await allPhonesQuery.GetSnapshotAsync();



            //all data
            Query allQuery = firestoreDb.Collection("emaildata");
            QuerySnapshot allQuerySnapshot = await allQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshotAll in allQuerySnapshot.Documents)
            {
                

                Dictionary<string, object> dataAll = documentSnapshotAll.ToDictionary();
                System.Diagnostics.Debug.WriteLine(dataAll.ToString());
                var resultContact = dataAll.Where(i => i.Key == "contact");
                var resultClient = dataAll.Where(i => i.Key == "client");
                var resultAddress = dataAll.Where(i => i.Key == "address");
                foreach (KeyValuePair<string, object> pairContact1 in resultContact)
                {
                    var stringContact = pairContact1.ToString();
                    studentsList.Add(new Mail
                    {
                        Contact = stringContact
                    });
                }
                foreach (KeyValuePair<string, object> pairClient1 in resultClient)
                {
                    var stringClient = pairClient1.ToString();
                    studentsList.Add(new Mail
                    {
                        Client = stringClient
                    });
                }
                foreach (KeyValuePair<string, object> pairAddress1 in resultAddress)
                {
                    var stringAddress = pairAddress1.ToString();
                    studentsList.Add(new Mail
                    {
                        Address = stringAddress
                    });
                }
                
               
                //foreach (KeyValuePair<string, object> pair in dataAll)
                //{


                //    var result = pair.ToString();
                //    studentsList.Add(new Mail
                //    {
                //        Client = result
                //    });
                //    System.Diagnostics.Debug.WriteLine(result);
                //}
            }



            //foreach (DocumentSnapshot documentSnapshot3 in allClientsQuerySnapshot.Documents)
            //{
            //    Dictionary<string, object> data3 = documentSnapshot3.ToDictionary();
            //    foreach (KeyValuePair<string, object> pairClient in data3)
            //    {
            //       resultClient = pairClient.ToString();
            //        studentsList.Add(new Mail
            //        {
            //            Client = resultClient
            //        });
            //    }
            //}

            //foreach (DocumentSnapshot documentSnapshot2 in allPhonesQuerySnapshot.Documents)
            //{
            //    Dictionary<string, object> data2 = documentSnapshot2.ToDictionary();
            //    foreach (KeyValuePair<string, object> pairPhone in data2)
            //    {
            //        resultContact = pairPhone.ToString();
            //        studentsList.Add(new Mail
            //        {
            //            Contact = resultContact
            //        });
            //    }
            //}

            //foreach (DocumentSnapshot documentSnapshot1 in allAddressQuerySnapshot.Documents)
            //{
            //    Dictionary<string, object> data1 = documentSnapshot1.ToDictionary();
            //    foreach (KeyValuePair<string, object> pairAddress in data1)
            //    {
            //        string resultAddress = pairAddress.ToString();

            //        studentsList.Add(new Mail {
            //            Address = resultAddress
            //        });
            //        System.Diagnostics.Debug.WriteLine(resultAddress);
            //    }
            //}







            //Query allClientsQuery = firestoreDb.Collection("emaildata").Select("client"); ;
            //QuerySnapshot allClientsQuerySnapshot = await allClientsQuery.GetSnapshotAsync();
            //foreach (DocumentSnapshot documentSnapshot2 in allClientsQuerySnapshot.Documents)
            //{
            //    Dictionary<string, object> data1 = documentSnapshot2.ToDictionary();
            //    foreach (KeyValuePair<string, object> pair in data1)
            //    {
            //        string resultClient = pair.ToString();
            //        studentsList.Add(new Mail { Client = resultClient });
            //        System.Diagnostics.Debug.WriteLine(resultClient);
            //    }
            //}
            //Query allPhonesQuery = firestoreDb.Collection("emaildata").Select("contact"); ;
            //QuerySnapshot allPhonesQuerySnapshot = await allPhonesQuery.GetSnapshotAsync();
            //foreach (DocumentSnapshot documentSnapshot3 in allPhonesQuerySnapshot.Documents)
            //{
            //    Dictionary<string, object> data1 = documentSnapshot3.ToDictionary();
            //    foreach (KeyValuePair<string, object> pair in data1)
            //    {
            //        string resultContact = pair.ToString();
            //        studentsList.Add(new Mail { Contact = resultContact });
            //        System.Diagnostics.Debug.WriteLine(resultContact);
            //    }
            //}
            return View(studentsList);
            //return studentsList.Select(s => new mail
            //{
            //    address = s.address
            //}
            //                           ).ToList();

            //Call("emailparser-cb4bb").GetAwaiter();
            
        }

        //public static async Task Call(string emaildata)
        //{
        //    string path = AppDomain.CurrentDomain.BaseDirectory + @"connect/creds.json";
        //    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
        //    FirestoreDb db = FirestoreDb.Create(emaildata);
        //    System.Diagnostics.Debug.WriteLine("mphke firebase");


        //    Query usersRef = db.Collection("emaildata");
        //    System.Diagnostics.Debug.WriteLine("eim anamesa");
        //    QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
        //    System.Diagnostics.Debug.WriteLine("eftasa prin to foreach");
        //    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
        //    {
        //        Dictionary<string, object> documentDictionary = documentSnapshot.ToDictionary();
        //        if (documentDictionary.ContainsKey("address"))
        //        {
        //            System.Diagnostics.Debug.WriteLine("Address: {0}", documentDictionary["address"]);
        //        }
        //    }

        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Google.Cloud.Firestore;

namespace MVCServer.Models
{
    //[FirestoreData]
    public class Mail
    {
        //[FirestoreProperty]
        public string Address { get; set; }
        public string Client { get; set; }
        public string Contact { get; set; }
    }

    
}
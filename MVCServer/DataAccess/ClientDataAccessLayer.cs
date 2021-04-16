using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using MVCServer.Models;

namespace MVCServer.DataAccess
{
    public class ClientDataAccessLayer
    {
        string projectId;
        FirestoreDb firestoreDb;
        public ClientDataAccessLayer()
        {
            string filepath = "connect/creds.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            projectId = "emailparser-cb4bb";
            firestoreDb = FirestoreDb.Create(projectId);
        }
        public async Task<List<Mail>> Getmails()
        {
            var studentsList = new List<Mail>();
            FirestoreDb db = FirestoreDb.Create(projectId);
            Query allStudentsQuery = db.Collection("mail");
            QuerySnapshot allStudentsQuerySnapshot = await allStudentsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allStudentsQuerySnapshot.Documents)
            {
                studentsList.Add(documentSnapshot.ConvertTo<Mail>());
            }
            return studentsList.Select(s => new Mail
            {
                Address = s.Address
            }
                                       ).ToList();
        }
            public  async Task<List<Mail>> GetMails1()

        {
            try
            {

                Query usersRef = firestoreDb.Collection("mail");
                QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
                
                List<Mail> Lst = new List<Mail>();

                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> documentDictionary = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(documentDictionary);
                        Mail newMail = JsonConvert.DeserializeObject<Mail>(json);
                        Lst.Add(newMail);
                    }
                }
                return Lst;
            }
            catch(Exception e){
                throw e;
            }
        }
    }
}
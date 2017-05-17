using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestProject
{
    public class Program
    {
        private static void Main(string[] args)
        {

          //Load data from Data class and map to JsonMapper.
           var data = new Data();

           var recipientsInformation = JsonConvert.DeserializeObject<JsonMapper>(data.MemberInformation());

           FindMatch(recipientsInformation);
        }

        private static void FindMatch(JsonMapper recipientsInformation)
        {
            var finalListOfMatchedRecipients = new List<Tuple<string, string>>();

            foreach(var recipient in recipientsInformation.recipients)
            {
               var matchedResult =  CheckForMatchedTags(recipient, recipientsInformation);
                finalListOfMatchedRecipients.AddRange(matchedResult);
            }
            foreach (var matchingRecipient in finalListOfMatchedRecipients)
                Console.WriteLine(matchingRecipient);
            Console.ReadLine();
        }

        private static List<Tuple<string, string>> CheckForMatchedTags(Recipient recipient, JsonMapper recipientsInformation)
        {
            var myTags = recipient.tags;
            var matchedRecipients = new List<Tuple<string, string>>();

           var remainingRecipients = recipientsInformation.recipients.Select(x => x).Where(x=> x.id > recipient.id);

            foreach (var eachRecipient in remainingRecipients)
            {
                var matchingCount = new List<bool>();

                for (var i = 0; i < myTags.Count; i++)
                {
                    var isMatched = eachRecipient.tags.Any(x => myTags[i].Equals(x));

                    if(isMatched)
                        matchingCount.Add(isMatched);
                }              

                if (matchingCount.Count > 1)
                    matchedRecipients.Add( new Tuple<string, string> (recipient.name, eachRecipient.name));
            }
            return matchedRecipients;
        }
    }
}

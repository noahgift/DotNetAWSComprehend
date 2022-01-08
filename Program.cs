using System;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;


namespace Comprehend
{
    class Program
    {
        static async Task Main(string[] args)
        {

            // Display title
            Console.WriteLine("AWS AI API Sentiment Detector" + Environment.NewLine);

            // Ask for phrase
            Console.WriteLine("Type in phrase for analysis" + Environment.NewLine);
            var phrase = Console.ReadLine();
            
            // Detect Sentiment
            AmazonComprehendClient comprehendClient = new AmazonComprehendClient();
            Console.WriteLine("Calling DetectSentiment");
            DetectSentimentRequest detectSentimentRequest = new DetectSentimentRequest()
            {
                Text = phrase,
                LanguageCode = "en"
            };

            DetectSentimentResponse detectSentimentResponse = await
            comprehendClient.DetectSentimentAsync(detectSentimentRequest);
            Console.WriteLine(detectSentimentResponse.Sentiment);
            Console.WriteLine("Done");
        }
    }
}


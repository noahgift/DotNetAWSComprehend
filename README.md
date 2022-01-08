# DotNetAWSComprehend
Using .NET with AWS Comprehend


## Getting started

### AWS Cloudshell

```bash
aws comprehend detect-sentiment \
    --language-code "en" \
    --text "I love C#."
```


#### Using lynx to dump text

You can also use lynx to go further.

`sudo yum install lynx`

Next, dump the page for Albert Enstein and pipe it into less to explore it.

`lynx -dump https://en.wikipedia.org/wiki/Albert_Einstein | less`

By using `wc -l` we get a count of the lines:

`lynx -dump https://en.wikipedia.org/wiki/Albert_Einstein | wc -l`

To get the amount of bytes we can use `wc --bytes`:

`lynx -dump https://en.wikipedia.org/wiki/Albert_Einstein | wc --bytes
432232`

Because the AWS command-line tool only accepts up to 5000 bytes, we need to truncate the output.

```bash
TEXT=`lynx -dump https://en.wikipedia.org/wiki/Albert_Einstein | head -c 5000`
```

The output is below.

```
[cloudshell-user@ip-10-1-85-74 ~]$ aws comprehend detect-sentiment --language-code "en" --text "$TEXT"
{
    "Sentiment": "NEUTRAL",
    "SentimentScore": {
        "Positive": 0.3402811586856842,
        "Negative": 0.0033634265419095755,
        "Neutral": 0.6556956768035889,
        "Mixed": 0.0006596834864467382
    }
}
```

#### Going further with Bash shell processing and AWS entity extraction

![pipeline](https://user-images.githubusercontent.com/58792/148614301-0d739107-3923-4f6e-b3fa-a47222300cc3.png)


Next let's move on to trying to do another more AWS Comprehend action

```bash
aws comprehend detect-entities \
    --language-code "en" \
    --text "$TEXT" \
    --output text | head
```

* You see a more complex pipeline here:  [ai-pipeline-aws.sh](https://github.com/noahgift/DotNetAWSComprehend/blob/main/ai-pipeline-aws.sh)

## Using CSharp

```csharp
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
```


## References

* [Prototyping AI apis aws cloudshell bash on YouTube](https://www.youtube.com/watch?v=o469kbmh_iM)
* [Prototyping AI apis aws cloudshell bash on O'Reilly](https://learning.oreilly.com/videos/prototyping-aws-ai/01072022VIDEOPAIML/)

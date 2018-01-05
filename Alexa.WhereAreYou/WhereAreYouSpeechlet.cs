using AlexaSkillsKit.Slu;
using AlexaSkillsKit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlexaSkillsKit.Speechlet;

namespace Alexa.WhereAreYou
{
    public class WhereAreYouSpeechlet : Speechlet
    {
        private const string StartTimeKey = "startTime";
        public override SpeechletResponse OnIntent(IntentRequest request, Session session)
        {
            Intent intent = request.Intent;
            string intentName = (intent != null) ? intent.Name : null;

            // Note: If the session is started with an intent, no welcome message will be rendered;
            // rather, the intent specific response will be returned.
            if ("StartGame".Equals(intentName))
            {
                return StartGame(intentName);

            }
            else if ("WhereAreYou".Equals(intentName))
            {
                return BuildSpeechletResponse(intentName, "I'm hiding", false);
            }
            else if("Found".Equals(intentName))
            {
                if (session.Attributes.ContainsKey(StartTimeKey) && session.Attributes[StartTimeKey] != null)
                {
                    return BuildSpeechletResponse(intentName, $"You found me in {(int)(DateTime.UtcNow - DateTime.Parse(session.Attributes[StartTimeKey])).TotalSeconds} seconds", false);
                }
                return BuildSpeechletResponse(intentName, "You must say ready before you can find me", false);
            }
            else if("Ready".Equals(intentName))
            {
                if (!session.Attributes.ContainsKey(StartTimeKey))
                {
                    session.Attributes.Add(StartTimeKey, DateTime.UtcNow.ToString());
                }else
                {
                    session.Attributes[StartTimeKey] = DateTime.UtcNow.ToString();
                }
                return BuildSpeechletResponse(intentName, "", false);

            }
            else if ("EndGame".Equals(intentName))
            {
                return BuildSpeechletResponse(intentName, "Terminating game", true);
            }
            else
            {
                throw new SpeechletException("Invalid Intent");
            }
        }

        private SpeechletResponse StartGame(string intentName)
        {

            return BuildSpeechletResponse(intentName, "Let's play. Hide me somewhere, once I'm hidden say ready and the game starts. If you need a clue while we play say where are you?", false);
        }

        //private SpeechletResponse SetNameInSessionAndSayHello(Intent intent, Session session)
        //{
        //    // Get the slots from the intent.
        //    IDictionary<string, Slot> slots = intent.Slots;

        //    // Get the name slot from the list slots.
        //    Slot nameSlot = slots[NAME_SLOT];
        //    string speechOutput = "";

        //    // Check for name and create output to user.
        //    if (nameSlot != null)
        //    {
        //        // Store the user's name in the Session and create response.
        //        string name = nameSlot.Value;
        //        session.Attributes[NAME_KEY] = name;
        //        speechOutput = String.Format(
        //            "Hello {0}, now I can remember your name, you can ask me your name by saying, whats my name?", name);
        //    }
        //    else
        //    {
        //        // Render an error since we don't know what the users name is.
        //        speechOutput = "I'm not sure what your name is, please try again";
        //    }

        //    // Here we are setting shouldEndSession to false to not end the session and
        //    // prompt the user for input
        //    return BuildSpeechletResponse(intent.Name, speechOutput, false);
        //}

        private SpeechletResponse BuildSpeechletResponse(string title, string output, bool shouldEndSession)
        {
            SimpleCard card = new SimpleCard();
            card.Title = String.Format("SessionSpeechlet - {0}", title);
            card.Subtitle = String.Format("SessionSpeechlet - Sub Title");
            card.Content = String.Format("SessionSpeechlet - {0}", output);

            // Create the plain text output.
            PlainTextOutputSpeech speech = new PlainTextOutputSpeech();
            speech.Text = output;
            
            // Create the speechlet response.
            SpeechletResponse response = new SpeechletResponse();
            response.ShouldEndSession = shouldEndSession;
            response.OutputSpeech = speech;
            response.Card = card;
            return response;

        }

        public override SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session)
        {
            return GetWelcomeResponse();
        }


        public override void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session)
        {
            
        }

        public override void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session)
        {
           
        }

        private SpeechletResponse GetWelcomeResponse()
        {
            // Create the welcome message.
            string speechOutput =
                "Welcome to hide and seek with Alexa. Say start game to begin a new game.";

            // Here we are setting shouldEndSession to false to not end the session and
            // prompt the user for input
            return BuildSpeechletResponse("Welcome", speechOutput, false);
        }
    }
}
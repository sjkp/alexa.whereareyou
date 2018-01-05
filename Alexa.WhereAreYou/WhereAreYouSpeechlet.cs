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
        public override SpeechletResponse OnIntent(IntentRequest request, Session session)
        {
            Intent intent = request.Intent;
            string intentName = (intent != null) ? intent.Name : null;

            // Note: If the session is started with an intent, no welcome message will be rendered;
            // rather, the intent specific response will be returned.
            if ("StartGame".Equals(intentName))
            {
                return BuildSpeechletResponse(intentName, "Starting game", false);
            }
            else if ("WhereAreYou".Equals(intentName))
            {
                return BuildSpeechletResponse(intentName, "I'm hiding", false);
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
                "Welcome to the Alexa AppKit session sample app, please tell me your name by saying, my name is Sam";

            // Here we are setting shouldEndSession to false to not end the session and
            // prompt the user for input
            return BuildSpeechletResponse("Welcome", speechOutput, false);
        }
    }
}
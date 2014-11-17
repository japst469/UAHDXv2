//http://stackoverflow.com/questions/704636/sending-email-through-gmail-smtp-server-with-c-sharp
//@Andres Pompiglio: Yes that's right you must change your password at least once.. this codes works just fine:
//COPY CODE HERE

//Bugfix for credentials and stuff at http://answers.unity3d.com/questions/433283/how-to-send-email-with-c.html

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Net.Mail;
using System.Net.Security;                              //Needed for Security
using System.Security.Cryptography.X509Certificates;    //Needed for SSL Stuff
using UnityEngine;

public class Email: MonoBehaviour
{
    public static string Send(string[] mail_creds, string toList, string from, string[] username, byte[] scores)
    {
        int i;                      //Generic counter variables
        int Entries=scores.Count()-1; //# of players

        string body=string.Empty;   //Body of message
        DateTime Time = System.DateTime.Now;    //The time NOW
        string subject = "UAHDX Match Results ("+Time+")";  //Subject
        string temp;    //Dummy string var for concatenation

        byte HiScore=0; //HiScore Value
        string[] Winner=new string[Entries+1]; //Big string holding winner(s)
        SByte Ties=-1;  //# of ties

        //Build message body. Use "<br />" in place of carriage returns "\n"
        body="To whom it may concern:<br /><br />";        
        body+="The results of your online Ultra Air Hockey Deluxe match on "+Time.ToString()+" resulted in scores of:<br /><br />";

        //Add player usernames and scores
        for (i = 0; i <= Entries; i++)
        {
            body += username[i] + ": " + scores[i].ToString();   //"Name: Score"
            body += "<br />";                                   //Carriage return
        }
        body += "<br />";

        //Get the HiScore and the name(s)
        HiScore=scores.Max();    //Get HiScore
        //Find index(es) of Winner(s), then get the name(s)
        for (i = 0; i <= Entries; i++)
        {
            if (scores[i]==HiScore)  //If equal to the HiScore
            {
                Ties++; //Increment Ties
                Winner[i]=username[i];  //Get the username
            }
            else
            {
                Winner[i]=string.Empty;
            }
        }

        //Get concat strings of winner(s)
        temp=string.Empty;
        for (i = 0; i <= Entries; i++)
        {
            if (Winner[i] != string.Empty)
            {
                temp += Winner[i] + ", ";
            }
        }
        temp = temp.TrimEnd(' ',',');   //Remove extra, trailing comma+space for final name in concatenation

        //Change # of noun of "to have" as appropriately
        if (Ties > 0)
        {
            temp += " have";    //Plural
        }
        else
        {
            temp += " has"; //Singular
        }

        body += "Congratulations! " + temp + " won the match!<br /><br />";
        body += "Thank you for playing Ultra Air Hockey DX!<br />";
        body += "You can find other exciting video games and applications<br />";
        body += "at EagleSoft Ltd's website (https://sites.google.com/site/eaglesoft92/)<br /><br />";
        body += "Cordially,<br />";
        body +="The UAHDX Team";

        //SMTP Stuff.
        //See links at top of this script for info.

        MailMessage message = new MailMessage();
        string msg = string.Empty;
        try
        {
            //Allocate parameters as appropriately to MailMessage properties
            MailAddress fromAddress = new MailAddress(from);
            message.From = fromAddress;
            message.To.Add(toList);     //Get the "To:" fields
            message.Subject = subject;
            message.IsBodyHtml = true;  //Use HTML!
            message.Body = body;        //Dump the body into the variable

            // We use GMAIL as our smtp client!
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(mail_creds[0], mail_creds[1]) as ICredentialsByHost; //Use GMail username and password as credentials
            smtpClient.Send(message);   //Send da message!
            Debug.Log("You got spammed!");
        }

        catch (Exception ex)
        {
            //Catch errors and display them
            Debug.Log("Email Failure");
            msg = ex.Message;            
        }
        return msg;
    }
    
    //End Send Email Function
}

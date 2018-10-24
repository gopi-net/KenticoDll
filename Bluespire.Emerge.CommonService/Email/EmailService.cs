using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.EmailEngine;
using CMS.SiteProvider;
using System.Net.Mail;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.Common;
using System.Net.Mime;
using CMS.DataEngine;
using CMS.MacroEngine;

namespace Bluespire.Emerge.CommonService.Email
{
    /// <summary>
    /// Class for sending emails
    /// </summary>
    public static class EmailService
    {
        /// <summary>
        /// Send an email using the EmailMessageInfo object
        /// </summary>
        /// <param name="messageInfo">EmailMessageInfo object</param>
        public static void SendEmail(EmailMessageInfo messageInfo)
        {
            try
            {
                EmailMessage message = getEmailMessageObject(messageInfo);
                EmailSender.SendEmail(SiteContext.CurrentSiteName, message, true);
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("Email Service - SendEmail", EventCode.EMERGE_EMAIL, ex.ToString());
                throw new EmailSendException("There has been an error while sending email.", ex);
            }
        }

        /// <summary>
        /// Sends an email using the template created in CMS.
        /// </summary>
        /// <param name="messageInfo">EmailMessageInfo object</param>
        /// <param name="templateName">Code name of the template to be used.</param>
        /// <param name="macros">An array of values that should be dynamically inserted into the e-mail body text and subject</param>
        /// <param name="sendImmediately">if false then email will be stored in email queue and then will be sent through schedular.</param>
        public static void SendEmailUsingTemplate(EmailMessageInfo messageInfo, string templateName, string[,] macros, bool sendImmediately)
        {
            try
            {
                string siteName = SiteContext.CurrentSiteName;
                string DefaultSenderAddress = String.Format(Constants.DEFAULT_SENDER_ADDRESS, siteName);

                EmailTemplateInfo emailTemplate = EmailTemplateProvider.GetEmailTemplate(templateName, SiteContext.CurrentSiteName);

                messageInfo.From = (messageInfo.From == null || messageInfo.From.Trim() == string.Empty) ? EmailHelper.GetSender(emailTemplate, DefaultSenderAddress) : messageInfo.From;
                messageInfo.Body = emailTemplate.TemplateText;


                EmailMessage message = getEmailMessageObject(messageInfo);
                message.EmailFormat = EmailFormatEnum.Html;


                EmailHelper.ResolveMetaFileImages(message, emailTemplate.TemplateID, Constants.KENTICO_EMAILOBJECTTYPE_EMAILTEMPLATE, ObjectAttachmentsCategories.TEMPLATE);

                MacroResolver resolver = MacroResolver.GetInstance();
                MacroSettings settings = new MacroSettings();
                settings.EncodeResolvedValues = true;
                resolver.SetNamedSourceData(macros);
                message.Body = resolver.ResolveMacros(message.Body);

                settings.EncodeResolvedValues = false;
                if (string.IsNullOrEmpty(message.Subject))
                    message.Subject = resolver.ResolveMacros(emailTemplate.TemplateSubject);
                else
                    message.Subject = resolver.ResolveMacros(message.Subject);


                EmailSender.SendEmail(siteName, message, false);
                //EmailSender.SendEmailWithTemplateText(siteName, message, emailTemplate, resolver, sendImmediately);

            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("Email Service - SendEmailUsingTemplate", EventCode.EMERGE_EMAIL, ex.ToString());
                throw new EmailSendException("There has been an error while sending email.", ex);
            }
        }

        private static EmailMessage getEmailMessageObject(EmailMessageInfo messageInfo)
        {
            EmailMessage message = new EmailMessage();

            //MailMessage mailMessage = new MailMessage();

            //if (messageInfo.LinkedResources.Count > 0)
            //{
            //    AlternateView alternateView = AlternateView.CreateAlternateViewFromString(messageInfo.Body, Encoding.UTF8, MediaTypeNames.Text.Html);
            //    foreach (LinkedResource resource in messageInfo.LinkedResources)
            //    {
            //        alternateView.LinkedResources.Add(resource);
            //    }
            //    mailMessage.AlternateViews.Add(alternateView);
            //}

            //EmailMessage message = new EmailMessage(mailMessage);


            message.EmailFormat = messageInfo.IsBodyHtml ? EmailFormatEnum.Html : EmailFormatEnum.Default;
            message.From = messageInfo.From;
            message.Recipients = messageInfo.Recipients;
            message.CcRecipients = messageInfo.CcRecipients;
            message.BccRecipients = messageInfo.BccRecipients;
            message.Body = messageInfo.Body;
            message.PlainTextBody = messageInfo.PlainTextBody;
            message.Priority = EmailPriorityEnum.Normal;
            message.ReplyTo = messageInfo.ReplyTo;
            message.Subject = messageInfo.Subject;

            foreach (Attachment attachment in messageInfo.Attachments)
            {
                message.Attachments.Add(attachment);
            }


            return message;
        }
    }
}

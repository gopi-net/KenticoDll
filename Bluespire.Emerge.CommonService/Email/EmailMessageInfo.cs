using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;

namespace Bluespire.Emerge.CommonService.Email
{
    /// <summary>
    /// Class that represents an email information
    /// </summary>
    public class EmailMessageInfo
    {
        #region "Variables"

        private AttachmentCollection mAttachments;
        private List<LinkedResource> mLinkedResources;
        
        #endregion


        #region "Properties"

        /// <summary>
        /// Gets or sets the From address.       
        /// </summary>
        public string From
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the ReplyTo address.
        /// </summary>
        public string ReplyTo
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the Recipients.
        /// </summary>
        public string Recipients
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the CcRecipients.
        /// </summary>
        public string CcRecipients
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the BccRecipients.
        /// </summary>
        public string BccRecipients
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the Subject.
        /// </summary>
        public string Subject
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the Body.
        /// </summary>
        public string Body
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets plain text body.
        /// </summary>
        public string PlainTextBody
        {
            get;
            set;
        }

        public bool IsBodyHtml
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the collection of e-mail attachments.
        /// </summary>
        public AttachmentCollection Attachments
        {
            get
            {
                return mAttachments ?? (mAttachments = new MailMessage().Attachments);
            }
        }

        /// <summary>
        /// Gets the collection of linked resources.
        /// </summary>
        public List<LinkedResource> LinkedResources
        {
            get
            {
                return mLinkedResources ?? (mLinkedResources = new List<LinkedResource>());
            }
        }
        
        #endregion
    }
}

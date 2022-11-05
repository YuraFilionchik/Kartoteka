using System.Configuration;
using Crypt;
namespace DBFprog1
{
    /// <summary>
    /// Class holds the <pass> element
    /// </summary>
    public class PassElement : ConfigurationElement
    {
         
        
        // Holds the Name attribute of the Message
        private static  ConfigurationProperty UserName =
            new ConfigurationProperty("Name", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

        // Holds the Value attribute value of Message.
        private static  ConfigurationProperty PassValue =
            new ConfigurationProperty("Value", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

        public PassElement()
        {
            
            base.Properties.Add(UserName);
            base.Properties.Add(PassValue);
        }

        /// <summary>
        /// Name
        /// </summary>
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get { return (string)this[UserName]; }
            set { this[UserName] = Crypt.Cripting.Protect(value); }
        }

        /// <summary>
        /// Value
        /// </summary>
        [ConfigurationProperty("Value", IsRequired = true)]
        public string Value
        {
            get
            {
                return Crypt.Cripting.Unprotect((string)this[PassValue]);
            }
            set { this[PassValue] = Crypt.Cripting.Protect(value); }
        }
    }
}

using System.Configuration;

namespace DBFprog1
{
    public class MyConfiguration : ConfigurationSection
    {
        private static readonly ConfigurationProperty PassElement =
             new ConfigurationProperty("Passwords", typeof(PassCollection), null, ConfigurationPropertyOptions.IsRequired);

        public MyConfiguration()
        {
           base.Properties.Add(PassElement);
        }

        ///// <summary>
        ///// To
        ///// </summary>
        //[ConfigurationProperty("To", IsRequired = true)]
        //public string To
        //{
        //    get { return (string)this[toAttribute]; }
        //}
        
        ///// <summary>
        ///// From
        ///// </summary>
        //[ConfigurationProperty("From", IsRequired = true)]
        //public string From
        //{
        //    get { return (string)this[fromAttribute]; }
        //}

        /// <summary>
        /// pass Collection
        /// </summary>
        [ConfigurationProperty("Passwords", IsRequired = true)]
        public PassCollection Passwords
        {
            get { return (PassCollection)this[PassElement]; }
        }
    }
}

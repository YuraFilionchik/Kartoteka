using System.Configuration;

namespace DBFprog1
{
    [ConfigurationCollection(typeof(PassElement), AddItemName = "Pass",
         CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class PassCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PassElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PassElement)element).Name;
        }
     
        new public PassElement this[string name]
        {
            get { return (PassElement)BaseGet(name); }
        }
    }
}

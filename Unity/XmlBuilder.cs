using System.Xml;

public class XmlBuilder : MonoBehaviour
{
    private void Build(string path)
    {
        XmlDocument xmlDoc = new XmlDocument();

        // Creazione dell'elemento radice
        XmlElement root = xmlDoc.CreateElement("root");
        xmlDoc.AppendChild(root);

        // Creazione di un nuovo elemento
        XmlElement newElement = xmlDoc.CreateElement("block");
        newElement.SetAttribute("active", "true");
        newElement.SetAttribute("name", "A");
        newElement.SetAttribute("child", "true");

        // Creazione di un elemento figlio
        XmlElement childElement = xmlDoc.CreateElement("elem");
        childElement.SetAttribute("name", "B");
        childElement.SetAttribute("mode", "both");
        childElement.SetAttribute("full", "true");

        // Aggiungi l'elemento figlio al nuovo elemento
        newElement.AppendChild(childElement);

        // Aggiungi il nuovo elemento al file XML
        root.AppendChild(newElement);

        // Salva il file XML
        xmlDoc.Save(path);
    }
    
    //Non crea la struttura in memoria ma scrive direttamente sul file (più performante)
    private void BuildNoMemory(string path)
    {
        // Creazione dell'oggetto XmlWriter
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        XmlWriter xmlWriter = XmlWriter.Create(path, settings);

        // Scrittura del documento XML
        xmlWriter.WriteStartElement("root");
        
        //Blocco con controlli di sicurezza (evita attacchi di tipo injection)
        xmlWriter.WriteStartElement("block");
        
        //L'attributo "active" viene convertito in stringa in modo sicuro
        xmlWriter.WriteAttributeString("active", XmlConvert.ToString(true));
        //L'attributo "name" viene verificato con la funzione XmlConvert.VerifyNCName per garantire che sia valido
        xmlWriter.WriteAttributeString("name", XmlConvert.VerifyNCName(name));
        //L'attributo "active" viene convertito in stringa in modo sicuro
        xmlWriter.WriteAttributeString("child", XmlConvert.ToString(true));
        
        //Blocco senza controlli di sicurezza
        xmlWriter.WriteStartElement("elem");
        xmlWriter.WriteAttributeString("name", "B");
        xmlWriter.WriteAttributeString("mode", "both");
        xmlWriter.WriteAttributeString("full", "true");
        
        xmlWriter.WriteEndElement(); // chiusura di elem
        xmlWriter.WriteEndElement(); // chiusura di block
        xmlWriter.WriteEndElement(); // chiusura di root
        
        // Chiusura dell'oggetto XmlWriter e salvataggio del file XML
        xmlWriter.Close();
    }
}

using System.Threading.Tasks;

namespace AXML
{
    public class GetXml
    {
        private readonly bool addXmlHead = true;
        private readonly string inputFilePath;
        private readonly string outputFilePath;

        public GetXml(bool addXmlHead, string inputFilePath, string outputFilePath)
        {
            this.addXmlHead = addXmlHead;
            this.inputFilePath = inputFilePath;
            this.outputFilePath = outputFilePath;
        }

        public async Task<string> GetAsync()
        {
            // TODO
            return null;
        }
    }
}

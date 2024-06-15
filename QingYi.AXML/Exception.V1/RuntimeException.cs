namespace QingYi.AXML.Exception.V1
{
    public class RuntimeException : System.Exception
    {
        public override string Message
        {
            get
            {
                return $"[RuntimeException] {base.Message}";
            }
        }

        public RuntimeException() : base()
        {
        }

        public RuntimeException(string message) : base(message)
        {
        }

        public RuntimeException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
